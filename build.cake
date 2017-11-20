///////////////////////////////////////////////////////////////////////////////
// USINGS
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// SETTINGS
///////////////////////////////////////////////////////////////////////////////

var platforms = new [] { "AnyCPU", "x64", "x86" }.ToList();
var configurations = new [] { "Debug", "Release" }.ToList();
var solution = "./LanguageEditor.sln";
var artifactsDir = (DirectoryPath)Directory("./artifacts");
var binDir = (DirectoryPath)Directory("./src/bin");
var objDir = (DirectoryPath)Directory("./src/obj");

///////////////////////////////////////////////////////////////////////////////
// RELEASE
///////////////////////////////////////////////////////////////////////////////

var isAppVeyorBuild = AppVeyor.IsRunningOnAppVeyor;
var isLocalBuild = BuildSystem.IsLocalBuild;
var isPullRequest = BuildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
var isMainRepo = StringComparer.OrdinalIgnoreCase.Equals("wieslawsoltes/LanguageEditor", BuildSystem.AppVeyor.Environment.Repository.Name);
var isMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", BuildSystem.AppVeyor.Environment.Repository.Branch);
var isTagged = BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag 
               && !string.IsNullOrWhiteSpace(BuildSystem.AppVeyor.Environment.Repository.Tag.Name);
var isRelease =  !isLocalBuild && !isPullRequest && isMainRepo && isMasterBranch && isTagged;

///////////////////////////////////////////////////////////////////////////////
// VERSION
///////////////////////////////////////////////////////////////////////////////

var suffix = (isRelease || !isAppVeyorBuild) ? "" : "-build" + EnvironmentVariable("APPVEYOR_BUILD_NUMBER") + "-alpha";
var version = ParseAssemblyInfo(File("./src/Properties/AssemblyInfo.cs")).AssemblyVersion;

Information("Release Version: {0}", version + suffix);

///////////////////////////////////////////////////////////////////////////////
// ACTIONS
///////////////////////////////////////////////////////////////////////////////

var buildSolutionAction = new Action<string,string,string> ((solution, configuration, platform) => 
{
    Information("Building: {0}, {1} / {2}", solution, configuration, platform);
    MSBuild(solution, settings => {
        settings.SetConfiguration(configuration);
        settings.WithProperty("Platform", "\"" + platform + "\"");
        settings.SetVerbosity(Verbosity.Minimal); });
});

var packageBinariesAction = new Action<string,string> ((configuration, platform) => 
{
    var path = "./src/bin/" + platform + "/" + configuration + "/";
    var output = "LanguageEditor-" + version + suffix + "-" + platform + (configuration == "Release" ? "" : ("-(" + configuration + ")"));
    var outputDir = artifactsDir.Combine(output);
    var outputZip = artifactsDir.CombineWithFilePath(output + ".zip");
    CleanDirectory(outputDir);
    CopyFileToDirectory(File(path + "LanguageEditor.exe"), outputDir);
    CopyFileToDirectory(File(path + "LanguageEditor.config"), outputDir);
    CopyFileToDirectory(File(path + "app.manifest"), outputDir);
    CopyFileToDirectory(File("./LICENSE.TXT"), outputDir);
    CopyFileToDirectory(File("./README.md"), outputDir);
    Zip(outputDir, outputZip);
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectory(binDir);
    CleanDirectory(objDir);
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    configurations.ForEach(configuration => platforms.ForEach(platform => buildSolutionAction(solution, configuration, platform)));
});

Task("Package-Binaries")
    .IsDependentOn("Build")
    .Does(() =>
{
    configurations.ForEach(configuration => platforms.ForEach(platform => packageBinariesAction(configuration, platform)));
});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Package")
  .IsDependentOn("Package-Binaries");

Task("Default")
  .IsDependentOn("Build");

///////////////////////////////////////////////////////////////////////////////
// EXECUTE
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
