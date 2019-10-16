using System;
using System.Collections.Generic;
using NUnit.Framework;
using ASmarty.BlockFunctions;
using ASmarty.ExpressionFunctions;
using ASmarty.InlineFunctions;
using ASmarty.VariableModifiers;
using ASmarty.ViewEngine;
using Strip=ASmarty.BlockFunctions.Strip;

namespace ASmarty.Testing.Evaluator
{
    [TestFixture]
    public abstract class EvaluatorFixture
    {
        protected IDictionary<string, object> viewData;
        protected IDictionary<string, object> functionData;
        protected IEvaluator evaluator;
        protected IFunctionEvaluator functionEvaluator;

        [SetUp]
        public void Setup()
        {
            var blockFunctions = new IBlockFunction[] { new ForEach(), new Literal(), new Strip(), new Capture() };
            var inlineFunctions = new IInlineFunction[] { new LDelim(), new RDelim(), new Assign(), new Cycle() };
            var expressionFunctions = new IExpressionFunction[] { new If() };
            var variableModifiers = new IVariableModifier[] { new Capitalize(), new Cat(), new CountCharacters(), new CountParagraphs(), new CountSentences(), new CountWords(), new DateFormat(), new Default(), new Lower(), new NewLineToBreak(), new RegexReplace(), new Replace(), new Spacify(), new StringFormat(), new ASmarty.VariableModifiers.Strip(), new StripTags(), new Truncate(), new Upper(), new WordWrap(), new Indent() };
            var functions = new Functions(blockFunctions, inlineFunctions, expressionFunctions, variableModifiers);

            viewData = new Dictionary<string, object>();
            functionData = new Dictionary<string, object>();

            var internalEvaluator = new InternalEvaluator(null, null, functions);
            evaluator = new ViewEngine.Evaluator(internalEvaluator, 0);
            functionEvaluator = new FunctionEvaluator(internalEvaluator, 0, functionData);
        }

        protected static void AssertHtmlEqual(string expected, string actual)
        {
            Assert.AreEqual(expected.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""), actual.Replace(" ", "").Replace(Environment.NewLine, "").Replace("\t", ""));
        }
    }
}
