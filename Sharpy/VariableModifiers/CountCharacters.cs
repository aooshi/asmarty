using System.ComponentModel.Composition;
using ASmarty.ViewEngine;

namespace ASmarty.VariableModifiers
{
    [Export(typeof(IVariableModifier))]
    public class CountCharacters : IVariableModifier
    {
        public object Evaluate(object input, IEvaluator evaluator, params object[] parameters)
        {
            if (input == null)
            {
                return 0;
            }
            else
            {
                var includeWhitespacesInCount = false;
                if (parameters.Length == 1)
                {
                    includeWhitespacesInCount = (bool) parameters[0];
                }
                return GetNumberOfCharacters(input.ToString(), includeWhitespacesInCount);
            }
        }

        private static int GetNumberOfCharacters(string input, bool includeWhitespacesInCount)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (includeWhitespacesInCount || !char.IsWhiteSpace(input[i]))
                {
                    count++;
                }
            }

            return count;
        }

        public string Name
        {
            get { return "count_characters"; }
        }
    }
}
