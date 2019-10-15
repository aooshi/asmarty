using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    internal class ASmartyFunctions
    {
        internal IDictionary<string, IBlockFunction> BlockFunctions { get; private set; }
        internal IDictionary<string, IInlineFunction> InlineFunctions { get; private set; }
        internal IDictionary<string, IExpressionFunction> ExpressionFunctions { get; private set; }
        internal IDictionary<string, IVariableModifier> VariableModifiers { get; private set; }

        public ASmartyFunctions(IEnumerable<IBlockFunction> blockFunctions, IEnumerable<IInlineFunction> inlineFunctions, IEnumerable<IExpressionFunction> expressionFunctions, IEnumerable<IVariableModifier> variableModifiers)
        {
            BlockFunctions = CreateDictionary(blockFunctions);
            InlineFunctions = CreateDictionary(inlineFunctions);
            ExpressionFunctions = CreateDictionary(expressionFunctions);
            VariableModifiers = CreateDictionary(variableModifiers);
        }

        private static IDictionary<string, TValueType> CreateDictionary<TValueType>(IEnumerable<TValueType> list) where TValueType : INamedExport
        {
            var dictionary = new Dictionary<string, TValueType>();
            foreach (var value in list)
            {
                dictionary.Add(value.Name, value);
            }

            return dictionary;
        }
    }
}
