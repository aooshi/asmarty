﻿using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class CountSentences : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return 0;
            }
            else
            {
                return Regex.Matches(input.ToString(), @"[^\s]\.(?!\w)").Count;
            }
        }

        public string Name
        {
            get { return "count_sentences"; }
        }
    }
}
