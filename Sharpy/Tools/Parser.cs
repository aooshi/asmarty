using System.Collections.Generic;
using System.Text;
using Sharpy.Tools.ParserNodes;
using Sharpy.ViewEngine;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.Tools
{
    internal class Parser
    {
        private int currentNodeId;
        private readonly Tokenizer tokenizer;
        private readonly SharpyFunctions functions;
        private Token currentToken;
        private const string master = "master";

        public Parser(int currentNodeId, Tokenizer tokenizer, SharpyFunctions functions)
        {
            this.currentNodeId = currentNodeId;
            this.tokenizer = tokenizer;
            this.functions = functions;

            ReadNextToken(true);
        }

        public IEnumerable<IParserNode> ParseAll()
        {
            var nodes = new List<IParserNode>();

            var currentNode = GetNextNode();
            while (currentNode != null)
            {
                nodes.Add(currentNode);
                currentNode = GetNextNode();
            }

            return nodes;
        }

        private void ReadNextToken(bool couldBeHtml) 
        { 
            currentToken = tokenizer.ReadNextToken(couldBeHtml); 
        }

        internal IParserNode GetNextNode()
        {
            if (currentToken == null)
            {
                return null;
            }
            else
            {
                if (currentToken.Type == TokenType.OpenBrace)
                {
                    return Dynamic();
                }
                else
                {
                    var html = new StringBuilder();
                    while (currentToken != null && currentToken.Type != TokenType.OpenBrace)
                    {
                        html.Append(currentToken.Value);
                        ReadNextToken(true);
                    }

                    return new HtmlNode(currentNodeId++, html.ToString());
                }    
            }
        }

        private IParserNode Dynamic()
        {
            ReadNextToken(false);

            if (currentToken.Type == TokenType.Asterisk)
            {
                return Comment();
            }
            else
            {
                var functionName = currentToken.Value;
                if (functions.BlockFunctions.ContainsKey(functionName))
                {
                    ReadNextToken(false);
                    return BlockFunction(functionName);
                }
                else if (functions.ExpressionFunctions.ContainsKey(functionName))
                {
                    ReadNextToken(false);
                    return ExpressionFunction(functionName);
                }
                else if (functions.InlineFunctions.ContainsKey(functionName))
                {
                    ReadNextToken(false);
                    return InlineFunction(functionName);
                }
                else if (functionName == master)
                {
                    ReadNextToken(false);
                    return Master();
                }
                else
                {
                    var expressionNode = Expression();
                    Expect(TokenType.CloseBrace, true);
                    return expressionNode;
                }
            }
        }

        private IParserNode Comment()
        {
            var foundClosingCommentTag = false;
            while (!foundClosingCommentTag)
            {
                ReadNextToken(false);
                if (currentToken.Type == TokenType.Asterisk)
                {
                    ReadNextToken(false);
                    if (currentToken.Type == TokenType.CloseBrace)
                    {
                        ReadNextToken(true);
                        foundClosingCommentTag = true;
                    }
                }
            }

            return new CommentNode(currentNodeId++);
        }

        private IParserNode BlockFunction(string functionName)
        {
            var attributes = Attributes();

            Expect(TokenType.CloseBrace, true);

            var content = GetFunctionContent(functionName);

            var node = new BlockFunctionNode(currentNodeId++, functions.BlockFunctions[functionName], attributes, content);

            ReadNextToken(true);

            return node;
        }

        private IParserNode ExpressionFunction(string functionName)
        {
            Expect(TokenType.Whitespace, false);

            var expression = new StringBuilder();
            while (currentToken.Type != TokenType.CloseBrace)
            {
                expression.Append(currentToken.Value);
                ReadNextToken(false);
            }

            Expect(TokenType.CloseBrace, true);

            var content = GetFunctionContent(functionName);

            ReadNextToken(true);

            return new ExpressionFunctionNode(currentNodeId++, functions.ExpressionFunctions[functionName], expression.ToString(), content);
        }

        private IParserNode InlineFunction(string functionName)
        {
            var attributes = Attributes();

            Expect(TokenType.CloseBrace, true);

            return new InlineFunctionNode(currentNodeId++, functions.InlineFunctions[functionName], attributes);
        }

        private IParserNode Master()
        {
            var attributes = Attributes();

            Expect(TokenType.CloseBrace, true);

            var content = new List<IParserNode>();
            var currentNode = GetNextNode();
            while (currentNode != null)
            {
                content.Add(currentNode);
                currentNode = GetNextNode();
            }

            return new MasterNode(currentNodeId++, attributes, content);
        }

        private string GetFunctionContent(string name)
        {
            var content = new StringBuilder();
            var depth = 0;
            while (depth >= 0)
            {
                if (currentToken.Type == TokenType.OpenBrace)
                {
                    content.Append(currentToken.Value);

                    ReadNextToken(false);
                    content.Append(currentToken.Value);

                    if (currentToken.Type == TokenType.Identifier && currentToken.Value == name)
                    {
                        depth++;
                    }
                }
                else if (currentToken.Type == TokenType.OpenBraceForwardSlash)
                {
                    var savedValue = currentToken.Value;

                    ReadNextToken(false);
                    if (currentToken.Type == TokenType.Identifier && currentToken.Value == name)
                    {
                        depth--;
                    }

                    if (depth >= 0)
                    {
                        content.Append(savedValue);
                        content.Append(currentToken.Value);
                    }
                }
                else
                {
                    content.Append(currentToken.Value);
                }

                ReadNextToken(true);
            }
            return content.ToString();
        }

        private IDictionary<string, ExpressionNode> Attributes()
        {
            if (currentToken.Type == TokenType.Whitespace)
            {
                var attributes = new Dictionary<string, ExpressionNode>();

                ReadNextToken(false);

                var attribute = Attribute();
                attributes.Add(attribute.Key, attribute.Value);

                var optionalAttributes = Attributes();
                if (optionalAttributes != null)
                {
                    foreach (var optionalAttribute in optionalAttributes)
                    {
                        attributes.Add(optionalAttribute.Key, optionalAttribute.Value);
                    }
                }

                return attributes;
            }
            else
            {
                return null;
            }
        }

        private KeyValuePair<string, ExpressionNode> Attribute()
        {
            var identifier = currentToken.Value;

            ReadNextToken(false);
            if (currentToken.Type == TokenType.Equals)
            {
                ReadNextToken(false);

                return new KeyValuePair<string, ExpressionNode>(identifier, Expression());
            }
            else
            {
                return new KeyValuePair<string, ExpressionNode>(identifier, null);
            }
        }

        private ExpressionNode Expression()
        {
            string expression;
            if (currentToken.Type == TokenType.BackTick)
            {
                ReadNextToken(false);
                expression = ReadUntil(new List<TokenType> { TokenType.BackTick });
                ReadNextToken(false);
            }
            else
            {
                expression = ReadUntil(new List<TokenType> { TokenType.CloseBrace, TokenType.Pipe, TokenType.Whitespace });    
            }
            var modifiers = VariableModifiers();

            return new ExpressionNode(currentNodeId++, expression, modifiers);
        }

        private IDictionary<string, IList<ExpressionNode>> VariableModifiers()
        {
            if (currentToken.Type == TokenType.Pipe)
            {
                ReadNextToken(false);
                if (currentToken.Type != TokenType.Identifier)
                {
                    throw new ParsingException(string.Format("Expected {0}, but was {1}", TokenType.Identifier, currentToken.Type));
                }

                var modifiers = new Dictionary<string, IList<ExpressionNode>>();
                var modifier = VariableModifier();
                modifiers.Add(modifier.Key, modifier.Value);

                var optionalModifiers = VariableModifiers();
                if (optionalModifiers != null)
                {
                    foreach (var optionalModifier in optionalModifiers)
                    {
                        modifiers.Add(optionalModifier.Key, optionalModifier.Value);
                    }
                }

                return modifiers;
            }
            else
            {
                return null;
            }
        }

        private KeyValuePair<string, IList<ExpressionNode>> VariableModifier()
        {
            var modifier = currentToken.Value;

            ReadNextToken(false);

            var parameters = new List<ExpressionNode>();
            while (currentToken.Type == TokenType.Colon)
            {
                ReadNextToken(false);

                var value = ReadUntil(new List<TokenType> { TokenType.Colon, TokenType.CloseBrace, TokenType.Pipe });
                parameters.Add(new ExpressionNode(currentNodeId++, value, null));
            }

            return new KeyValuePair<string, IList<ExpressionNode>>(modifier, parameters);
        }

        private string ReadUntil(ICollection<TokenType> typesToLookFor)
        {
            var foundEndingToken = false;
            var escaped = false;
            var doubleEscaped = false;
            string currentQuote = null;
            var value = new StringBuilder();
            while (!foundEndingToken)
            {
                if (currentToken.Type == TokenType.Backslash)
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            value.Append(currentToken.Value);
                        }
                        doubleEscaped = true;
                    }
                    escaped = true;
                    ReadNextToken(false);
                    continue;
                }

                if (currentToken.Value == currentQuote)
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            // We should actually interpret one backslash and the quote
                            value.Append(@"\");
                            value.Append(currentToken.Value);
                            currentQuote = null;
                        }
                        else
                        {
                            // This is an escaped quote - interpret the quote but no the backslash
                            value.Append(currentToken.Value);
                        }
                    }
                    else
                    {
                        currentQuote = null;
                        value.Append(currentToken.Value);
                    }
                }
                else
                {
                    if (escaped)
                    {
                        if (doubleEscaped)
                        {
                            // Just interpret 2 backslashes
                            value.Append(@"\\");
                        }
                        else
                        {
                            // Just interpret a single backslash
                            value.Append(@"\");
                        }
                    }

                    if (currentQuote == null && (currentToken.Type == TokenType.SingleQuote || currentToken.Type == TokenType.DoubleQuote))
                    {
                        currentQuote = currentToken.Value;
                    }

                    if (currentQuote == null && typesToLookFor.Contains(currentToken.Type))
                    {
                        foundEndingToken = true;
                    }
                    else
                    {
                        value.Append(currentToken.Value);
                    }
                }

                escaped = false;
                doubleEscaped = false;
                if (!foundEndingToken)
                {
                    ReadNextToken(false);
                }
            }

            return value.ToString();
        }

        private void Expect(TokenType type, bool couldBeHtml)
        {
            if (currentToken.Type != type)
            {
                throw new ParsingException(string.Format("Expected {0}, but was {1}", type, currentToken.Type));
            }
            ReadNextToken(couldBeHtml);
        }
    }
}
