using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using ASmarty.ViewEngine;

namespace ASmarty.BlockFunctions
{
    [Export(typeof(IBlockFunction))]
    public class Strip : IBlockFunction
    {
        // Thanks to Mads Kristensen for the original regular expression
        // http://madskristensen.net/post/A-whitespace-removal-HTTP-module-for-ASPNET-20.aspx
        private static readonly Regex regex = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");

        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator, string content)
        {
            return regex.Replace(evaluator.Evaluate(content), string.Empty);
        }

        public IList<string> TagsToIgnore()
        {
            return null;
        }

        public string Name
        {
            get { return "strip"; }
        }
    }
}
