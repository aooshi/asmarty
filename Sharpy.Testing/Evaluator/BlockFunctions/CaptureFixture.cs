using System.Collections.Generic;
using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.BlockFunctions
{
    public class CaptureFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldAssignContentToVariable()
        {
            const string input =  @"{capture name='captured'}
                                    <html>
                                        <ul>
                                            <li>One - 1</li>
                                            <li>Two - 2</li>
                                            <li>Three - 3</li>
                                        </ul>
                                    </html>
                                    {/capture}";

            const string variable = @"<html>
                                        <ul>
                                            <li>One - 1</li>
                                            <li>Two - 2</li>
                                            <li>Three - 3</li>
                                        </ul>
                                      </html>";

            var result = functionEvaluator.Evaluate(input);

            Assert.IsTrue(string.IsNullOrEmpty(result));
            AssertHtmlEqual(variable, functionEvaluator.LocalData["captured"] as string);
        }

        [Test]
        public void ContentShouldBeEvaluated()
        {
            const string input = @"{capture name='captured'}
                                    <html>
                                        <ul>
                                            {foreach from=$list item='index'}
                                            <li>{$index}</li>
                                            {/foreach}
                                        </ul>
                                    </html>
                                    {/capture}";

            const string variable = @"<html>
                                        <ul>
                                            <li>1</li>
                                            <li>2</li>
                                            <li>3</li>
                                        </ul>
                                      </html>";

            var list = new List<int> { 1, 2, 3 };

            viewData["list"] = list;

            var result = functionEvaluator.Evaluate(input);

            Assert.IsTrue(string.IsNullOrEmpty(result));
            AssertHtmlEqual(variable, functionEvaluator.LocalData["captured"] as string);
        }
    }
}
