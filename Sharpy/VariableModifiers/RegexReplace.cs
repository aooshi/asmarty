using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Sharpy.ViewEngine;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class RegexReplace : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new RequiredParameterException(string.Format("The function {0} requires 2 parameters, but {1} parameters were supplied", "regex_replace", parameters.Length));
            }
            else
            {
                return input == null ? null : Regex.Replace(input.ToString(), (string)parameters[0], (string)parameters[1]);
            }
        }

        public string Name
        {
            get { return "regex_replace"; }
        }
    }
}
