using NUnit.Framework;
using Rhino.Mocks;
using ASmarty.VariableModifiers;
using ASmarty.ViewEngine;

namespace ASmarty.Testing.VariableModifiers
{
    [TestFixture]
    public class EscapeFixture
    {
        [Test]
        public void ShouldEncodeVariableValue()
        {
            var evaluator = MockRepository.GenerateMock<IEvaluator>();

            evaluator.Stub(x => x.Encode("unencoded")).Return("encoded");

            var result = new Escape().Evaluate("unencoded", evaluator);

            Assert.AreEqual("encoded", result);
        }
    }
}
