using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class CountWordsFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReturnTheNumberOfWords()
        {
            const string input = @"{$article|count_words}";
            const string article = @"Dealers Will Hear Car Talk at Noon.";

            viewData.Add("article", article);
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("7", result);
        }
    }
}
