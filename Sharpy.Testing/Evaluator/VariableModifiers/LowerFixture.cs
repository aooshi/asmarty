using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class LowerFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldConvertVariableToLowerCase()
        {
            const string input = @"{$name|lower}";

            const string output = @"fred";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
