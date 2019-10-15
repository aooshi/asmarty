using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class CombiningModifiersFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldAllowCombiningMultipleModifiers()
        {
            const string input = "{$article|lower|spacify|truncate:30:\". . .\"}";

            viewData.Add("article", "Smokers are Productive, but Death Cuts Efficiency.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("s m o k e r s   a r e   p. . .", result);
        }

        [Test]
        public void ShouldAllowModifierAfterModifierWithParameters()
        {
            const string input = "{'This input has been wrapped on word boundaries'|wordwrap:15|nl2br}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual("This input has<br />been wrapped on<br />word boundaries", result);
        }
    }
}
