using System;
using System.Collections.Generic;
using System.Linq;
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

        public string EncryptedPassword { get; set; }

        public void SetPassword(string value)
        {
            EncryptedPassword = new TripleDESStringEncryptor().EncryptString(value);
        }

        public string GetPassword()
        {
            return new TripleDESStringEncryptor().DecryptString(EncryptedPassword);
        }

        public ICiEntryPoint GetCiEntryPoint(SirenOfShameSettings settings)
        {
            return settings.CiEntryPoints.First(s => s.Name == Name);
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
