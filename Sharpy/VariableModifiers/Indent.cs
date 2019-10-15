using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class Indent : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }
            else
            {
                var numberOfIndents = 4;
                if (parameters.Length >= 1)
                {
                    numberOfIndents = (int) parameters[0];
                }

                var indentChar = " ";
                if (parameters.Length == 2)
                {
                    indentChar = (string) parameters[1];
                }

                var indent = new StringBuilder();
                for (var i = 0; i < numberOfIndents; i++)
                {
                    indent.Append(indentChar);
                }

                var result = new List<string>();
                foreach (var line in input.ToString().Split('\n'))
                {
                    result.Add(string.Format("{0}{1}", indent, line));       
                }
                return string.Join("\n", result.ToArray());
            }
        }

        public string Name
        {
            get { return "indent"; }
        }
    }
}
