using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.VariableModifiers
{
    public class StripTagsFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldStripTags()
        {
            const string input = @"{$name|strip_tags}";

            const string output = @"Blind Woman Gets  New Kidney  from Dad she Hasn't Seen in  years .";

            viewData.Add("name", "Blind Woman Gets <font face=\"helvetica\">New Kidney</font> from Dad she Hasn't Seen in <b>years</b>.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }

        [Test]
        public void ShouldReplaceTagsWithEmptyStringWhenSpecified()
        {
            const string input = @"{$name|strip_tags:false}";

            const string output = @"Blind Woman Gets New Kidney from Dad she Hasn't Seen in years.";

            viewData.Add("name", "Blind Woman Gets <font face=\"helvetica\">New Kidney</font> from Dad she Hasn't Seen in <b>years</b>.");
            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
