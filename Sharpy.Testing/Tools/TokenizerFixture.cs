using System;
using System.IO;
using NUnit.Framework;
using ASmarty.Tools;

namespace ASmarty.Testing.Tools
{
    [TestFixture]
    public class TokenizerFixture
    {
        private const string input = @"
            <html>
                <head>
                    <title>{$title|capitalize}</title>
                </head>
                <body>
                    {* This is a comment *}
                    <ul>
                        {foreach item='person' source=$model}
                            <li>{$person.Name}: {$person.Age|string_format:'{0:n0}'}</li>
                        {/foreach}
                    </ul>
                </body>
            </html>";

        [Test]
        public void ShouldMatchHtmlToken()
        {
            var tokenizer = new Tokenizer(new StringReader(@"<html><head><title>"));

            var token = tokenizer.ReadNextToken(true);

            Assert.AreEqual(TokenType.Html, token.Type);
            AssertHtmlEqual(@"<html><head><title>", token.Value);
        }

        [Test]
        public void ShouldMatchOpenBrace()
        {
            var tokenizer = new Tokenizer(new StringReader(@"{$title|capitalize}"));

            var token = tokenizer.ReadNextToken(true);

            Assert.AreEqual(TokenType.OpenBrace, token.Type);
        }

        [Test]
        public void ShouldMatchIdentifier()
        {
            var tokenizer = new Tokenizer(new StringReader(@"$title|capitalize}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.Identifier, token.Type);
        }

        [Test]
        public void ShouldMatchPipe()
        {
            var tokenizer = new Tokenizer(new StringReader(@"|capitalize}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.Pipe, token.Type);
        }

        [Test]
        public void ShouldMatchClosingBrace()
        {
            var tokenizer = new Tokenizer(new StringReader(@"}<ul><li>"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.CloseBrace, token.Type);
        }

        [Test]
        public void ShouldMatchAsterisk()
        {
            var tokenizer = new Tokenizer(new StringReader(@"* This is a comment *}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.Asterisk, token.Type);
        }

        [Test]
        public void ShouldMatchWhitespace()
        {
            var tokenizer = new Tokenizer(new StringReader(@" item='person' source=$model}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.Whitespace, token.Type);
        }

        [Test]
        public void ShouldMatchEquals()
        {
            var tokenizer = new Tokenizer(new StringReader(@"='person' source=$model}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.Equals, token.Type);
        }

        [Test]
        public void ShouldMatchSingleQuote()
        {
            var tokenizer = new Tokenizer(new StringReader(@"'person' source=$model}"));

            var token = tokenizer.ReadNextToken(false);

            Assert.AreEqual(TokenType.SingleQuote, token.Type);
        }

        private static void AssertHtmlEqual(string expected, string actual)
        {
            Assert.AreEqual(expected.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""), actual.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""));
        }
    }
}
