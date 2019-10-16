using System.Collections.Generic;
using System.ComponentModel.Composition;
using ASmarty.Extensions;
using ASmarty.ViewEngine;

namespace ASmarty.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    public class Cycle : IInlineFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            var valuesAttribute = attributes.GetRequiredAttribute<string>("values");
            const string name = "default";

            var slotName = GetType().FullName + "_" + name;
            if (!evaluator.FunctionData.ContainsKey(slotName))
            {
                var splitAttributes = valuesAttribute.Split(',');
                var cycleValue = new CycleValue(splitAttributes);
                evaluator.FunctionData[slotName] = cycleValue;
            }

            return ((CycleValue)evaluator.FunctionData[slotName]).NextValue();
        }

        private class CycleValue
        {
            private int nextIndex;
            private readonly string[] values;

            public CycleValue(string[] values)
            {
                this.values = values;
            }

            public string NextValue()
            {
                var value = values[nextIndex];

                nextIndex++;
                if (nextIndex >= values.Length)
                {
                    nextIndex = 0;
                }

                return value;
            }
        }

        public string Name
        {
            get { return "cycle"; }
        }
    }
}
