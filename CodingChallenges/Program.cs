using CodingChallenges;
using System.Diagnostics;
using System.Reactive.Linq;

var challengeName = "SherlockString";

var sourcesPath = Path.Combine(Environment.CurrentDirectory, "Sources", challengeName);
var inputFile = Path.Combine(Environment.CurrentDirectory, "Input", challengeName, "inputs.txt");

Console.WriteLine($"Running {challengeName} from: {Environment.CurrentDirectory}");
Console.WriteLine($"Sources from: {sourcesPath}");
Console.WriteLine($"Input from: {inputFile}");
Console.WriteLine("Modify the sources to compile and run it!");

var compiler = new Compiler();
var runner = new Runner();
var inputLines = await File.ReadAllLinesAsync(inputFile);
var results = inputLines.Select(x =>
{
    var split = x.Split("|");
    return new RunnerResult
    {
        TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
        Inputs = new object[]
        {
            split[1].Split(",").Select(x => int.Parse(x)).ToArray() //This type must be the same as the type in ImplementThis
        },
        ExpectedOutput = split[2]
    };
});

using (var watcher = new ObservableFileSystemWatcher(c => { c.Path = sourcesPath; }))
{
    var changes = watcher.Changed.Throttle(TimeSpan.FromSeconds(.5)).Where(c => c.FullPath.EndsWith(@"Challenge.cs")).Select(c => c.FullPath);

    changes.Subscribe(Run);

    watcher.Start();

    ConsoleKeyInfo key = new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false);

    do
    {
        Console.WriteLine("Press any key to exit or press R to run now!");
        key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.R)
            Run(Path.Combine(sourcesPath, "Challenge.cs"));
        if (key.Key == ConsoleKey.S)
        {
            Console.WriteLine("***** Running solution *****");
            Console.WriteLine(SherlockStringSolution.ChallengeClass.ProduceInputFile());
        }
    } while (key.Key == ConsoleKey.R || key.Key == ConsoleKey.S);
}

void Run(string filepath)
{
    var compiledAssembly = compiler.Compile(filepath);
    foreach (var result in results)
    {
        var elapsedTime = TimeSpan.Zero;
        Console.WriteLine($"############## Running 5x with input {result.PrintInput()}");
        var thread = new Thread(() =>
        {
            try
            {
                Console.WriteLine($"##############");
                var output = runner.Execute(challengeName, compiledAssembly, result.Inputs);
                result.Output = output.Result;
                elapsedTime += output.elapsedTime;
                result.Completed = true;
            }
            catch
            {
                Console.WriteLine($"Error running code with input {result.PrintInput()}. Edit and try again.");
                result.Completed = false;
            }
        });

        thread.Start();
        var hasRunWithinTime = thread.Join(TimeSpan.FromSeconds(10));
        if (!hasRunWithinTime)
        {
            Console.WriteLine("Code has not finished within 10 seconds.");
            result.Completed = false;
            break;
        }
        if (result.Completed)
        {
            result.ElapsedTime = elapsedTime;
            Console.WriteLine(result.Print());
        }

        Console.WriteLine("##############");
    }
}
