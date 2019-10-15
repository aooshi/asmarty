using System.Collections.Generic;
using System.ComponentModel.Composition;
using ASmarty.ViewEngine;
using ASmarty.Extensions;

namespace ASmarty.BlockFunctions
{
    [Export(typeof(IBlockFunction))]
    public class Capture : IBlockFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content)
        {
            var name = attributes.GetRequiredAttribute<string>("name");

            evaluator.LocalData[name] = evaluator.Evaluate(content);

            return null;
        }

        public IList<string> TagsToIgnore()
        {
            return null;
        }

        public string Name
        {
            get { return "capture"; }
        }
    }
}
