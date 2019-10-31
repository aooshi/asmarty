using Adf;
using ASmarty.ViewEngine;
using System;
using System.IO;
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

            vc.WwwrootPath = "/";
            vc.Caching = true;
            //vc.PluginFolder = "";
            vc.ViewRootPath = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../Views/");
            vc.ViewExtension = ".tpl";

            //
            ViewEngine = new ViewEngine(vc);


            Adf.HttpServer server = new Adf.HttpServer(8082);
            server.Callback = HttpServerCallback;
            server.Start();

            Console.WriteLine("ok");
            Console.ReadLine();
        }

        private static HttpStatusCode HttpServerCallback(HttpServerContext context)
        {
            var view = ViewEngine.CreateView("Guestbook/Index.tpl", "Shared/Master.tpl");

            using (StringWriter sw = new StringWriter())
            {
                var viewContext = new ViewContext(ViewEngine);

                view.Render(viewContext, ViewEngine.CreateAccessContext(null), sw);

                context.Content = sw.ToString();
            }

            return HttpStatusCode.OK;
        }
    }
}
