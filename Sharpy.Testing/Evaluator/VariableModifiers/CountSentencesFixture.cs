using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class CountSentencesFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReturnTheNumberOfSentences()
        {
            const string input = @"{$article|count_sentences}";

            const string article = @"   War Dims Hope for Peace. Child's Death Ruins Couple's Holiday.
                                        Man is Fatally Slain. Death Causes Loneliness, Feeling of Isolation.";

            viewData.Add("article", article);
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("4", result);
        }
    }
}
