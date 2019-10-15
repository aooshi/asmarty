using System.Collections.Generic;

namespace Sharpy.ViewEngine
{
    public interface IEvaluator
    {
        IDictionary<string, object> ViewData { get; }
        IDictionary<string, object> LocalData { get; }
        object Model { get; }

        string Evaluate(string content);
        string EvaluateUrl(string contentPath);
        string EvaluateTemplate(string templatePath);
        string EvaluateTemplate(string templatePath, object model);
        string EvaluateTemplate(string templatePath, object model, IDictionary<string, object> viewData);
        string EvaluateTemplate(string templatePath, IDictionary<string, object> viewData);
        object EvaluateExpression(string expression);

        string Encode(string content);
    }
}