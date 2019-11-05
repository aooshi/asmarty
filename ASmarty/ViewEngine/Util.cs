using System;

namespace ASmarty.ViewEngine
{
    /// <summary>
    /// util implement
    /// </summary>
    public class Util : IUtil
    {
        ViewConfiguration _configuration;

        public ViewConfiguration Configuration
        {
            get { return this._configuration; }
        }

        public Util(ViewConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public virtual string MapPath(String path)
        {
            return System.IO.Path.Combine(this._configuration.ViewFolder, path);
        }

        public virtual string HtmlEncode(String content)
        {
            //return HttpUtility.HtmlEncode(content);
            return System.Net.WebUtility.HtmlEncode(content);
        }

        public virtual string Content(string path)
        {
            return string.Concat(this._configuration.HomePath, path);
        }
    }
}
