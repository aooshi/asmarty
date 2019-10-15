using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class CountCharactersFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReturnTheNumberOfCharacters()
        {
            const string input = @"{$name|count_characters}";

            viewData.Add("name", "fred");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("4", result);
        }

        [Test]
        public void ShouldNotIncludeWhitespaceCharactersInTheCountByDefault()
        {
            const string input = @"{$name|count_characters}";

            viewData.Add("name", "  fred  ");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("4", result);
        }

        [Test]
        public void ShouldIncludeWhitespaceCharactersInTheCount()
        {
            const string input = @"{$name|count_characters:true}";

            viewData.Add("name", "  fred  ");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("8", result);

        }
    }
}
