using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class CapitalizeFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldCapitalizeVariable()
        {
            const string input = @"{$name|capitalize}";

            const string output = @"Fred";

            viewData.Add("name", "fred");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldNotCapitalizeWordsWithDigitsByDefault()
        {
            const string input = @"{$articleTitle|capitalize}";

            const string output = @"Next X-Men Film, x3, Delayed";

            viewData.Add("articleTitle", "next x-men film, x3, delayed");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldCapitalizeWordsWithDigits()
        {
            const string input = @"{$articleTitle|capitalize:true}";

            const string output = @"Next X-Men Film, X3, Delayed";

            viewData.Add("articleTitle", "next x-men film, x3, delayed");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
