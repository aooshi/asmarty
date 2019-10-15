using Adf;
using ASmarty.ViewEngine;
using System;
using System.Net;

namespace TestApplication
{
    class Program
    {
        public static ViewEngine ViewEngine
        {
            get;
            private set;
        }

        static void Main(string[] args)
        {
            ViewConfiguration vc = new ViewConfiguration();

            vc.ApplicationPath = "/";
            vc.Caching = true;
            //vc.PluginFolder = "";
            vc.ViewRootPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Views/");
            vc.ViewExtension = ".tpl";

            //
            ViewEngine = new ViewEngine(vc);


            Adf.HttpServer server = new Adf.HttpServer(8082);
            server.Callback = HttpServerCallback;
            server.Start();
        }

        private static HttpStatusCode HttpServerCallback(HttpServerContext context)
        {
            context.Content = "23";

            return HttpStatusCode.OK;
        }
    }
}
