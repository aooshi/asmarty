using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Truncate : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }

            // Set defaults and check parameters
            var truncateLength = 80;
            if (parameters.Length >= 1)
            {
                truncateLength = (int) parameters[0];
            }

            var truncateIndicator = "...";
            if (parameters.Length >= 2)
            {
                truncateIndicator = (string)parameters[1];
            }

            var ignoreWordBoundaries = false;
            if (parameters.Length >= 3)
            {
                ignoreWordBoundaries = (bool)parameters[2];
            }

            var truncateInMiddle = false;
            if (parameters.Length == 4)
            {
                truncateInMiddle = (bool)parameters[3];
            }

            if (truncateLength == 0)
            {
                return string.Empty;
            }

            var truncated = input.ToString();
            if (truncated.Length > truncateLength)
            {
                truncateLength = truncateLength - Math.Min(truncateLength, truncateIndicator.Length);
                if (!ignoreWordBoundaries && !truncateInMiddle)
                {
                    truncated = Regex.Replace(truncated.Substring(0, truncateLength + 1), @"\s+?(\S+)?$", string.Empty);
                }

                if (!truncateInMiddle)
                {
                    return truncated.Substring(0, Math.Min(truncateLength, truncated.Length)) + truncateIndicator;
                }
                else
                {
                    return truncated.Substring(0, truncateLength / 2) + truncateIndicator + truncated.Substring(truncated.Length - (truncateLength / 2));
                }
            }
            else
            {
                return truncated;
            }
        }

        public string Name
        {
            get { return "truncate"; }
        }
    }
}
