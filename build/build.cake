///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solution = "../SpotifyLyrics.sln";

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

RunTarget(target);
