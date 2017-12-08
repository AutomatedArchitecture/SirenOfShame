#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var platform = Argument("platform", "x86");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./SirenOfShame.WixSetup/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./SirenOfShame.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    // Use MSBuild
    MSBuild("./SirenOfShame.sln", settings => {
		settings.WithProperty("Platform", platform);
		settings.SetConfiguration(configuration);
	});
});

Task("MakeMsi")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
{
	// Use MSBuild
	MSBuild("./SirenOfShame.WixSetup/SirenOfShame.WixSetup.wixproj", settings =>
	{
		settings.WithProperty("Platform", platform);
		settings.SetConfiguration(configuration);
	});
});

Task("SignMsi")
	.IsDependentOn("MakeMsi")
	.Does(() =>
{
	var file = new FilePath("./SirenOfShame.WixSetup/bin/Release/SirenOfShame.WixSetup.msi");
	Sign(file, new SignToolSignSettings {
		CertSubjectName = "Open Source Developer, Lee RICHARDSON",
		TimeStampUri = new Uri("http://time.certum.pl"),
		DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
		Description = "Siren of Shame",
		DescriptionUri = new Uri("https://sirenofshame.com")
	});
});

Task("Publish")
	.IsDependentOn("SignMsi");

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
