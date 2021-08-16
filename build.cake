//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

string target = Argument("target", "Default");
string configuration = Argument("configuration", "Debug");
string package = Argument("package", "*");
string nugetApiKey = Argument("nugetApiKey", "");
bool check = Argument<bool>("check", false);
string framework = Argument("framework", "netcoreapp3.0");
string versionSuffix = Argument("versionSuffix", "");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

string sourceDirectory = "./src";
string outputDirectory = "./out";
EnsureDirectoryExists(outputDirectory);

string testResultsDirectory = $"{outputDirectory}/TestResults";
string testLogger = "html";

List<string> solutions = GetFiles($"{sourceDirectory}/{package}/{package}.sln")
    .Select(file => file.FullPath)
    .ToList();

var tests = GetFiles($"{sourceDirectory}/{package}/{package}.sln").ToList();

var restoreSettings = new DotNetCoreRestoreSettings 
{
    PackagesDirectory = Directory(sourceDirectory) + Directory("packages"),
    Verbosity = DotNetCoreVerbosity.Normal
};

var cleanSettings = new DotNetCoreCleanSettings
{
    Configuration = configuration,
    ArgumentCustomization = args => args.Append("--nologo")
};

var buildSettings = new DotNetCoreBuildSettings 
{
    Configuration = configuration,
    NoRestore = true,
    ArgumentCustomization = args => args.Append("--nologo")
};

var testSettings = new DotNetCoreTestSettings 
{
    Configuration = configuration,
    NoBuild = true,
    NoRestore = true,
    ResultsDirectory = testResultsDirectory,
    Logger = testLogger,
    ArgumentCustomization = args => args.Append("--nologo")
};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Restore")
    .Does(() => 
    {
        foreach (string solution in solutions)
        {
            System.Console.WriteLine(solution);
            DotNetCoreRestore(solution, restoreSettings);
        }
    });

Task("Clean")
    .IsDependentOn("Restore")
    .Does(() => 
    {
        CleanDirectory(outputDirectory);
        foreach (string solution in solutions)
        {
            DotNetCoreClean(solution, cleanSettings);
        }
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => 
    {
        foreach (string solution in solutions)
        {
            DotNetCoreBuild(solution, buildSettings);
        }
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => 
    {
        CleanDirectories(new []{testResultsDirectory});

        FilePathCollection projects = GetFiles($"{sourceDirectory}/{package}/*.Tests*/*.csproj");
        foreach (FilePath project in projects)
        {
            Information($"Testing {project}...");
            DotNetCoreTest(project.FullPath, testSettings);
            Information($"Finished testing {project}.");
        }
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
