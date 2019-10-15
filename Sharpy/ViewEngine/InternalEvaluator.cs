using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sharpy.Tools;
using Sharpy.Extensions;
using Sharpy.Tools.ParserNodes;

namespace Sharpy.ViewEngine
{
    internal class InternalEvaluator
    {
        internal IDictionary<string, object> ViewData { get; private set; }
        internal IDictionary<string, object> LocalData { get; private set; }
        internal object Model { get; private set; }

        private readonly RequestContext requestContext;
        private readonly HttpContextBase httpContext;
        private readonly SharpyFunctions functions;
        private readonly UrlHelper urlHelper;
        private readonly IDictionary<int, IDictionary<string, object>> functionData;

        internal InternalEvaluator(RequestContext requestContext, HttpContextBase httpContext, IDictionary<string, object> viewData, object model, SharpyFunctions functions)
        {
            this.requestContext = requestContext;
            this.httpContext = httpContext;
            this.functions = functions;

            ViewData = viewData;
            LocalData = new Dictionary<string, object>();
            Model = model;
            functionData = new Dictionary<int, IDictionary<string, object>>();

            if (requestContext != null)
            {
                urlHelper = new UrlHelper(requestContext);                
            }
        }        

        private IDictionary<string, object> GetFunctionData(int nodeId)
        {
            if (!functionData.ContainsKey(nodeId))
            {
                functionData.Add(nodeId, new Dictionary<string, object>());
            }

            return functionData[nodeId];
        }

        internal string Evaluate(IEnumerable<IParserNode> nodes)
        {
            var output = new StringBuilder();

            foreach (var node in nodes)
            {
                EvaluateNode(node, output);
            }

            return output.ToString();
        }

        internal string Evaluate(int nodeId, string content)
        {
            IEnumerable<IParserNode> nodes;
            using (var stream = new StringReader(content))
            {
                var tokenizer = new Tokenizer(stream);
                var parser = new Parser(nodeId + 1, tokenizer, functions);

                nodes = parser.ParseAll();
            }

            return Evaluate(nodes);
        }

        internal string EvaluateUrl(string contentPath)
        {
            return urlHelper.Content(contentPath);
        }

        internal string EvaluateTemplate(int nodeId, string templatePath)
        {
            return EvaluateTemplate(nodeId, templatePath, new Dictionary<string, object>());
        }

        internal string EvaluateTemplate(int nodeId, string templatePath, object model)
        {
            return EvaluateTemplate(nodeId, templatePath, model, ViewData);
        }

        internal string EvaluateTemplate(int nodeId, string templatePath, IDictionary<string, object> viewData)
        {
            return EvaluateTemplate(nodeId, templatePath, null, viewData);
        }

        internal string EvaluateTemplate(int nodeId, string templatePath, object model, IDictionary<string, object> viewData)
        {
            IEnumerable<IParserNode> nodes;
            using (var stream = new StreamReader(httpContext.Server.MapPath(templatePath)))
            {
                var tokenizer = new Tokenizer(stream);
                var parser = new Parser(nodeId + 1, tokenizer, functions);

                nodes = parser.ParseAll();
            }

            var evaluator = new InternalEvaluator(requestContext, httpContext, viewData, model, functions);
            return evaluator.Evaluate(nodes);
        }

        internal object EvaluateExpression(int nodeId, ExpressionNode expression)
        {
            var expressionParts = expression.Detail.ParseExpression();

