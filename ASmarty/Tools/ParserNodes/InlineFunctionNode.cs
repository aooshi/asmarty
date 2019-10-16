using System.Collections.Generic;
using ASmarty.ViewEngine;

namespace ASmarty.Tools.ParserNodes
{
    internal class InlineFunctionNode : IParserNode
    {
        public int Id { get; private set; }
        public IInlineFunction InlineFunction { get; private set; }
        public IDictionary<string, ExpressionNode> Attributes { get; set; }

        public InlineFunctionNode(int id, IInlineFunction inlineFunction, IDictionary<string, ExpressionNode> attributes)
        {
            Id = id;
            InlineFunction = inlineFunction;
            Attributes = attributes;
        }
    }
}
