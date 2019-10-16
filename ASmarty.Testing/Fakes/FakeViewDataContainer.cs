using System;
using System.Web.Mvc;

namespace ASmarty.Testing.Fakes
{
    internal class FakeViewDataContainer : IViewDataContainer
    {
        public ViewDataDictionary ViewData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}