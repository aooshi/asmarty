using System.Collections.Generic;
using Sharpy.Tools.ParserNodes;

namespace Sharpy.ViewEngine
{
    internal class FunctionEvaluator : IFunctionEvaluator
    {
        private readonly InternalEvaluator internalEvaluator;
        private readonly int currentNodeId;
        public IDictionary<string, object> FunctionData { get; private set; }

        public FunctionEvaluator(InternalEvaluator internalEvaluator, int currentNodeId, IDictionary<string,object> functionData)
        {
            this.internalEvaluator = internalEvaluator;
            this.currentNodeId = currentNodeId;
            FunctionData = functionData;
        }

        public IDictionary<string, object> ViewData
        {
            get { return internalEvaluator.ViewData; }
        }

        public IDictionary<string, object> LocalData
        {
            get { return internalEvaluator.LocalData; }
        }

        public object Model
        {
            get { return internalEvaluator.Model; }
        }

        public string Evaluate(string content)
        {
            return internalEvaluator.Evaluate(currentNodeId, content);
        }

        public string EvaluateUrl(string contentPath)
        {
            return internalEvaluator.EvaluateUrl(contentPath);
        }

        public string EvaluateTemplate(string templatePath)
        {
            return internalEvaluator.EvaluateTemplate(currentNodeId, templatePath);
        }

        public string EvaluateTemplate(string templatePath, object model)
        {
            return internalEvaluator.EvaluateTemplate(currentNodeId, templatePath, model);
        }

        public string EvaluateTemplate(string templatePath, object model, IDictionary<string, object> viewData)
        {
            return internalEvaluator.EvaluateTemplate(currentNodeId, templatePath, model, viewData);
        }

        public string EvaluateTemplate(string templatePath, IDictionary<string, object> viewData)
        {
            return internalEvaluator.EvaluateTemplate(currentNodeId, templatePath, viewData);
        }

        public object EvaluateExpression(string expression)
        {
            return internalEvaluator.EvaluateExpression(currentNodeId, new ExpressionNode(currentNodeId, expression, null));
        }

        public string Encode(string content)
        {
            return internalEvaluator.Encode(content);
        }        
    }
}
