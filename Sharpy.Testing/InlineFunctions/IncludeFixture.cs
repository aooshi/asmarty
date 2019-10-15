using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using ASmarty.InlineFunctions;
using ASmarty.ViewEngine;

namespace ASmarty.Testing.InlineFunctions
{
    [TestFixture]
    public class IncludeFixture
    {
        private IDictionary<string, object> localData;
        private IFunctionEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            localData = new Dictionary<string, object>();

            evaluator = MockRepository.GenerateMock<IFunctionEvaluator>();
            evaluator.Stub(x => x.LocalData).Return(localData);
        }

        [Test]
        public void ShouldFetchAndReturnEvaluatedTemplate()
        {
            evaluator.Stub(x => x.EvaluateTemplate("~/Folder/Path.ext")).Return("the output");

            var attributes = new Dictionary<string, object> { { "file", "~/Folder/Path.ext" } };

            var output = new Include().Evaluate(attributes, evaluator);

            Assert.AreEqual("the output", output);
        }

        [Test]
        public void ShouldAssignEvaluatedTemplateToVariable()
        {
            evaluator.Stub(x => x.EvaluateTemplate("~/Folder/Path.ext")).Return("the output");

            var attributes = new Dictionary<string, object> { { "file", "~/Folder/Path.ext" } };
            attributes.Add("assign", "result");

            var output = new Include().Evaluate(attributes, evaluator);

            Assert.IsNull(output);
            Assert.AreEqual("the output", localData["result"]);
        }
    }
}
