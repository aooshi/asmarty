using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.BlockFunctions
{
    public class StripFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateLiteralStatement()
        {
            const string input =  @"{strip}
                                    <html>
                                        <ul>
		                                    <li>One - 1</li>
		                                    <li>Two - 2</li>
		                                    <li>Three - 3</li>
                                        </ul>
                                    </html>
                                    {/strip}";

            const string output = @"<html><ul><li>One - 1</li><li>Two - 2</li><li>Three - 3</li></ul></html>";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(output, result);
        }
    }
}
