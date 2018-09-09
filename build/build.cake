#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"
#tool ReSharperReports
#addin Cake.ReSharperReports
#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression

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
    var appFolder = buildArtifactsFolder + "/app";
    if (!DirectoryExists(appFolder)){
        CreateDirectory(appFolder);
    }
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
        GitVersion(settings); // Need to run it twice for allow the variable to be populated
    }

    version = GitVersion();

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

Task("Package-For-Download")
    .IsDependentOn("Version")
    .Does(() =>
{
    var appVersion =  version.FullSemVer;
    ZipCompress("../src/SpotifyLyricsViewer/bin/" + configuration + "/SpotifyLyricsViewer.exe",
        buildArtifactsFolder + "/app/SpotifyViewer." + appVersion + ".zip");
});

Task("Default")
    .IsDependentOn("Restore-Packages")
    .IsDependentOn("Build-Solution");

Task("CI")
    .IsDependentOn("Version")
    .IsDependentOn("Default")
    .IsDependentOn("Code-Inspections")
    .IsDependentOn("Find-Duplicates")
    .IsDependentOn("Package-For-Download");

RunTarget(target);
