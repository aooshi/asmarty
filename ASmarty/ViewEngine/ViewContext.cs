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
            private set;
        }

        public IUtil Util
        {
            get;
            private set;
        }

        public ViewContext(ViewEngine viewEngine)
        {
            this.ViewEngine = viewEngine;
            this.Configuration = ViewEngine.ViewConfiguration;
            this.ViewData = new Hashtable();
            this.Util = viewEngine.Util;
        }

        public string MapPath(String path)
        {
            return this.Util.MapPath(path);
        }

        public string Content(String path)
        {
            return this.Util.Content(path);
        }

        public string HtmlEncode(String content)
        {
            return this.Util.HtmlEncode(content);
        }

    }
}
