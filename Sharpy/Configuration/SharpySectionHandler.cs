using System.Configuration;

namespace Sharpy.Configuration
{
    public class SharpySectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("plugins")]
        public PluginsElement Plugins
        {
            get { return (PluginsElement)this["plugins"]; }
            set { this["plugins"] = value; }
        }

        [ConfigurationProperty("caching")]
        [ConfigurationCollection(typeof(CachingElement))]
        public CachingElement Caching
        {
            get { return (CachingElement)this["caching"]; }
            set { this["caching"] = value; }
        }
    }
}
