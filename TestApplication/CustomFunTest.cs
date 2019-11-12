using ASmarty.ViewEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace TestApplication
{

    [Export(typeof(IVariableModifier))]
    internal class CustomFunTest : IVariableModifier
    {
        public string Name => "customfun";


        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
        }
    }
}
