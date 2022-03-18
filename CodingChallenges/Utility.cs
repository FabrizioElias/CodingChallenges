namespace CodingChallenges
{
    public static class Utility
    {
        public static void ReportProgress(int current, double total, object? input)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var heigth = Console.WindowHeight;
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorLeft = 0;
            Console.CursorTop = heigth - 2;

            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorLeft = 0;
            Console.Write($"{current / total:P2}          ");
            if (input != null)
            {
                Console.WriteLine();
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;
                Console.Write($"{input}          ");
            }

            Console.ForegroundColor = color;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }
    }
}