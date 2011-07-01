using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace SirenOfShame.Lib.Helpers
{
    /// <summary>
    /// Represents a factory export provider.
    /// </summary>
    /// <remarks>
    /// This class can be used to build custom <see cref="ExportProvider"/> which
    /// provides exports from various data sources.
    /// </remarks>
    public class FactoryExportProvider : ExportProvider
    {
        private readonly Func<Type, string, object> factoryMethod;
        private readonly List<FactoryExportDefinition> definitions;

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryExportProvider"/> class.
        /// </summary>
        public FactoryExportProvider()
        {
            definitions = new List<FactoryExportDefinition>();
            factoryMethod = (t, s) => { throw new InvalidOperationException("Global factory method has not been supplied."); };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryExportProvider"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to be registered.</param>
        /// <param name="factory">Factory method.</param>
        public FactoryExportProvider(Type type, Func<ExportProvider, object> factory)
            : this()
        {
            if (type == null) throw new ArgumentNullException("type");
            if (factory == null) throw new ArgumentNullException("factory");

            Register(type, factory);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryExportProvider"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to be registered.</param>
        /// <param name="registrationName">Registration name.</param>
        /// <param name="factory">Factory method.</param>
        public FactoryExportProvider(Type type, string registrationName, Func<ExportProvider, object> factory)
            : this()
        {
            if (type == null) throw new ArgumentNullException("type");
            if (registrationName == null) throw new ArgumentNullException("registrationName");
            if (factory == null) throw new ArgumentNullException("factory");

            Register(type, registrationName, factory);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryExportProvider"/> class.
        /// </summary>
        /// <param name="factoryMethod">Method that is called when an instance of specific type
        /// is requested, optionally with given registration name.</param>
        public FactoryExportProvider(Func<Type, string, object> factoryMethod)
        {
            if (factoryMethod == null)
                throw new ArgumentNullException("factoryMethod");

            definitions = new List<FactoryExportDefinition>();
            this.factoryMethod = factoryMethod;
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            return from exportDefinition in definitions
                   where definition.IsConstraintSatisfiedBy(exportDefinition)
                   select new Export(exportDefinition, () => exportDefinition.Factory(SourceProvider));
        }

        /// <summary>
        /// Registers a new type.
        /// </summary>
        /// <param name="type">Type that is being exported.</param>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider Register(Type type, Func<ExportProvider, object> factory = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Register(type, null, factory);
        }

        /// <summary>
        /// Registers a new type.
        /// </summary>
        /// <typeparam name="T">Type that is being exported.</typeparam>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider Register<T>(Func<ExportProvider, T> factory = null)
        {
            var factoryWrapper = (factory != null) ? ep => factory(ep) : (Func<ExportProvider, object>)null;
            return Register(typeof(T), null, factoryWrapper);
        }

        /// <summary>
        /// Registers a new type as a singleton.
        /// </summary>
        /// <param name="type">Type that is being exported.</param>
        /// <param name="factory">Factory method.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider RegisterInstance(Type type, Func<ExportProvider, object> factory)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (factory == null) throw new ArgumentNullException("factory");

            var singleton = new Singleton<object>(factory);
            return Register(type, null, singleton.GetValue);
        }

        /// <summary>
        /// Registers a new type as a singleton.
        /// </summary>
        /// <typeparam name="T">Type that is being exported.</typeparam>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider RegisterInstance<T>(Func<ExportProvider, T> factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");

            var singleton = new Singleton<T>(factory);
            return Register(typeof(T), null, singleton.GetValueAsObject);
        }

        /// <summary>
        /// Registers a new type as a singleton.
        /// </summary>
        /// <param name="type">Type that is being registered.</param>
        /// <param name="registrationName">Registration name under which <paramref name="type"/>
        /// is being registered.</param>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider RegisterInstance(Type type, string registrationName, Func<ExportProvider, object> factory)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (factory == null) throw new ArgumentNullException("factory");

            var singleton = new Singleton<object>(factory);
            return Register(type, registrationName, singleton.GetValueAsObject);
        }

        /// <summary>
        /// Registers a new type as a singleton.
        /// </summary>
        /// <typeparam name="T">Type that is being exported.</typeparam>
        /// <param name="registrationName">Registration name under which <typeparamref name="T"/>
        /// is being registered.</param>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider RegisterInstance<T>(string registrationName, Func<ExportProvider, T> factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");

            var singleton = new Singleton<T>(factory);
            return Register(typeof(T), registrationName, singleton.GetValueAsObject);
        }

        /// <summary>
        /// Registers a new type.
        /// </summary>
        /// <typeparam name="T">Type that is being exported.</typeparam>
        /// <param name="registrationName">Registration name under which <typeparamref name="T"/>
        /// is being registered.</param>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider Register<T>(string registrationName, Func<ExportProvider, T> factory = null)
        {
            var factoryWrapper = (factory != null) ? ep => factory(ep) : (Func<ExportProvider, object>)null;
            return Register(typeof(T), registrationName, factoryWrapper);
        }

        /// <summary>
        /// Registers a new type.
        /// </summary>
        /// <param name="type">Type that is being registered.</param>
        /// <param name="registrationName">Registration name under which <paramref name="type"/>
        /// is being registered.</param>
        /// <param name="factory">Optional factory method. If not supplied, the general
        /// factory method will be used.</param>
        /// <returns><see cref="FactoryExportProvider"/> instance for fluent registration.</returns>
        public FactoryExportProvider Register(Type type, string registrationName, Func<ExportProvider, object> factory = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (factory == null)
            {
                factory = ep => factoryMethod(type, registrationName);
            }

            var exportDefinitions = ReadOnlyDefinitions.Where(t => t.ContractType == type &&
                                                                   t.RegistrationName == registrationName);

            // We cannot add an export definition with the same type and registration name
            // since we will introduce cardinality problems
            if (exportDefinitions.Count() == 0)
            {
                definitions.Add(new FactoryExportDefinition(type, registrationName, factory));
            }

            return this;
        }

        /// <summary>
        /// Gets a read only list of definitions known to the export provider.
        /// </summary>
        public IEnumerable<FactoryExportDefinition> ReadOnlyDefinitions
        {
            get { return new ReadOnlyCollection<FactoryExportDefinition>(definitions); }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExportProvider"/> which is used to satisfy additional exports.
        /// </summary>
        public ExportProvider SourceProvider { get; set; }

        private class Singleton<T>
        {
            private readonly Func<ExportProvider, T> factory;
            private T value;

            public Singleton(Func<ExportProvider, T> factory)
            {
                this.factory = factory;
            }

            public T GetValue(ExportProvider sourceProvider)
            {
                lock (this)
                {
                    return ReferenceEquals(value, null)
                               ? value = factory(sourceProvider)
                               : value;
                }
            }

            public object GetValueAsObject(ExportProvider sourceProvider)
            {
                return GetValue(sourceProvider);
            }
        }
    }

    /// <summary>
    /// Represents a factory export definition that has a type and
    /// an optional registration name.
    /// </summary>
    public class FactoryExportDefinition : ExportDefinition
    {
        /// <summary>
        /// Represents a metadata which identifies a factory export.
        /// </summary>
        public const string IsFactoryExportMetadataName = "IsFactoryExport";

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryExportDefinition"/> class.
        /// </summary>
        /// <param name="type">Type this export defines.</param>
        /// <param name="registrationName">Registration name under which <paramref name="type"/>
        /// has been registered.</param>
        /// <param name="factory">Export factory.</param>
        public FactoryExportDefinition(Type type, string registrationName, Func<ExportProvider, object> factory)
            : base(GetContractName(type, registrationName), GetMetadata(type))
        {
            if (type == null) throw new ArgumentNullException("type");
            if (factory == null) throw new ArgumentNullException("factory");

            ContractType = type;
            RegistrationName = registrationName;
            Factory = factory;
        }

        /// <summary>
        /// Gets a type this export defines.
        /// </summary>
        public Type ContractType { get; private set; }

        /// <summary>
        /// Gets a registration name under which <see cref="ContractType"/> has been registered.
        /// </summary>
        public string RegistrationName { get; private set; }

        /// <summary>
        /// Gets the factory used to create instances of export described by this <see cref="ExportDefinition"/>.
        /// </summary>
        public Func<ExportProvider, object> Factory { get; private set; }

        #region Private Implementation

        private static IDictionary<string, object> GetMetadata(Type type)
        {
            return new Dictionary<string, object>
                       {
                           {
                               CompositionConstants.ExportTypeIdentityMetadataName,
                               AttributedModelServices.GetTypeIdentity(type)
                           },
                           {
                               IsFactoryExportMetadataName,
                               true
                           }
                       };
        }

        private static string GetContractName(Type type, string registrationName)
        {
            return registrationName ?? AttributedModelServices.GetContractName(type);
        }

        #endregion
    }
}

