using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.InlineFunctions
{
    public class LDelimFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateToSingleBracket()
        {
            const string input = @"<p>{ldelim} is an opening bracket<p>";
            const string output = @"<p>{ is an opening bracket<p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}
