using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class CiEntryPointSetting
    {
        public CiEntryPointSetting()
        {
            BuildDefinitionSettings = new List<BuildDefinitionSetting>();
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

// ReSharper disable once MemberCanBePrivate.Global
        public string EncryptedPassword { get; set; }

        /// <summary>
        /// Note: At the moment this setting only applies to TFS.
        /// </summary>
        public bool ApplyBuildQuality { get; set; }

        /// <summary>
        /// Note: At the moment this setting only applies to TFS.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Note: At the moment this setting only applies to TFS.
        /// </summary>
        public bool IgnoreEmptyCommentsInXamlBuilds { get; set; }

        /// <summary>
        /// Note: At the moment this only applies to Jenkins/Hudson
        /// </summary>
        public bool TreatUnstableAsSuccess { get; set; }
        
        public void SetPassword(string value)
        {
            EncryptedPassword = new TripleDesStringEncryptor().EncryptString(value);
        }

        public string GetPassword()
        {
            return new TripleDesStringEncryptor().DecryptString(EncryptedPassword);
        }

        public ICiEntryPoint GetCiEntryPoint(SirenOfShameSettings settings)
        {
            var ciEntryPoint = settings.CiEntryPoints.FirstOrDefault(s => s.Name == Name);
            if (ciEntryPoint == null) throw new SosException("Unable to find plugin: " + Name);
            return ciEntryPoint;
        }

        public List<BuildDefinitionSetting> BuildDefinitionSettings { get; set; }

        public BuildDefinitionSetting FindAddBuildDefinition(MyBuildDefinition buildDefinition, string buildServer)
        {
            BuildDefinitionSetting buildDefinitionSetting = BuildDefinitionSettings.FirstOrDefault(bd => bd.Id == buildDefinition.Id);
            if (buildDefinitionSetting != null) return buildDefinitionSetting;
            buildDefinitionSetting = new BuildDefinitionSetting(buildDefinition, buildServer);
            BuildDefinitionSettings.Add(buildDefinitionSetting);
            return buildDefinitionSetting;
        }

        public BuildDefinitionSetting GetBuildDefinition(string buildDefinitionId)
        {
            return BuildDefinitionSettings.First(bd => bd.Id == buildDefinitionId);
        }

        public virtual WatcherBase GetWatcher(SirenOfShameSettings settings)
        {
            var ciEntryPoint = GetCiEntryPoint(settings);
            return ciEntryPoint.GetWatcher(settings);
        }
    }
}
