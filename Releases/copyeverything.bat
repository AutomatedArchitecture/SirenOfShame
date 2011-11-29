copy ..\SirenOfShame\bin\SirenOfShame.exe .\BuildArtifacts\%1
copy ..\SirenOfShame\bin\SirenOfShame.Lib.dll .\BuildArtifacts\%1
mkdir BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\BambooServices.dll .\BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\CruiseControlNetServices.dll .\BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\HudsonServices.dll .\BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\TeamCityServices.dll .\BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\TfsServices.dll .\BuildArtifacts\%1\Plugins
copy ..\SirenOfShame\bin\Plugins\UsbLib.dll .\BuildArtifacts\%1\Plugins