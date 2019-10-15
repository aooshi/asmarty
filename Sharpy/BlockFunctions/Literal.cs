﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.BlockFunctions
{
    [Export(typeof(IBlockFunction))]
    internal class Literal : IBlockFunction
    {
        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content)
        {
            return content;
        }

        public IList<string> TagsToIgnore()
        {
            return null;
        }

        public string Name
        {
            get { return "literal"; }
        }
    }
}