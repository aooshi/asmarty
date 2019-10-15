using NUnit.Framework;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class StringFormatFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldFormatDecimalUsingSpecifiedFormat()
        {
            const string input = @"{$number|string_format:'{0:n3}'}";

            const string output = @"23.579";

            viewData.Add("number", 23.5787446m);
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldFormatStringUsingSpecifiedFormat()
        {
            const string input = @"{$string|string_format:'->{0,10}<-'}";

            const string output = @"->     Hello<-";

            viewData.Add("string", "Hello");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        [ExpectedException(typeof(RequiredParameterException))]
        public void ShouldThrowExceptionIfCorrectNumberOfParametersAreNotSupplied()
        {
            const string input = @"{$name|string_format}";

            viewData.Add("name", "value");
            functionEvaluator.Evaluate(input);
        }
    }
}
