using System.Configuration;

namespace ASmarty.Configuration
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
