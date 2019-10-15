using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class StripFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldStripWhitespaces()
        {
            const string input = @"{$name|strip}";

            const string output = @"Grandmother of eight makes hole in one.";

            viewData.Add("name", "Grandmother of\neight makes\t    hole in one.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldReplaceWhitespacesWithSuppliedParameter()
        {
            const string input = @"{$name|strip:'&nbsp;'}";

            const string output = @"Grandmother&nbsp;of&nbsp;eight&nbsp;makes&nbsp;hole&nbsp;in&nbsp;one.";

            viewData.Add("name", "Grandmother of\neight makes\t    hole in one.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
