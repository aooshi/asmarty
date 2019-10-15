using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.BlockFunctions
{
    public class LiteralFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateLiteralStatement()
        {
            const string input = @" <html>
                                        {literal}
                                        <ul>
                                        {foreach from=$list item=int}
		                                    <li>{$int}</li>
                                        {/foreach}
                                        </ul>
                                        {/literal}
                                    </html>";

            const string output = @" <html>
                                        <ul>
                                        {foreach from=$list item=int}
		                                    <li>{$int}</li>
                                        {/foreach}
                                        </ul>
                                    </html>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}