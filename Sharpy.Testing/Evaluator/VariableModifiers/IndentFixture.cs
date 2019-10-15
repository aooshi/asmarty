using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.VariableModifiers
{
    public class IndentFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldIndentToFourSpacesByDefault()
        {
            const string input = "{$article|indent}";

            viewData.Add("article", "NJ judge to rule on nude beach.\nSun or rain expected today, dark tonight.\nStatistics show that teen pregnancy drops off significantly after 25.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("    NJ judge to rule on nude beach.", splitResults[0]);
            Assert.AreEqual("    Sun or rain expected today, dark tonight.", splitResults[1]);
            Assert.AreEqual("    Statistics show that teen pregnancy drops off significantly after 25.", splitResults[2]);
        }

        [Test]
        public void ShouldIndentToTenSpaces()
        {
            const string input = "{$article|indent:10}";

            viewData.Add("article", "NJ judge to rule on nude beach.\nSun or rain expected today, dark tonight.\nStatistics show that teen pregnancy drops off significantly after 25.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            Assert.AreEqual("          NJ judge to rule on nude beach.", splitResults[0]);
            Assert.AreEqual("          Sun or rain expected today, dark tonight.", splitResults[1]);
            Assert.AreEqual("          Statistics show that teen pregnancy drops off significantly after 25.", splitResults[2]);
        }

        [Test]
        public void ShouldIndentUsingASingleTab()
        {
            const string input = "{$article|indent:1:'\t'}";

            viewData.Add("article", "NJ judge to rule on nude beach.\nSun or rain expected today, dark tonight.\nStatistics show that teen pregnancy drops off significantly after 25.");
            var result = functionEvaluator.Evaluate(input);

            var splitResults = result.Split('\n');

            foreach (var line in splitResults)
            {
                Assert.IsTrue(line.StartsWith("\t"));
            }
        }
    }
}
