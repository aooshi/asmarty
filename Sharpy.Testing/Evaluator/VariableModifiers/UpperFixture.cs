using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class UpperFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldConvertVariableToUpperCase()
        {
            const string input = @"{$name|upper}";

            const string output = @"FRED";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
