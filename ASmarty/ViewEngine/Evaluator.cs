using System.Collections.Generic;
using ASmarty.Tools.ParserNodes;

namespace ASmarty.ViewEngine
{
    internal class Evaluator : IEvaluator
    {
        private readonly InternalEvaluator evaluator;
        private readonly int currentNodeId;

        public Evaluator(InternalEvaluator evaluator, int currentNodeId)
        {
            this.evaluator = evaluator;
            this.currentNodeId = currentNodeId;
        }

        public IDictionary<string, object> ViewData
        {
            get { return evaluator.ViewData; }
        }

        public IDictionary<string, object> LocalData
        {
            get { return evaluator.LocalData; }
        }

        public object Model
        {
            get { return evaluator.Model; }
        }

        public string Evaluate(string content)
        {
            return evaluator.Evaluate(currentNodeId, content);
        }

        public string EvaluateUrl(string contentPath)
        {
            return evaluator.EvaluateUrl(contentPath);
        }

        public string EvaluateTemplate(string templatePath)
        {
            return evaluator.EvaluateTemplate(currentNodeId, templatePath);
        }

        public string EvaluateTemplate(string templatePath, object model)
        {
            return evaluator.EvaluateTemplate(currentNodeId, templatePath, model);
        }

        public string EvaluateTemplate(string templatePath, object model, IDictionary<string, object> viewData)
        {
            return evaluator.EvaluateTemplate(currentNodeId, templatePath, model, viewData);
        }

        public string EvaluateTemplate(string templatePath, IDictionary<string, object> viewData)
        {
            return evaluator.EvaluateTemplate(currentNodeId, templatePath, viewData);
        }

        public object EvaluateExpression(string expression)
        {
            return evaluator.EvaluateExpression(currentNodeId, new ExpressionNode(currentNodeId, expression, null));
        }

        public string Encode(string content)
        {
            return evaluator.Encode(content);
        }        
    }
}
