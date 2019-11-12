using System;
using ASmarty.ViewEngine;

namespace TestApplication
{
    class CallTest
    {
        private ViewContext viewContext;

        public CallTest(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }

        public void Test(object[] args)
        {
            this.viewContext.ViewData["t1"] = string.Concat(this.viewContext.ViewData["t1"]," Call Test OK,Parameters: " + args[0]);
        }
    }
}
