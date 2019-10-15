using System.Collections.Generic;
using Sharpy.ViewEngine.Exceptions;

namespace Sharpy.Extensions
{
    public static class AttributeExtensions
    {
        public static TObjectType GetRequiredAttribute<TObjectType>(this IDictionary<string,object> attributes, string name)
        {
            if (attributes.ContainsKey(name))
            {
                return (TObjectType)attributes[name];
            }
            else
            {
                throw new RequiredAttributeException(string.Format("The attribute {0} is required, but was not supplied", name));
            }
        }

        public static TObjectType GetOptionalAttribute<TObjectType>(this IDictionary<string, object> attributes, string name) where TObjectType : class 
        {
            if (attributes.ContainsKey(name))
            {
                return (TObjectType)attributes[name];
            }
            else
            {
                return null;
            }
        }
    }
}
