using System;
using System.Web.Mvc;

namespace Sharpy.Testing.Fakes
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