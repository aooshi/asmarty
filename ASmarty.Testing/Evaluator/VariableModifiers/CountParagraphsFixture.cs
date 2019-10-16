using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class CountParagraphsFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReturnTheNumberOfParagraphs()
        {
            const string input = @"{$article|count_paragraphs}";

            const string article = @"   War Dims Hope for Peace. Child's Death Ruins Couple's Holiday.

                                        Man is Fatally Slain. Death Causes Loneliness, Feeling of Isolation.";

            viewData.Add("article", article);
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("2", result);
        }
    }
}
