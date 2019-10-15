using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;

namespace Sharpy.ExpressionFunctions
{
    [Export(typeof(IExpressionFunction))]
    internal class If : IExpressionFunction
    {
        public string Evaluate(string functionDetails, IFunctionEvaluator evaluator, string content)
        {
            Match elseIfMatch = null;
            Match elseMatch = null;

            var depth = 0;
            foreach (Match match in Regex.Matches(content, @"{elseif .*}|{else}|{if .*}|{/if}"))
            {
                if (Regex.IsMatch(match.Value, @"{if .*}"))
                {
                    depth++;
                }
                else if (Regex.IsMatch(match.Value, @"{/if}"))
                {
                    depth--;
                }
                else
                {
                    if (depth == 0)
                    {
                        if (elseIfMatch == null && Regex.IsMatch(match.Value, @"{elseif .*}"))
                        {
                            elseIfMatch = match;
                        }
                        else if (elseMatch == null && Regex.IsMatch(match.Value, @"{else}"))
                        {
                            elseMatch = match;
                        }
                    }
                }
            }

            var result = (bool)evaluator.EvaluateExpression(functionDetails);
            if (result)
            {
                if (elseIfMatch != null)
                {
                    return evaluator.Evaluate(content.Substring(0, elseIfMatch.Index));
                }
                else if (elseMatch != null)
                {
                    return evaluator.Evaluate(content.Substring(0, elseMatch.Index));
                }
                else
                {
                    return evaluator.Evaluate(content);    
                }
            }
            else
            {
                if (elseIfMatch != null)
                {
                    var newDetails = elseIfMatch.Value.Substring(8, elseIfMatch.Length - 9);
                    var newContent = content.Substring(elseIfMatch.Index + elseIfMatch.Length);

                    return Evaluate(newDetails, evaluator, newContent);
                }
                else if (elseMatch != null)
                {
                    return evaluator.Evaluate(content.Substring(elseMatch.Index + elseMatch.Length));
                }
                else
                {
                    return string.Empty;    
                }
            }
        }

        public IList<string> TagsToIgnore()
        {
            return new List<string> { "else", "elseif" };
        }

        public string Name
        {
            get { return "if"; }
        }
    }
}