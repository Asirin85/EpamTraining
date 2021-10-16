using System.Linq;
using System.Xml.Linq;

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

var propGroup = XDocument.Parse(@"
    <PropertyGroup Label=""XXX"" Condition=""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"">
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
        <EnableNETAnalyzers>true</EnableNETAnalyzers>
        <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    </PropertyGroup>
").Root;

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
    .IsDependentOn("TreatWarningAsError")
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

Task("TreatWarningAsError")
    .Does(() => 
    {
        FilePathCollection projects = GetFiles($"{sourceDirectory}/{package}/*/*.csproj");
        foreach (FilePath project in projects)
        {
            var filePath = project.FullPath;
            XDocument doc = XDocument.Load(filePath);
            var hasAlreadyContainsLabel = doc.Root
                .DescendantNodes()
                .OfType<XElement>()
                .SelectMany(x => x.Attributes())
                .Select(x => (x.Name, x.Value))
                .Contains(("Label", "XXX"));

            if (hasAlreadyContainsLabel is false)
            {
                doc.Root.Add(propGroup);
                System.IO.File.WriteAllText(filePath, doc.ToString());
            }
        }
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
