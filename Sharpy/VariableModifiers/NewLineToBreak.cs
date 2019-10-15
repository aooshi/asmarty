﻿using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class NewLineToBreak : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return null;
            }
            else
            {
                return input.ToString().Replace("\n", "<br />");
            }
        }

        public string Name
        {
            get { return "nl2br"; }
        }
    }
}
