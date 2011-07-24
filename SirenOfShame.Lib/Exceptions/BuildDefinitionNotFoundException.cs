using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Exceptions
{
    public class BuildDefinitionNotFoundException : Exception
    {
        public BuildDefinitionSetting BuildDefinitionSetting { get; set; }

        public BuildDefinitionNotFoundException(BuildDefinitionSetting buildDefinitionSetting)
        {
            BuildDefinitionSetting = buildDefinitionSetting;
        }
    }
}
