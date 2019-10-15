using System.Collections.Generic;
using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.BlockFunctions
{
    public class ForEachFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluateForEachStatement()
        {
            const string input = @" <html>
                                        <ul>
                                        {foreach from=$list item='int'}
		                                    <li>{$int}</li>
                                        {/foreach}
                                        </ul>
                                    </html>";

            const string output = @"<html>
                                        <ul>
                                            <li>2</li>
                                            <li>4</li>
                                            <li>6</li>
                                        </ul>
                                    </html>";

            viewData.Add("list", new List<int> { 2, 4, 6 });
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNestedForEachStatements()
        {
            const string input = @" <html>
                                        <ul>
                                        {foreach from=$people item='person'}
                                            {foreach from=$person.Items item='item'}
		                                    <li>{$person.Name} - {$item}</li>
                                            {/foreach}
                                        {/foreach}
                                        </ul>
                                    </html>";

            const string output = @"<html>
                                        <ul>
                                            <li>Bob - 1</li>
                                            <li>Bob - 2</li>
                                            <li>Bob - 3</li>
                                            <li>Dave - 4</li>
                                            <li>Dave - 5</li>
                                            <li>Dave - 6</li>
                                        </ul>
                                    </html>";

            var bob = new { Name = "Bob", Items = new List<int> { 1, 2, 3 } };
            var dave = new { Name = "Dave", Items = new List<int> { 4, 5, 6 } };
            viewData.Add("people", new [] { bob, dave });
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateForEachElseStatement()
        {
            const string input = @" <html>
                                        {foreach from=$list item='int'}
		                                    <a>{$int}</a>
                                        {foreachelse}
                                            <p>No items were found in the search</p>
                                        {/foreach}
                                    </html>";

            const string output = @"<html>
                                        <p>No items were found in the search</p>
                                    </html>";

            viewData.Add("list", new List<int>());
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}