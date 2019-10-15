using NUnit.Framework;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class ReplaceFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReplaceStringWithSpecifiedParameter()
        {
            const string input = @"{$name|replace:'Spain':'Hungary'}";

            const string output = @"The rain in Hungary does not fall mainly on the plain.";

            viewData.Add("name", "The rain in Spain does not fall mainly on the plain.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        [ExpectedException(typeof(RequiredParameterException))]
        public void ShouldThrowExceptionIfCorrectNumberOfParametersAreNotSupplied()
        {
            const string input = @"{$name|replace}";

            viewData.Add("name", "The rain in Spain does not fall mainly on the plain.");
            functionEvaluator.Evaluate(input);
        }

    }
}
