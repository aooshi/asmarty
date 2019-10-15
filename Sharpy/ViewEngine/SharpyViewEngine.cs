using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using Sharpy.Configuration;

namespace Sharpy.ViewEngine
{
    public class SharpyViewEngine : VirtualPathProviderViewEngine
    {
        [ImportMany(typeof(IBlockFunction))] private IEnumerable<IBlockFunction> ImportedBlockFunctions { get; set; }
        [ImportMany(typeof(IInlineFunction))] private IEnumerable<IInlineFunction> ImportedInlineFunctions { get; set; }
        [ImportMany(typeof(IExpressionFunction))] private IEnumerable<IExpressionFunction> ImportedExpressionFunctions { get; set; }
        [ImportMany(typeof(IVariableModifier))] private IEnumerable<IVariableModifier> ImportedVariableModifiers { get; set; }

        private readonly SharpyFunctions functions;
        private readonly SharpySectionHandler settings;

        public SharpyViewEngine()
        {
            settings = (SharpySectionHandler) ConfigurationManager.GetSection("sharpy") ?? new SharpySectionHandler();

            ViewLocationFormats = new[] { "~/Views/{1}/{0}.sharpy" };
            PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.sharpy" };
            MasterLocationFormats = new[] { "~/Views/Shared/{0}.sharpy" };

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog = new AggregateCatalog(assemblyCatalog);
            if (!string.IsNullOrEmpty(settings.Plugins.Folder))
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.Plugins.Folder));
                catalog.Catalogs.Add(directoryCatalog);
            }

            new CompositionContainer(catalog).ComposeParts(this);
            functions = new SharpyFunctions(ImportedBlockFunctions, ImportedInlineFunctions, ImportedExpressionFunctions, ImportedVariableModifiers);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new SharpyView(partialPath, null, functions, false);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new SharpyView(viewPath, masterPath, functions, settings.Caching[viewPath] != null);
        }
    }
}