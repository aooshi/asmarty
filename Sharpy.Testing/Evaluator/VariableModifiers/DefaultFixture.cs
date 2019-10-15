using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class DefaultFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldUseParameterIfVariableIsNull()
        {
            const string input = @"{$name|default:'No name was supplied'}";

            const string output = @"No name was supplied";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldIgnoreParameterIfVariableIsNotNull()
        {
            const string input = @"{$name|default:'No name was supplied'}";

            const string output = @"bob";

            viewData["name"] = "bob";
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldUseOriginalVariableIfNoParameterIsSupplied()
        {
            const string input = @"{$name|default}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldUseParameterIfVariableIsAnEmptyString()
        {
            const string input = @"{$name|default:'No name was supplied'}";

            const string output = @"No name was supplied";

            viewData["name"] = string.Empty;
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

    }
}
