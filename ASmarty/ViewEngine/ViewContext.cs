using System;
using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public class ViewContext
    {
        ViewConfiguration _configuration;
        public ViewConfiguration Configuration
        {
            get { return this._configuration; }
        }

        ViewEngine _viewEngine;
        public ViewEngine ViewEngine
        {
            get { return _viewEngine; }
        }

        IDictionary<string, object> _viewData;
        public IDictionary<string, object> ViewData
        {
            get { return _viewData; }
        }

        object _viewModel;
        public object ViewModel
        {
            get { return this._viewModel; }
        }

        public ViewContext(ViewEngine viewEngine, object viewModel, IDictionary<string, object> viewData)
        {
            this._viewEngine = viewEngine;
            this._configuration = viewEngine.ViewConfiguration;
            this._viewData = viewData;
            this._viewModel = viewModel;
        }

        public ViewContext(ViewEngine viewEngine, object viewModel)
        {
            this._viewEngine = viewEngine;
            this._configuration = viewEngine.ViewConfiguration;
            this._viewData = new Dictionary<string, object>();
            this._viewModel = viewModel;
        }

        public ViewContext(ViewEngine viewEngine)
        {
            this._viewEngine = viewEngine;
            this._configuration = viewEngine.ViewConfiguration;
            this._viewData = new Dictionary<string, object>();
            this._viewModel = null;
        }

        public virtual string MapPath(String path)
        {
            if (path == null)
                return this._configuration.WwwrootFolder;

            if (path == "")
                return this._configuration.WwwrootFolder;

            if (path[0] == '/')
                return this._configuration.WwwrootFolder + path.Substring(1);

            if (path[0] == '\\')
                return this._configuration.WwwrootFolder + path.Substring(1);

            return this._configuration.WwwrootFolder + path;
        }

        public virtual string MapViewPath(String path)
        {
            if (path == null)
                return this._configuration.ViewFolder;

            if (path == "")
                return this._configuration.ViewFolder;

            if (path[0] == '/')
                return this._configuration.ViewFolder + path.Substring(1);

            if (path[0] == '\\')
                return this._configuration.ViewFolder + path.Substring(1);

            return this._configuration.ViewFolder + path;
        }

        public virtual string HtmlEncode(String content)
        {
            //return HttpUtility.HtmlEncode(content);
            return System.Net.WebUtility.HtmlEncode(content);
        }

        public virtual string Content(string path)
        {
            if (path == null)
                return this._configuration.HomePath;

            if (path == "")
                return this._configuration.HomePath;

            if (path[0] == '/')
                return this._configuration.HomePath + path.Substring(1);

            if (path[0] == '\\')
                return this._configuration.HomePath + path.Substring(1);

            return string.Concat(this._configuration.HomePath, path);
        }
    }
}
