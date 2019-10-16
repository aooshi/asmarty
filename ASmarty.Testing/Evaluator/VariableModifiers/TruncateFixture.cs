using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class TruncateFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldTruncateToLengthOf80()
        {
            const string input = @"{$name|truncate}";

            viewData.Add("name", "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(80, result.Length);
        }

        [Test]
        public void ShouldTruncateLengthAccordingToSpecifiedParameter()
        {
            const string input = @"{$name|truncate:30}";

            viewData.Add("name", "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(30, result.Length);
        }

        [Test]
        public void ShouldTruncateOnWordBoundaryAndEndStatementInEllipses()
        {
            const string input = @"{$name|truncate:30}";

            const string output = @"Two Sisters Reunite after...";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldTruncateAndUseEmptyTrailingStatement()
        {
            const string input = @"{$name|truncate:30:''}";

            const string output = @"Two Sisters Reunite after";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldTruncateAndUseSpecifiedTrailingStatement()
        {
            const string input = @"{$name|truncate:30:'---'}";

            const string output = @"Two Sisters Reunite after---";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldTruncateUsingEmptyTrailingStatementAndIgnoreWordBoundary()
        {
            const string input = @"{$name|truncate:30:'':true}";

            const string output = @"Two Sisters Reunite after Eigh";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldTruncateUsingSpecifiedTrailingStatementAndIgnoreWordBoundary()
        {
            const string input = @"{$name|truncate:30:'...':true}";

            const string output = @"Two Sisters Reunite after E...";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldTruncateInTheMiddleOfTheWordUsingSpecifiedTrailingStatement()
        {
            const string input = @"{$name|truncate:30:'..':true:true}";

            const string output = @"Two Sisters Re..ckout Counter.";

            viewData.Add("name", "Two Sisters Reunite after Eighteen Years at Checkout Counter.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
