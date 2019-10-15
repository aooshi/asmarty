using System.Configuration;

namespace Sharpy.Configuration
{
    public class CachingElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ViewElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {            
            return ((ViewElement)element).View;
        }

        public new ViewElement this[string key]
        {
            get { return BaseGet(key) as ViewElement; }
        }
    }
}
