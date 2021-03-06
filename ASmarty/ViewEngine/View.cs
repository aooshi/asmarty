﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASmarty.Extensions;
using ASmarty.Tools;
using ASmarty.Tools.ParserNodes;

namespace ASmarty.ViewEngine
{
    internal class View : IView
    {
        private static readonly IDictionary<string, IEnumerable<IParserNode>> cachedViews = new Dictionary<string, IEnumerable<IParserNode>>();

        private readonly string viewPath;
        private readonly string masterPath;
        private readonly Functions functions;
        private readonly bool cache;

        public View(string viewPath, string masterPath, Functions functions, bool cache)
        {
            this.viewPath = viewPath;
            this.masterPath = masterPath;
            this.functions = functions;
            this.cache = cache;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            var nodes = GetNodes(viewContext, viewPath);

            var evaluator = new InternalEvaluator(viewContext, functions);
            var masterNode = nodes.First() as MasterNode;
            if (!string.IsNullOrEmpty(masterPath))
            {
                string content;
                if (masterNode != null)
                {
                    var attributes = evaluator.EvaluateAttributes(masterNode.Id, masterNode.Attributes);
                    foreach (var attribute in attributes.Where(a => a.Key != "file"))
                    {
                        evaluator.LocalData[attribute.Key] = attribute.Value;
                    }

                    content = evaluator.Evaluate(masterNode.Content);
                }
                else
                {
                    content = evaluator.Evaluate(nodes);
                }

                evaluator.ViewData["content"] = content;

                var masterNodes = GetNodes(viewContext, masterPath);
                writer.Write(evaluator.Evaluate(masterNodes));
            }
            else if (masterNode != null)
            {
                var attributes = evaluator.EvaluateAttributes(masterNode.Id, masterNode.Attributes);
                var evaluatedView = evaluator.Evaluate(masterNode.Content);

                var file = attributes.GetRequiredAttribute<string>("file");
                var masterNodes = GetNodes(viewContext, file);
                foreach (var attribute in attributes.Where(a => a.Key != "file"))
                {
                    evaluator.LocalData[attribute.Key] = attribute.Value;
                }

                evaluator.ViewData["content"] = evaluatedView;
                writer.Write(evaluator.Evaluate(masterNodes));
            }
            else
            {
                writer.Write(evaluator.Evaluate(nodes));
            }
        }

        private IEnumerable<IParserNode> GetNodes(ViewContext viewContext, string path)
        {
            IEnumerable<IParserNode> nodes = null;
            //if (cache && cachedViews.ContainsKey(path))
            //{
            //    nodes = cachedViews[path];
            //}
            if (cache && cachedViews.TryGetValue(path, out nodes))
            {
                //
            }
            else
            {
                using (var stream = new StreamReader(viewContext.MapViewPath(path)))
                {
                    var tokenizer = new Tokenizer(stream);
                    var parser = new Parser(1, tokenizer, functions);

                    nodes = parser.ParseAll();
                }

                if (cache)
                {
                    cachedViews[path] = nodes;
                }
            }
            return nodes;
        }
    }
}

