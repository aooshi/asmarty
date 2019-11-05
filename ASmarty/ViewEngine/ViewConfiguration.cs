using System;

namespace ASmarty.ViewEngine
{
    public class ViewConfiguration
    {
        /// <summary>
        /// web home path
        /// </summary>
        public string HomePath { get; set; }
        /// <summary>
        /// www root folder
        /// </summary>
        public string WwwrootFolder { get; set; }
        /// <summary>
        /// view folder
        /// </summary>
        public string ViewFolder { get; set; }
        /// <summary>
        /// view extension
        /// </summary>
        public string ViewExtension { get; set; }
        /// <summary>
        /// plugin folder
        /// </summary>
        public string PluginFolder { get; set; }
        /// <summary>
        /// caching enable / disable
        /// </summary>
        public bool Caching { get; set; }
    }
}
