using System.Configuration;

namespace Sharpy.Configuration
{
    public class PluginsElement : ConfigurationElement
    {
        [ConfigurationProperty("folder")]
        public string Folder
        {
            get { return (string)this["folder"]; }
            set { this["folder"] = value; }
        }
    }
}
