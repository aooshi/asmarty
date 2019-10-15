using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.InlineFunctions
{
    public class RDelimFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateToSingleBracket()
        {
            const string input = @"<p>{rdelim} is a closing bracket<p>";
            const string output = @"<p>} is a closing bracket<p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}
