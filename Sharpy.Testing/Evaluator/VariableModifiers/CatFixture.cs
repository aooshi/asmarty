using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class CatFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldConcatenateSpecifiedValue()
        {
            const string input = @"{$articleTitle|cat:' yesterday.'}";

            const string output = @"Physics predict world didn't end yesterday.";

            viewData.Add("articleTitle", "Physics predict world didn't end");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldReturnOriginalObjectIfNoValueIsSpecified()
        {
            const string input = @"{$articleTitle|cat}";

            const string output = @"Original Text";

            viewData.Add("articleTitle", "Original Text");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
