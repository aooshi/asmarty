using NUnit.Framework;
using Rhino.Mocks;
using Sharpy.VariableModifiers;
using Sharpy.ViewEngine;

namespace Sharpy.Testing.VariableModifiers
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
