using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class WordWrap : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }
            
            var columnLength = 80;
            if (parameters.Length >= 1)
            {
                columnLength = (int) parameters[0];
            }

            var delimiter = "\n";
            if (parameters.Length >= 2)
            {
                delimiter = (string)parameters[1];
            }

            var ignoreWordBoundaries = false;
            if (parameters.Length == 3)
            {
                ignoreWordBoundaries = (bool) parameters[2];
            }

            var results = new List<string>();
            var content = input.ToString();
            while (content.Length > columnLength)
            {
                string line;
                if (!ignoreWordBoundaries)
                {
                    line = Regex.Replace(content.Substring(0, columnLength + 1), @"\s+?(\S+)?$", string.Empty);
                }
                else
                {
                    line = content.Substring(0, columnLength);
                }
                content = content.Substring(line.Length).TrimStart();
                results.Add(line);
            }

            if (content.Length > 0)
            {
                results.Add(content);
            }

            return string.Join(delimiter, results.ToArray());
        }

        public string Name
        {
            get { return "wordwrap"; }
        }
    }
}
