using NUnit.Framework;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class RegexReplaceFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldReplaceRegularExpressionWithSuppliedParameter()
        {
            const string input = @"{$name|regex_replace:'[\r\t\n]':' '}";

            const string output = @"Infertility unlikely to be passed on, experts say.";

            viewData.Add("name", "Infertility unlikely to\nbe passed on, experts say.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        [ExpectedException(typeof(RequiredParameterException))]
        public void ShouldThrowExceptionIfCorrectNumberOfParametersAreNotSupplied()
        {
            const string input = @"{$name|regex_replace:'[\r\t\n]'}";

            viewData.Add("name", "Infertility unlikely to\nbe passed on, experts say.");
            functionEvaluator.Evaluate(input);
        }
    }
}
