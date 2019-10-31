using System;
using System.Collections;
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

        public ViewEngine ViewEngine
        {
            get;
            private set;
        }

        public IDictionary ViewData
        {
            get;
            protected set;
        }

        public ViewContext(ViewEngine viewEngine)
        {
            this.ViewEngine = viewEngine;
            this.Configuration = ViewEngine.ViewConfiguration;
            this.ViewData = new Hashtable();
        }

        public virtual string MapPath(String path)
        {
            return System.IO.Path.Combine(this.Configuration.ViewRootPath, path);
        }

        public virtual string ContentUrl(String path)
        {
            return string.Concat(this.Configuration.WwwrootPath, path);
        }

        public virtual string HtmlEncode(String content)
        {
            //return HttpUtility.HtmlEncode(content);
            return System.Net.WebUtility.HtmlEncode(content);
        }
    }
}
