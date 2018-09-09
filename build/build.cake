#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"
#tool ReSharperReports
#addin Cake.ReSharperReports

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
var buildArtifactsFolder = "../artifacts";
GitVersion version = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    // Executed BEFORE the first task.
    Information("Running tasks...");

    CleanDirectory(buildArtifactsFolder);

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

    version = GitVersion(settings); // version variable only set if not a CI build
    if ( !isCiBuild ){
        Information($"Semantic version: {version.FullSemVer}");
    }
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

Task("Find-Duplicates")
    .Does(() =>
{
    DupFinder(solution, new DupFinderSettings{
        ExcludePattern = new String[]{ "../**/*Designer.cs" },
        OutputFile = buildArtifactsFolder + "/analysis/dupfinder-output.xml",
        ThrowExceptionOnFindingDuplicates = false
    });
    ReSharperReports(buildArtifactsFolder + "/analysis/dupfinder-output.xml",
        buildArtifactsFolder + "/analysis/dupfinder-output.html");
});

Task("Code-Inspections")
    .Does(() =>
{
    InspectCode(solution, new InspectCodeSettings{
        SolutionWideAnalysis = true,
        OutputFile = buildArtifactsFolder + "/analysis/inspectcode-output.xml",
        ThrowExceptionOnFindingViolations = false
    });
    ReSharperReports(buildArtifactsFolder + "/analysis/inspectcode-output.xml",
        buildArtifactsFolder + "/analysis/inspectcode-output.html");
});

Task("Default")
    .IsDependentOn("Restore-Packages")
    .IsDependentOn("Build-Solution");

Task("CI")
    .IsDependentOn("Version")
    .IsDependentOn("Default")
    .IsDependentOn("Code-Inspections")
    .IsDependentOn("Find-Duplicates");

RunTarget(target);
