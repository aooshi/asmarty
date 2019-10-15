using System;
using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class DateFormatFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldFormatDateTimeVariable()
        {
            const string input = @"{$date|date_format:'dd MMM yyyy'}";

            const string output = @"12 Mar 1999";

            viewData.Add("date", new DateTime(1999, 3, 12));
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldFormatDateTimeVariableWithDefaultFormatIfNoneIsSpecified()
        {
            const string input = @"{$date|date_format}";

            const string output = @"Mar 12, 1999";

            viewData.Add("date", new DateTime(1999, 3, 12));
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldUseSecondParameterIfNoDateIsSupplied()
        {
            const string input = @"{$date|date_format:'dd MMM yyyy':'1 Jan 2000'}";

            const string output = @"1 Jan 2000";

            viewData.Add("date", null);
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);

        }
    }
}
