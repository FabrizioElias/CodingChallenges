namespace CodingChallenges
{
    public static class Utility
    {
        public static void ReportProgress(int current, double total, object? input, int upperOffset = 0)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var heigth = Console.WindowHeight;
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorLeft = 0;
            Console.CursorTop = heigth - 2 - upperOffset;

            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorLeft = 0;
            var progress = $"{current / total:P2} [";
            var progressBarTotalWidth = Console.WindowWidth - progress.Length - 1;
            var progressBarWidth = Convert.ToInt32(Math.Round(progressBarTotalWidth * (current / total * 100) / 100));
            var remainingProgressBarWidth = progressBarTotalWidth - progressBarWidth;
            Console.Write(progress + new string('#', progressBarWidth) + new string('_', remainingProgressBarWidth) + "]");
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

        public static void ClearReport(int upperOffset = 0)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var heigth = Console.WindowHeight;
            var color = Console.ForegroundColor;
            var blankString = new string(' ', Console.WindowWidth);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorLeft = 0;
            Console.CursorTop = heigth - 2 - upperOffset;

            for (int i = 0; i < upperOffset; i++)
            {
                Console.WriteLine(blankString);
            }

            Console.WriteLine(blankString);
            Console.Write(blankString);

            Console.ForegroundColor = color;
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }
    }
}