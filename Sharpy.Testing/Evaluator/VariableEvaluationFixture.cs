using NUnit.Framework;

namespace Sharpy.Testing.Evaluator
{
    public class VariableEvaluationFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateStringVariable()
        {
            const string input = @"{'LOWER'|lower}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("lower", result);
        }

        [Test]
        public void ShouldEvaluateStringContainingDollarSign()
        {
            const string input = @"{'$5.00'}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("$5.00", result);
        }

        [Test]
        public void ShouldEvaluateBackTicksExpression()
        {
            const string input = @"{`10 > 5 ? 'expected' : 'unexpected'`}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("expected", result);
        }

        [Test]
        public void ShouldEvaluateBackTicksExpressionAndApplyModifier()
        {
            const string input = @"{`10 > 5 ? 'expected' : 'unexpected'`|upper}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("EXPECTED", result);
        }
    }
}
