using System.Configuration;

namespace ASmarty.Configuration
{
    public class SectionHandler : ConfigurationSection
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
