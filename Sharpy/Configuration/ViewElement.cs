using System.Configuration;

namespace Sharpy.Configuration
{
    public class ViewElement : ConfigurationElement
    {
        [ConfigurationProperty("view")]
        public string View
        {
            get { return (string)this["view"]; }
            set { this["view"] = value; }
        }
    }
}
