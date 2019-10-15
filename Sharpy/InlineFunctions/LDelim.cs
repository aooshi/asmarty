﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using Sharpy.ViewEngine;

namespace Sharpy.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    public class LDelim : IInlineFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            return "{";
        }

        public string Name
        {
            get { return "ldelim"; }
        }
    }
}
