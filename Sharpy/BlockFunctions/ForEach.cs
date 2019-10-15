using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;
using Sharpy.Extensions;

namespace Sharpy.BlockFunctions
{
    [Export(typeof(IBlockFunction))]
    internal class ForEach : IBlockFunction
    {
        private const string foreachElse = "foreachelse";

        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content)
        {
            var from = attributes.GetRequiredAttribute<IEnumerable>("from");
            var item = attributes.GetRequiredAttribute<string>("item");

            return EvaluateContent(from, item, evaluator, content);
        }

        public IList<string> TagsToIgnore()
        {
            return new List<string> { foreachElse };
        }

        private static string EvaluateContent(IEnumerable from, string item, IEvaluator evaluator, string content)
        {
            var output = new StringBuilder();
            var match = Regex.Match(content, string.Format("{{{0}}}", foreachElse));

            var innerContent = content;
            if (match.Success)
            {
                innerContent = content.Substring(0, match.Index);
            }

            var isNullOrEmpty = true;
            if (from != null)
            {
                foreach (var obj in from)
                {
                    evaluator.LocalData[item] = obj;
                    output.Append(evaluator.Evaluate(innerContent));
                    evaluator.LocalData.Remove(item);

                    isNullOrEmpty = false;
                }
            }

            if (isNullOrEmpty && match.Success)
            {
                var elseContent = content.Substring(match.Index + match.Length);
                output.Append(evaluator.Evaluate(elseContent));
            }

            return output.ToString();
        }

        public string Name
        {
            get { return "foreach"; }
        }
    }
}