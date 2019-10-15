using System.ComponentModel.Composition;
using ASmarty.ViewEngine;
using ASmarty.ViewEngine.Exceptions;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class StringFormat : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new RequiredParameterException(string.Format("The function {0} requires 1 parameter, but {1} parameters were supplied", "string_format", parameters.Length));
            }
            else
            {
                return string.Format((string)parameters[0], input);    
            }
        }

        public string Name
        {
            get { return "string_format"; }
        }
    }
}
