using System.ComponentModel.Composition;
using Sharpy.ViewEngine;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Replace : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new RequiredParameterException(string.Format("The function {0} requires 2 parameters, but {1} parameters were supplied", "replace", parameters.Length));
            }
            else
            {
                return input == null ? null : input.ToString().Replace((string)parameters[0], (string)parameters[1]);    
            }
        }

        public string Name
        {
            get { return "replace"; }
        }
    }
}
