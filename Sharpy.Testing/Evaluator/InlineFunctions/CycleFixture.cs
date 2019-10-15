using System.Collections.Generic;
using NUnit.Framework;

namespace ASmarty.Testing.Evaluator.InlineFunctions
{
    public class CycleFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldCycleBetweenSpecifiedValues()
        {
            const string input = @" <ul>
                                    {foreach from=$list item='int'}
		                                <li bgcolor='{cycle values='#eeeeee,#d0d0d0'}'>{$int}</li>
                                    {/foreach}
                                    </ul>";

            const string output = @"<ul>
                                        <li bgcolor='#eeeeee'>1</li>
                                        <li bgcolor='#d0d0d0'>2</li>
                                        <li bgcolor='#eeeeee'>3</li>
                                    </ul>";

            viewData.Add("list", new List<int> { 1, 2, 3 });
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldResetCycleForEachNewCycleTag()
        {
            const string input = @" <ul>
                                    {foreach from=$list item='int'}
		                                <li bgcolor='{cycle values='#eeeeee,#d0d0d0'}'>{$int}</li>
                                    {/foreach}
                                    </ul>
                                    <ul>
                                    {foreach from=$list item='int'}
		                                <li bgcolor='{cycle values='#eeeeee,#d0d0d0'}'>{$int}</li>
                                    {/foreach}
                                    </ul>";

            const string output = @"<ul>
                                        <li bgcolor='#eeeeee'>1</li>
                                        <li bgcolor='#d0d0d0'>2</li>
                                        <li bgcolor='#eeeeee'>3</li>
                                    </ul>
                                    <ul>
                                        <li bgcolor='#eeeeee'>1</li>
                                        <li bgcolor='#d0d0d0'>2</li>
                                        <li bgcolor='#eeeeee'>3</li>
                                    </ul>";

            viewData.Add("list", new List<int> { 1, 2, 3 });
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}
