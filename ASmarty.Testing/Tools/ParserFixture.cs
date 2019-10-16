using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ASmarty.BlockFunctions;
using ASmarty.ExpressionFunctions;
using ASmarty.InlineFunctions;
using ASmarty.Tools;
using ASmarty.Tools.ParserNodes;
using ASmarty.ViewEngine;

namespace ASmarty.Testing.Tools
{
    [TestFixture]
    public class ParserFixture
    {
        private const string input = @"
            <html>
                <head>
                    <title>{$title|capitalize}</title>
                </head>
                <body>
                    {* This is a comment *}
                    {ldelim}
                    <ul>
                        {foreach item='person' source=$model}
                            <li>{$person.Name}: {$person.Age|string_format:'{0:n0}'}</li>
                        {/foreach}
                    </ul>
                    {if $name eq 'bob'}
                        <p>Hello {$name}!</p>
                    {/if}
                </body>
            </html>";

        private Parser parser; 

        [SetUp]
        public void Setup()
        {
            var tokenizer = new Tokenizer(new StringReader(input));
            var blockFunctions = new IBlockFunction[] { new ForEach() };
            var inlineFunctions = new IInlineFunction[] { new LDelim() };
            var expressionFunctions = new IExpressionFunction[] { new If() };

            var functions = new Functions(blockFunctions, inlineFunctions, expressionFunctions, new List<IVariableModifier>());

            parser = new Parser(0, tokenizer, functions);
        }

        [Test]
        public void FirstNodeShouldBeHtml()
        {
            var node = parser.GetNextNode() as HtmlNode;

            Assert.IsNotNull(node);
            AssertHtmlEqual(@"<html><head><title>", node.Content);
        }

        [Test]
        public void SecondNodeShouldBeExpression()
        {
            var node = GetNthNode(2) as ExpressionNode;

            Assert.IsNotNull(node);
            Assert.AreEqual(@"$title", node.Detail);
            Assert.AreEqual(1, node.Modifiers.Count);
        }

        [Test]
        public void ThirdNodeShouldBeHtml()
        {
            var node = GetNthNode(3) as HtmlNode;

            Assert.IsNotNull(node);
            AssertHtmlEqual(@"</title></head><body>", node.Content);
        }

        [Test]
        public void FourthNodeShouldBeComment()
        {
            var node = GetNthNode(4) as CommentNode;

            Assert.IsNotNull(node);
        }

        [Test]
        public void SixthNodeShouldBeInlineFunction()
        {
            var node = GetNthNode(6) as InlineFunctionNode;

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(typeof(LDelim), node.InlineFunction);
        }

        [Test]
        public void SeventhNodeShouldBeHtml()
        {
            var node = GetNthNode(7) as HtmlNode;

            Assert.IsNotNull(node);
            AssertHtmlEqual(@"<ul>", node.Content);
        }

        [Test]
        public void EighthNodeShouldBeBlockFunction()
        {
            var node = GetNthNode(8) as BlockFunctionNode;

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(typeof(ForEach), node.BlockFunction);
            AssertHtmlEqual(@"<li>{$person.Name}: {$person.Age|string_format:'{0:n0}'}</li>", node.Content);
        }

        [Test]
        public void TenthNodeShouldBeExpressionFunction()
        {
            var node = GetNthNode(10) as ExpressionFunctionNode;

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(typeof(If), node.ExpressionFunction);
            Assert.AreEqual(@"$name eq 'bob'", node.Detail);
            AssertHtmlEqual(@"<p>Hello {$name}!</p>", node.Content);            
        }

        [Test]
        public void TwelthNodeShouldBeNullIndicatingEndOfFile()
        {
            var node = GetNthNode(12);

            Assert.IsNull(node);
        }

        private IParserNode GetNthNode(int n)
        {
            for (var i = 0; i < n - 1; i++)
            {
                parser.GetNextNode();
            }

            return parser.GetNextNode();
        }

        private static void AssertHtmlEqual(string expected, string actual)
        {
            Assert.AreEqual(expected.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""), actual.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""));
        }
    }
}
