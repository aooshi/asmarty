using NUnit.Framework;
using Sharpy.Extensions;
using Sharpy.ViewEngine;

namespace Sharpy.Testing.Extensions
{
    [TestFixture]
    public class StringExtensionsParseExpressionFixture
    {
        [Test]
        public void ShouldParseStringExpression()
        {
            const string expression = "\"somestring\"";

            var result = expression.ParseExpression();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expression, result[0].Detail);
            Assert.AreEqual(PartType.String, result[0].Type);
        }

        [Test]
        public void ShouldParseExpressionWithoutAnyStrings()
        {
            const string expression = "15 > 50";

            var result = expression.ParseExpression();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expression, result[0].Detail);
            Assert.AreEqual(PartType.Expression, result[0].Type);
        }

        [Test]
        public void ShouldParseExpressionWithStringExpression()
        {
            const string expression = "$name eq 'bob'";

            var result = expression.ParseExpression();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("$name eq ", result[0].Detail);
            Assert.AreEqual(PartType.Expression, result[0].Type);
            Assert.AreEqual("\"bob\"", result[1].Detail);
            Assert.AreEqual(PartType.String, result[1].Type);
        }

        [Test]
        public void ShouldEscapeSingleQuotesInSingleQuotes()
        {
            const string input = @"name eq 'isn\'t'";

            var result = input.ParseExpression();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"isn't\"", result[1].Detail);
        }

        [Test]
        public void ShouldEscapeBackslashWithBackslash()
        {
            const string input = @"name eq 'slash:\\'";

            var result = input.ParseExpression();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"slash:\\\"", result[1].Detail);
        }

        [Test]
        public void ShouldEscapeDoubleQuote()
        {
            // The input looks like this: name eq 'eq"eq'
            const string input = "name eq 'eq\"eq'";

            var result = input.ParseExpression();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"eq\\\"eq\"", result[1].Detail);
        }

        [Test]
        public void ShouldLeaveEscapedDoubleQuotesInsideDoubleQuotes()
        {
            // The input looks like this: name eq "eq\"eq"
            const string input = "name eq \"eq\\\"eq\"";

            var result = input.ParseExpression();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("\"eq\\\"eq\"", result[1].Detail);
        }
    }
}
