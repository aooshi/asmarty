using System;
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

        public ViewEngine(ViewConfiguration viewConfiguration)
        {
            this.ViewConfiguration = viewConfiguration;

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var catalog = new AggregateCatalog(assemblyCatalog);
            if (!string.IsNullOrEmpty(viewConfiguration.PluginFolder))
            {
                if (System.IO.Directory.Exists(viewConfiguration.PluginFolder) == false)
                {
                    throw new DirectoryNotFoundException("PluginFolder Not found");
                }
                //var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, viewConfiguration.PluginFolder));
                var directoryCatalog = new DirectoryCatalog(viewConfiguration.PluginFolder);
                catalog.Catalogs.Add(directoryCatalog);
            }

            new CompositionContainer(catalog).ComposeParts(this);
            functions = new Functions(ImportedBlockFunctions, ImportedInlineFunctions, ImportedExpressionFunctions, ImportedVariableModifiers);
        }

        public IView CreatePartialView(string partialPath)
        {
            var partialFile = string.IsNullOrEmpty(partialPath) ? null : (partialPath + this.ViewConfiguration.ViewExtension);
            return new View(partialFile, null, functions, false);
        }

        public IView CreateView(string viewPath, string masterPath)
        {
            var ext = this.ViewConfiguration.ViewExtension;
            var viewFile = viewPath + ext;
            var masterFile = string.IsNullOrEmpty(masterPath) ? null : (masterPath + ext);
            return new View(viewFile, masterFile, functions, this.ViewConfiguration.Caching);
        }
    }
}