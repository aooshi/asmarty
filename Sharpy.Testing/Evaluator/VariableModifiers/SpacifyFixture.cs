using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class SpacifyFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldInsertSpaceBetweenEveryCharacter()
        {
            const string input = @"{$articleTitle|spacify}";

            const string output = @"n e x t   x - m e n   f i l m ,   x 3 ,   d e l a y e d";

            viewData.Add("articleTitle", "next x-men film, x3, delayed");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldUseSpecifiedParameterInsteadOfSpace()
        {
            const string input = @"{$articleTitle|spacify:'^^'}";

            const string output = @"n^^e^^x^^t^^ ^^x^^-^^m^^e^^n^^ ^^f^^i^^l^^m^^,^^ ^^x^^3^^,^^ ^^d^^e^^l^^a^^y^^e^^d";

            viewData.Add("articleTitle", "next x-men film, x3, delayed");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
