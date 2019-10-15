using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Sharpy.Tools;
using Sharpy.Tools.ParserNodes;
using Sharpy.ViewEngine;

namespace Sharpy.Testing.Tools
{
    [TestFixture]
    public class ParserQuoteEscapeFixture
    {
        [Test]
        public void ShouldIgnoreDoubleQuotesInsideSingleQuoteExpression()
        {
            const string input = "{'bla bla \"bla\"'}";

            var functions = new SharpyFunctions(new List<IBlockFunction>(), new List<IInlineFunction>(), new List<IExpressionFunction>(), new List<IVariableModifier>());

            ExpressionNode node;
            using (var stringReader = new StringReader(input))
            {
                var tokenizer = new Tokenizer(stringReader);
                var parser = new Parser(0, tokenizer, functions);

                node = parser.GetNextNode() as ExpressionNode;
            }

            Assert.IsNotNull(node);
        }

        [Test]
        public void ShouldIgnoreEscapeSingleQuotesInsideSingleQuoteExpression()
        {
            const string input = "{'bla bla \\'bla\\''}";

            var functions = new SharpyFunctions(new List<IBlockFunction>(), new List<IInlineFunction>(), new List<IExpressionFunction>(), new List<IVariableModifier>());

            ExpressionNode node;
            using (var stringReader = new StringReader(input))
            {
                var tokenizer = new Tokenizer(stringReader);
                var parser = new Parser(0, tokenizer, functions);

                node = parser.GetNextNode() as ExpressionNode;
            }

            Assert.IsNotNull(node);
        }
    }
}
