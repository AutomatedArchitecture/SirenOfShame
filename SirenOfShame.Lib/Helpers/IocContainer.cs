using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;

namespace SirenOfShame.Lib.Helpers
{
    public class IocContainer
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(IocContainer));

        private static readonly IocContainer _instance = new IocContainer();
        private readonly CompositionContainer _container;
        private readonly string _pluginsDirectory;
        private readonly FactoryExportProvider _exportProvider;

        public static IocContainer Instance
        {
            get { return _instance; }
        }

        private IocContainer()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            _pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(_pluginsDirectory))
            {
                _log.Error(_pluginsDirectory + " does not exist, using current directory");
                return;
            }
            AggregateCatalog catalog = new AggregateCatalog(
                new AssemblyCatalog(GetType().Assembly),
                new DirectoryCatalog(_pluginsDirectory)
                );
            _exportProvider = new FactoryExportProvider();
            _container = new CompositionContainer(catalog, _exportProvider);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string name = args.Name;
            int firstComma = name.IndexOf(',');
            if (firstComma > 0)
            {
                name = name.Substring(0, firstComma);
            }
            string path = Path.Combine(_pluginsDirectory, name + ".dll");
            if (File.Exists(path))
            {
                return Assembly.LoadFrom(path);
            }
            return null;
        }

        public void Compose(object obj)
        {
            if (_container != null)
                _container.ComposeParts(obj);
        }

        public T GetExport<T>()
        {
            Lazy<T> export = _container.GetExport<T>();
            return export == null ? default(T) : export.Value;
        }

        public IEnumerable<T> GetExports<T>()
        {
            return _container.GetExports<T>().Select(e => e.Value);
        }

        public void Register<T>(Type type, T o)
        {
            _exportProvider.RegisterInstance(type, ep => o);
        }

        public void TryLogAssemblyVersions()
        {
            try
            {
                LogAssemblyVersions();
            }
            catch (Exception ex)
            {
                _log.Error("Could not log assembly versions", ex);
            }
        }

        private void LogAssemblyVersions()
        {
            LogAssemblyVersion(Assembly.GetEntryAssembly());
            LogAssemblyVersion(typeof(MyLogManager).Assembly);
            foreach (var file in Directory.GetFiles(_pluginsDirectory, "*.dll"))
            {
                _log.Info(file + " (Timestamp: " + AssemblyHelpers.GetTimestamp(file) + ")");
            }
        }

        private void LogAssemblyVersion(Assembly assembly)
        {
            _log.Info(assembly.Location + " (LinkerTimestamp: " + assembly.GetLinkerTimestamp() + ")");
        }

    }
}
