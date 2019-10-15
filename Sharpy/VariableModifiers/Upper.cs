﻿using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    internal class Upper : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            return input == null ? null : input.ToString().ToUpper();
        }

        public string Name
        {
            get { return "upper"; }
        }
    }
}
