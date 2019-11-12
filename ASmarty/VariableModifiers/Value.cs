using ASmarty.ViewEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    internal class Value : IVariableModifier
    {
        public string Name => "value";

        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            //'view data name'|value:'key':'args1':'args2'

            if (input == null)
                return null;

            var name = input.ToString();

            //
            object dictObj = null;
            string value = null;

            //
            if (parameters.Length > 0)
            {
                var k = (string)parameters[0];

                if (evaluator.ViewData.TryGetValue(name, out dictObj) && dictObj != null)
                {
                    if (dictObj is IDictionary<string, string>)
                    {
                        IDictionary<string, string> dict = (IDictionary<string, string>)dictObj;
                        dict.TryGetValue(k, out value);
                    }
                    else if (dictObj is IDictionary)
                    {
                        IDictionary dict = (IDictionary)dictObj;
                        try
                        {
                            value = dict[k] as string;
                        }
                        catch { }
                    }
                }
            }

            //
            if (value != null)
            {
                value = string.Format(value, parameters);
            }

            //
            return value;
        }
    }
}
