using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class NewLineToBreakFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldConvertNewLinesToBreaks()
        {
            const string input = @"{$title|nl2br}";

            const string output = @"Sun or rain expected<br />today, dark tonight";

            viewData.Add("title", "Sun or rain expected\ntoday, dark tonight");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

    }
}
