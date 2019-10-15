using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.InlineFunctions
{
    public class AssignFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldAssignValueToSpecifiedVariable()
        {
            const string input = @"{assign var='name' value='Bob'}";

            var result = functionEvaluator.Evaluate(input);

            Assert.IsTrue(string.IsNullOrEmpty(result));
            Assert.AreEqual("Bob", functionEvaluator.LocalData["name"]);
        }
    }
}