            var variables = new Dictionary<string, string>();
            foreach (var part in expressionParts.Where(p => p.Type == PartType.Expression))
            {
                while (true)
                {
                    var match = Regex.Match(part.Detail, @"\$[A-Za-z0-9_]+");
                    if (match.Success)
                    {
                        if (!variables.ContainsKey(match.Value))
                        {
                            variables.Add(match.Value, string.Format("__variable{0}", variables.Count));
                        }
                        part.Detail = part.Detail.Substring(0, match.Index) + variables[match.Value] + part.Detail.Substring(match.Index + match.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (var replacement in ExpressionReplacements)
                {
                    part.Detail = Regex.Replace(part.Detail, string.Format(@"\s{0}\s", replacement.Key), string.Format(" {0} ", replacement.Value));
                }
            }

            var currentExpression = new StringBuilder();
            foreach (var part in expressionParts)
            {
                currentExpression.Append(part.Detail);
            }

            var values = new List<object>();
            foreach (var variable in variables.Keys)
            {
                values.Add(GetCurrentVariableValue(variable.Substring(1)));
            }

            var parameterExpressions = new List<ParameterExpression>();
            int index = 0;
            foreach (var variable in variables)
            {
                Type parameterType = typeof(object);
                if (values[index] != null)
                {
                    parameterType = values[index].GetType();
                }

                parameterExpressions.Add(Expression.Parameter(parameterType, variable.Value));
                index++;
            }

            var lambda = DynamicExpression.ParseLambda(parameterExpressions.ToArray(), null, currentExpression.ToString());
            var compiled = lambda.Compile();
            var result = compiled.DynamicInvoke(values.ToArray());

            if (expression.Modifiers != null)
            {
                foreach (var modifier in expression.Modifiers)
                {
                    var evaluatedParameters = new List<object>();
                    foreach (var expressionNode in modifier.Value)
                    {
                        evaluatedParameters.Add(EvaluateExpression(nodeId, expressionNode));
                    }
                    result = functions.VariableModifiers[modifier.Key].Evaluate(result, new Evaluator(this, nodeId), evaluatedParameters.ToArray());
                }
            }

            return result;
        }

        internal string Encode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }

        private void EvaluateNode(IParserNode node, StringBuilder output)
        {
            if (node is HtmlNode)
            {
                output.Append(((HtmlNode)node).Content);
            }
            else if (node is ExpressionNode)
            {
                EvaluateExpressionNode((ExpressionNode) node, output);
            }
            else if (node is InlineFunctionNode)
            {
                EvaluateInlineFunctionNode((InlineFunctionNode) node, output);
            }
            else if (node is BlockFunctionNode)
            {
                EvaluateBlockFunctionNode((BlockFunctionNode) node, output);
            }
            else if (node is ExpressionFunctionNode)
            {
                EvaluateExpressionFunctionNode((ExpressionFunctionNode) node, output);
            }
        }

        private void EvaluateExpressionNode(ExpressionNode node, StringBuilder output)
        {
            var result = EvaluateExpression(node.Id, node);
            if (result != null)
            {
                output.Append(result.ToString());
            }
        }

        private void EvaluateInlineFunctionNode(InlineFunctionNode node, StringBuilder output)
        {
            var evaluatedAttributes = EvaluateAttributes(node.Id, node.Attributes);
            var result = node.InlineFunction.Evaluate(evaluatedAttributes, new FunctionEvaluator(this, node.Id, GetFunctionData(node.Id)));

            if (!string.IsNullOrEmpty(result))
            {
                output.Append(result);
            }
        }

        private void EvaluateBlockFunctionNode(BlockFunctionNode node, StringBuilder output)
        {
            var evaluatedAttributes = EvaluateAttributes(node.Id, node.Attributes);
            var result = node.BlockFunction.Evaluate(evaluatedAttributes, new FunctionEvaluator(this, node.Id, GetFunctionData(node.Id)), node.Content);

            if (!string.IsNullOrEmpty(result))
            {
                output.Append(result);
            }
        }

        private void EvaluateExpressionFunctionNode(ExpressionFunctionNode node, StringBuilder output)
        {
            var result = node.ExpressionFunction.Evaluate(node.Detail, new FunctionEvaluator(this, node.Id, GetFunctionData(node.Id)), node.Content);
            if (!string.IsNullOrEmpty(result))
            {
                output.Append(result);
            }
        }

        private object GetCurrentVariableValue(string variableName)
        {
            // First look for the variable in the local context
            if (LocalData.ContainsKey(variableName))
            {
                return LocalData[variableName];
            }
            else if (ViewData.ContainsKey(variableName))
            {
                return ViewData[variableName];
            }
            else if (variableName == "Model")
            {
                return Model;
            }
            else
            {
                return null;
            }
        }

        internal IDictionary<string,object> EvaluateAttributes(int nodeId, IEnumerable<KeyValuePair<string, ExpressionNode>> attributes)
        {
            var evaluatedAttributes = new Dictionary<string,object>();
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    object evaluatedValue = attribute.Value;
                    if (attribute.Value != null)
                    {
                        evaluatedValue = EvaluateExpression(nodeId, attribute.Value);
                    }

                    evaluatedAttributes.Add(attribute.Key, evaluatedValue);
                }
            }
            return evaluatedAttributes;
        }

        private static IDictionary<string,string> expressionReplacements = null;
        private static readonly object expressionReplacementsLock = new object();

        private static IDictionary<string, string> ExpressionReplacements
        {
            get
            {
                if (expressionReplacements == null)
                {
                    lock (expressionReplacementsLock)
                    {
                        if (expressionReplacements == null)
                        {
                            var result = new Dictionary<string, string>();

                            result.Add("eq", "==");
                            result.Add("ne", "!=");
                            result.Add("neq", "!=");
                            result.Add("gt", ">");
                            result.Add("lt", "<");
                            result.Add("gte", ">=");
                            result.Add("ge", ">=");
                            result.Add("lte", "<=");
                            result.Add("le", "<=");

                            expressionReplacements = result;
                        }
                    }
                }

                return expressionReplacements;
            }
        }
    }
}