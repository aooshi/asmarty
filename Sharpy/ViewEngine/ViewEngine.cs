﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using ASmarty.Configuration;

namespace ASmarty.ViewEngine
{
    public class ViewEngine : VirtualPathProviderViewEngine
    {
        [ImportMany(typeof(IBlockFunction))] private IEnumerable<IBlockFunction> ImportedBlockFunctions { get; set; }
        [ImportMany(typeof(IInlineFunction))] private IEnumerable<IInlineFunction> ImportedInlineFunctions { get; set; }
        [ImportMany(typeof(IExpressionFunction))] private IEnumerable<IExpressionFunction> ImportedExpressionFunctions { get; set; }
        [ImportMany(typeof(IVariableModifier))] private IEnumerable<IVariableModifier> ImportedVariableModifiers { get; set; }

        private readonly Functions functions;
        private readonly SectionHandler settings;

        public ViewEngine()
        {
            settings = (SectionHandler) ConfigurationManager.GetSection("ASmarty") ?? new SectionHandler();

            ViewLocationFormats = new[] { "~/Views/{1}/{0}.ASmarty" };
            PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.ASmarty" };
            MasterLocationFormats = new[] { "~/Views/Shared/{0}.ASmarty" };

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog = new AggregateCatalog(assemblyCatalog);
            if (!string.IsNullOrEmpty(settings.Plugins.Folder))
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.Plugins.Folder));
                catalog.Catalogs.Add(directoryCatalog);
            }

            new CompositionContainer(catalog).ComposeParts(this);
            functions = new Functions(ImportedBlockFunctions, ImportedInlineFunctions, ImportedExpressionFunctions, ImportedVariableModifiers);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new View(partialPath, null, functions, false);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new View(viewPath, masterPath, functions, settings.Caching[viewPath] != null);
        }
    }
}