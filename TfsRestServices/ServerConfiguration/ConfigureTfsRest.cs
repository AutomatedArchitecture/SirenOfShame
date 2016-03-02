using SirenOfShame.Lib.ServerConfiguration;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices.ServerConfiguration
{
    public partial class ConfigureTfsRest : ConfigureServerBase
    {
        public ConfigureTfsRest() { }

        public ConfigureTfsRest(SirenOfShameSettings sosSettings, TfsRestCiEntryPoint ciEntryPoint,
            CiEntryPointSetting ciEntryPointSetting)
            : base(sosSettings)
        {
            
        }

    }
}
