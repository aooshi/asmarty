using System;
using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public class AccessContext
    {
        public IDictionary<string, object> ViewData
        {
            get;
            private set;
        }

        public object ViewModel
        {
            get;
            private set;
        }

        internal AccessContext(object viewModel)
        {
            this.ViewData = new Dictionary<string, object>();
            this.ViewModel = viewModel;
        }

    }
}
