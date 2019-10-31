﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace ASmarty.ViewEngine
{
    public class ViewEngine
    {
        [ImportMany(typeof(IBlockFunction))] private IEnumerable<IBlockFunction> ImportedBlockFunctions { get; set; }
        [ImportMany(typeof(IInlineFunction))] private IEnumerable<IInlineFunction> ImportedInlineFunctions { get; set; }
        [ImportMany(typeof(IExpressionFunction))] private IEnumerable<IExpressionFunction> ImportedExpressionFunctions { get; set; }
        [ImportMany(typeof(IVariableModifier))] private IEnumerable<IVariableModifier> ImportedVariableModifiers { get; set; }

        private readonly Functions functions;
        
        public ViewConfiguration ViewConfiguration
        {
            get;
            private set;
        }

        public ViewContext Context
        {
            get;
            private set;
        }

        public ViewEngine(ViewConfiguration viewConfiguration)
        {
            //ViewLocationFormats = new[] { "~/Views/{1}/{0}.tpl" };
            //PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.tpl" };
            //MasterLocationFormats = new[] { "~/Views/Shared/{0}.tpl" };

            this.ViewConfiguration = viewConfiguration;
            this.Context = new ViewContext(viewConfiguration);

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog = new AggregateCatalog(assemblyCatalog);
            if (!string.IsNullOrEmpty(viewConfiguration.PluginFolder))
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, viewConfiguration.PluginFolder));
                catalog.Catalogs.Add(directoryCatalog);
            }

            new CompositionContainer(catalog).ComposeParts(this);
            functions = new Functions(ImportedBlockFunctions, ImportedInlineFunctions, ImportedExpressionFunctions, ImportedVariableModifiers);
        }

        public IView CreatePartialView(string partialPath)
        {
            return new View(partialPath, null, functions, false);
        }

        public IView CreateView(string viewPath, string masterPath)
        {
            return new View(viewPath, masterPath, functions, this.ViewConfiguration.Caching);
        }

        public AccessContext CreateAccessContext(object viewModel)
        {
            return new AccessContext(viewModel);
        }
    }
}