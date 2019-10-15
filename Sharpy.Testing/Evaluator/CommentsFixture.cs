﻿using NUnit.Framework;

namespace Sharpy.Testing.Evaluator
{
    public class CommentsFixture : EvaluatorFixture
    {
        [Test]
        public void ShouldNotDisplayCommentsInOutput()
        {
            const string input = @"{* I'm a Sharpy comment, I don't exist in the compiled output *}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void ShouldNotDisplayMultiLineCommentsInOutput()
        {
            const string input = @"{* this multiline sharpy
                                      comment is
                                      not sent to the browser *}";

            var result = functionEvaluator.Evaluate(input);

            Assert.AreEqual(0, result.Length);
        }
    }
}
