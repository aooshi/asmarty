using System;
using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class DateFormat : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                if (parameters.Length == 2)
                {
                    return parameters[1];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var inputAsDate = (DateTime)input;
                var dateFormat = "MMM dd, yyyy";
                if (parameters.Length >= 1)
                {
                    dateFormat = (string) parameters[0];
                }
                return inputAsDate.ToString(dateFormat);    
            }
        }

        public string Name
        {
            get { return "date_format"; }
        }
    }
}
