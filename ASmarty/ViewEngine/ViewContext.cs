using System;
using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public class ViewContext
    {
        public ViewConfiguration Configuration
        {
            get;
            private set;
        }

        public IDictionary<string, object> ViewData
        {
            get;
            private set;
        }

        internal ViewContext(ViewConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ViewData = new Dictionary<string, object>();
        }


        public string MapPath(String path)
        {
            return System.IO.Path.Combine(this.Configuration.ViewRootPath, path);
        }

        public string ContentUrl(String path)
        {
            return string.Concat(this.Configuration.WwwrootPath, path);
        }

        public string HtmlEncode(String content)
        {
            //return HttpUtility.HtmlEncode(content);
            return System.Net.WebUtility.HtmlEncode(content);
        }
    }
}
