﻿using Adf;
using ASmarty.ViewEngine;
using System;
using System.Collections;
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

            vc.Caching = true;
            //vc.PluginFolder = "";
            vc.WwwrootFolder = AppContext.BaseDirectory;
            vc.ViewFolder = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../Views/");
            vc.HomePath = "/";
            vc.ViewExtension = ".tpl";
            vc.Caching = false;

            //plugin register
            vc.PluginAssemblies.Add(typeof(Program).Assembly);

            //
            ViewEngine = new ViewEngine(vc);


            Adf.HttpServer server = new Adf.HttpServer(8080);
            server.Callback = HttpServerCallback;
            server.Start();

            Console.WriteLine("ok");
            Console.ReadLine();
        }

        private static HttpStatusCode HttpServerCallback(HttpServerContext context)
        {
            if (context.Path.Equals("/favicon.ico"))
                return HttpStatusCode.NotFound;

            var vars = new Hashtable();
            vars.Add("var1", Guid.NewGuid().ToString());
            vars.Add("var2", Guid.NewGuid().ToString());
            vars.Add("var3", Guid.NewGuid().ToString());

            var obj1 = new {
                field1 = Guid.NewGuid().ToString(),
                field2 = Guid.NewGuid().ToString(),
                field3 = Guid.NewGuid().ToString()
            };

            var view = ViewEngine.CreateView("Guestbook/Index", "Shared/Master");
            using (StringWriter sw = new StringWriter())
            {
                var viewContext = new ViewContext(ViewEngine);

                viewContext.ViewData["uuid"] = Guid.NewGuid().ToString();
                viewContext.ViewData["date"] = DateTime.Now;

                viewContext.ViewData["vars"] = vars;
                viewContext.ViewData["obj1"] = obj1;

                viewContext.ViewData["t1"] = Guid.NewGuid().ToString();
                viewContext.ViewData["CallTest"] = new CallTest(viewContext);

                view.Render(viewContext, sw);

                context.Content = sw.ToString();
            }

            return HttpStatusCode.OK;
        }
    }
}
