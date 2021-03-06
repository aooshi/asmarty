﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    public class RDelim : IInlineFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            return "}";
        }

        public string Name
        {
            get { return "rdelim"; }
        }
    }
}
