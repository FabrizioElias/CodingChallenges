using CodingChallenges;
using System.Diagnostics;
using System.Reactive.Linq;

var challengeName = "Test";

var sourcesPath = Path.Combine(Environment.CurrentDirectory, "Sources", challengeName);
var inputFile = Path.Combine(Environment.CurrentDirectory, "Input", challengeName, "inputs.txt");

Console.WriteLine($"Running from: {Environment.CurrentDirectory}");
Console.WriteLine($"Sources from: {sourcesPath}");
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
            new string[] { split[1] } //This type must be the same as the type in ImplementThis
        }
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
    } while (key.Key == ConsoleKey.R);
}

void Run(string filepath)
{
    var stopwatch = new Stopwatch();
    var compiledAssembly = compiler.Compile(filepath);
    foreach (var result in results)
    {
        var accummulatedTime = TimeSpan.Zero;
        Console.WriteLine($"############## Running 5x with input {result.PrintInput()}");
        for (int i = 0; i < 5; i++)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    Console.WriteLine($"##############");
                    stopwatch.Start();
                    result.Output = runner.Execute(compiledAssembly, result.Inputs);
                    stopwatch.Stop();
                    Console.WriteLine($"##############");
                    accummulatedTime += stopwatch.Elapsed;
                    result.Completed = true;
                }
                catch
                {
                    Console.WriteLine($"Error running code with input {result.PrintInput()}. Edit and try again.");
                    result.Completed = false;
                }
                finally
                {
                    stopwatch.Reset();
                }
            });

            thread.Start();
            var hasRunWithinTime = thread.Join(TimeSpan.FromSeconds(5));
            if (!hasRunWithinTime)
            {
                Console.WriteLine("Code has not finished within 5 seconds.");
                result.Completed = false;
                break;
            }
        }
        if (result.Completed)
        {
            result.ElapsedTime = accummulatedTime / 5;
            Console.WriteLine(result.Print());
        }

        Console.WriteLine("##############");
    }
}
