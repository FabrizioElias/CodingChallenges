using CodingChallenges;

var challengeName = "04-AlternatingString";

var inputFile = Path.Combine(Environment.CurrentDirectory, "Input", challengeName, "inputs.txt");
var solutionFn = AlternatingStringSolution.ChallengeClass.ProduceInputFile;
var challengeFn = AlternatingString.ChallengeClass.ChallengeRunner;
var results = await AlternatingStringSolution.ChallengeClass.ParseInputFile(inputFile);
var summaryFn = RunnerResult<string, int>.PrintSummary;
var output = new ThreadResult<int>();


ConsoleKeyInfo key = new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false);

do
{
    Console.WriteLine(challengeName);
    Console.WriteLine("Press any key to exit or press R to run now (S to solution)!");
    key = Console.ReadKey(true);
    Console.Clear();
    Console.CursorLeft = 0;
    Console.CursorTop = 0;
    if (key.Key == ConsoleKey.R)
    {
        Console.WriteLine("***** Running Challenge directly *****");
        Run();
    }
    if (key.Key == ConsoleKey.S)
    {
        Console.WriteLine("***** Running solution *****");
        Console.WriteLine(solutionFn(results));
    }
} while (key.Key == ConsoleKey.R || key.Key == ConsoleKey.S);

void Run()
{
    foreach (var result in results)
    {
        var elapsedTime = TimeSpan.Zero;
        output.Clear();
        var thread = new Thread(() =>
        {
            try
            {
                output = challengeFn(result.Input);
                output.Completed = true;
                elapsedTime += output.ElapsedTime;
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
        if (output?.Completed ?? false)
        {
            result.ElapsedTime = elapsedTime;
            result.Output = output.Output;
            result.Completed = true;
        }
    }
    Console.WriteLine("Done! See (r)esults or (s)ummary?");
    var key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.R)
    {
        foreach (var result in results)
            Console.WriteLine(result.Print());
    }
    if (key.Key == ConsoleKey.S)
    {
        Console.WriteLine(summaryFn(results));
    }
}