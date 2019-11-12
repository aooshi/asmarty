using ASmarty.ViewEngine;
using ASmarty.ViewEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace ASmarty.InlineFunctions
{
    [Export(typeof(IInlineFunction))]
    internal class Call : IInlineFunction
    {
        public string Name => "call";

        public string Evaluate(IDictionary<string, object> attributes, IFunctionEvaluator evaluator)
        {
            //{call object="user" function="load"}
            //{call object="user" function="load" param1="" param2="" param3=""}

            object _obj = null;
            object _fun = null;

            attributes.TryGetValue("object", out _obj);
            attributes.TryGetValue("function", out _fun);

            string obj = _obj as string;
            string fun = _fun as string;

            if (string.IsNullOrEmpty(obj))
                throw new RequiredParameterException("tag call not set object attribute");

            if (string.IsNullOrEmpty(fun))
                throw new RequiredParameterException("tag call not set function attribute");

            //
            var parameters = new List<object>(attributes.Count);
            for (int i = 1; i < int.MaxValue; i++)
            {
                object p = null;
                if (attributes.TryGetValue("param" + i, out p))
                {
                    parameters.Add(p);
                }
                else
                {
                    break;
                }
            }

            //
            object _object = null;
            if (evaluator.ViewData.TryGetValue(obj, out _object) == false)
            {
                throw new RequiredParameterException("tag call " + _object + " noset object in view date ");
            }

            //
            if (_object == null)
                return null;

            //
            object methodInfo = null;
            string methodName = string.Format("call_{0}_{1}", obj, fun);
            if (evaluator.FunctionData.TryGetValue(methodName, out methodInfo) == false)
            {
                var funMethodInfo = _object.GetType().GetMethod(fun);
                if (funMethodInfo == null)
                    throw new RequiredParameterException("tag call " + _object + " not method " + fun);

                methodInfo = Activator.CreateInstance(typeof(ASmarty.Tools.Accessor<>).MakeGenericType(_object.GetType()), _object.GetType(), fun) as Tools.IAccessor;
                evaluator.FunctionData[methodName] = methodInfo;
            }

            //
            var accessor = methodInfo as Tools.IAccessor;
            if (accessor != null)
            {
                accessor.Invoke(_object, parameters.ToArray());
            }

            //
            return null;
        }
    }
}
