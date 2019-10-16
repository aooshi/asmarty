using System;

namespace ASmarty.ViewEngine
{
    public class ViewConfiguration
    {
        public string ApplicationPath { get; set; }
        public string ViewRootPath { get; set; }
        public string ViewExtension { get; set; }
        public string PluginFolder { get; set; }

        public bool Caching { get; set; }
    }
}
