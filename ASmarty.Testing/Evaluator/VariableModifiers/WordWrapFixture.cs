using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class WordWrapFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldWrapInputOnWordBoundariesAndSpecifiedLength()
        {
            const string input = @"{$name|wordwrap:30}";

            viewData.Add("name", "Blind woman gets new kidney from dad she hasn't seen in years.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("Blind woman gets new kidney", splitResults[0]);
            Assert.AreEqual("from dad she hasn't seen in", splitResults[1]);
            Assert.AreEqual("years.", splitResults[2]);
        }

        [Test]
        public void ShouldWrapInputOnWordBoundariesAndLengthOf20()
        {
            const string input = @"{$name|wordwrap:20}";

            viewData.Add("name", "Blind woman gets new kidney from dad she hasn't seen in years.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("Blind woman gets new", splitResults[0]);
            Assert.AreEqual("kidney from dad she", splitResults[1]);
            Assert.AreEqual("hasn't seen in", splitResults[2]);
            Assert.AreEqual("years.", splitResults[3]);
        }

        [Test]
        public void ShouldWrapInputOnWordBoundariesAndUseSpecifiedDelimiter()
        {
            const string input = "{$name|wordwrap:30:'<br />\n'}";

            viewData.Add("name", "Blind woman gets new kidney from dad she hasn't seen in years.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("Blind woman gets new kidney<br />", splitResults[0]);
            Assert.AreEqual("from dad she hasn't seen in<br />", splitResults[1]);
            Assert.AreEqual("years.", splitResults[2]);
        }

        [Test]
        public void ShouldWrapInputAndIgnoreWordBoundaries()
        {
            const string input = "{$name|wordwrap:26:'\n':true}";

            viewData.Add("name", "Blind woman gets new kidney from dad she hasn't seen in years.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("Blind woman gets new kidne", splitResults[0]);
            Assert.AreEqual("y from dad she hasn't seen", splitResults[1]);
            Assert.AreEqual("in years.", splitResults[2]);
        }
    }
}
