#tool "nuget:?package=GitVersion.CommandLine"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");


///////////////////////////////////////////////////////////////////////////////
// Variables
///////////////////////////////////////////////////////////////////////////////

var isCiBuild = !string.IsNullOrWhiteSpace(EnvironmentVariable("BUILD_NUMBER"));
var solution = "../SpotifyLyrics.sln";
GitVersion version = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Version")
    .Does(() =>
{
    var settings = new GitVersionSettings {
        UpdateAssemblyInfo = isCiBuild
    };

    if ( isCiBuild ) {
        settings.OutputType = GitVersionOutput.BuildServer;
    }

    version = GitVersion(settings);

    Information($"Semantic version: {version.FullSemVer}");
});

Task("Restore-Packages")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Build-Solution")
    .Does(() =>
{
    var settings = new MSBuildSettings()
        .SetConfiguration(configuration)
        .WithTarget("Rebuild");

    MSBuild(solution, settings);
});

Task("Default")
    .IsDependentOn("Restore-Packages")
    .IsDependentOn("Build-Solution");

Task("CI")
    .IsDependentOn("Version")
    .IsDependentOn("Default");

RunTarget(target);
