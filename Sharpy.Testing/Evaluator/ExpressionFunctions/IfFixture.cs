using NUnit.Framework;

namespace Sharpy.Testing.Evaluator.ExpressionFunctions
{
    public class IfFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldEvaluatePositiveEqualsStatement()
        {
            const string input = @" {if $name eq 'Fred'}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"<p>Hello Fred!</p>";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNegativeEqualsStatement()
        {
            const string input = @" {if $name eq 'Fred'}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            viewData.Add("name", "Bob");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateEqualsStatementUsingEqualsSymbol()
        {
            const string input = @" {if $name == 'Fred'}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"<p>Hello Fred!</p>";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNotEqualsStatement()
        {
            const string input = @" {if $name ne 'Fred'}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNegativeNotEqualsStatement()
        {
            const string input = @" {if $name neq 'Fred'}
                                        <p>Hello Not Fred!</p>
                                    {/if}";

            const string output = @"<p>Hello Not Fred!</p>";

            viewData.Add("name", "Bob");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNotEqualsStatementUsingSymbol()
        {
            const string input = @" {if $name != 'Fred'}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            viewData.Add("name", "Fred");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateGreaterThanStatement()
        {
            const string input = @" {if 15 gt 12}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"<p>This is logic</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateGreaterThanStatementUsingSymbol()
        {
            const string input = @" {if 15 > 12}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"<p>This is logic</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateLesserThanStatement()
        {
            const string input = @" {if 15 lt 12}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateLesserThanStatementUsingSymbol()
        {
            const string input = @" {if 15 < 12}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateGreaterThanOrEqualsStatement()
        {
            const string input = @" {if 15 gte 15}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"<p>This is logic</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNegativeGreaterThanOrEqualsStatement()
        {
            const string input = @" {if 15 ge 26}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateGreaterThanOrEqualsStatementUsingSymbol()
        {
            const string input = @" {if 15 >= 25}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateLesserThanOrEqualsStatement()
        {
            const string input = @" {if 15 lte 15}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"<p>This is logic</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNegativeLesserThanOrEqualsStatement()
        {
            const string input = @" {if 15 le 0}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateLesserThanOrEqualsStatementUsingSymbol()
        {
            const string input = @" {if 15 <= 19}
                                        <p>This is logic</p>
                                    {/if}";

            const string output = @"<p>This is logic</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNotStatement()
        {
            const string input = @" {if not $trueVariable}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            viewData.Add("trueVariable", true);
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNotStatementUsingSymbol()
        {
            const string input = @" {if !$trueVariable}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            viewData.Add("trueVariable", true);
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateModStatement()
        {
            const string input = @" {if 5 mod 4 eq 1}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"<p>Hello Fred!</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateModStatementUsingSymbol()
        {
            const string input = @" {if 5 % 4 eq 3}
                                        <p>Hello Fred!</p>
                                    {/if}";

            const string output = @"";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateElseBlock()
        {
            const string input = @" {if 5 % 4 eq 3}
                                        <p>Result if true</p>
                                    {else}
                                        <p>Result if false</p>
                                    {/if}";

            const string output = @"<p>Result if false</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldIgnoreElseBlock()
        {
            const string input = @" {if 5 % 4 eq 1}
                                        <p>Result if true</p>
                                    {else}
                                        <p>Result if false</p>
                                    {/if}";

            const string output = @"<p>Result if true</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateIfElseBlock()
        {
            const string input = @" {if 15 > 25}
                                        <p>15 is greater than 25!</p>
                                    {elseif 5 > 2}
                                        <p>5 is greater than 2</p>
                                    {else}
                                        <p>Mathematics has failed us</p>
                                    {/if}";

            const string output = @"<p>5 is greater than 2</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateMultipleIfElseBlocks()
        {
            const string input = @" {if 15 > 25}
                                        <p>15 is greater than 25!</p>
                                    {elseif 5 > 15}
                                        <p>5 is greater than 15!</p>
                                    {elseif 5 > 0}
                                        <p>5 is greater than 0</p>
                                    {else}
                                        <p>Mathematics has failed us</p>
                                    {/if}";

            const string output = @"<p>5 is greater than 0</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNestedIfStatements()
        {
            const string input = @" {if 10 > 5}
                                        {if 25 > 5}
                                            <p>The expected result</p>
                                        {/if}
                                    {/if}";

            const string output = @"<p>The expected result</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNestedIfElseStatement()
        {
            const string input = @" {if 10 > 5}
                                        {if 5 > 25}
                                            <p>This should not happen</p>
                                        {else}
                                            <p>The expected result</p>
                                        {/if}
                                    {/if}";

            const string output = @"<p>The expected result</p>";

            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }

        [Test]
        public void ShouldEvaluateNestedIfStatementWithVariables()
        {
            const string input = @" {if $error ne null}
			                            <tr>
				                            <td bgcolor='yellow' colspan='2'>
					                            {if $error eq 'name_empty'}You must supply a name.
					                            {elseif $error eq 'comment_empty'} You must supply a comment.
					                            {/if}
				                            </td>
			                            </tr>
		                            {/if}";

            const string output = @"<tr>
                                        <td bgcolor='yellow' colspan='2'>
                                            You must supply a name.
                                        </td>
                                    </tr>";

            viewData.Add("error", "name_empty");
            var result = functionEvaluator.Evaluate(input);

            AssertHtmlEqual(output, result);
        }
    }
}