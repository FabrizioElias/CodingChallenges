using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NumberToTextSolution
{
    public static class ChallengeClass
    {
        public static string ImplementThis(decimal inputText)
        {
            return "NO";
        }

        public static string ProduceInputFile(List<RunnerResult<decimal, string>> inputs)
        {
            StringBuilder str = new StringBuilder();
            foreach (var input in inputs)
            {
                str.AppendLine(MeasureMethod(input.Input));
            }

            return str.ToString();
        }

        private static string MeasureMethod(decimal input)
        {
            var stopwatch = new Stopwatch();

            var ticks = 0L;
            var output = "";
            var accummulatedTime = TimeSpan.Zero;
            output = ImplementThis(input);
            for (int i = 0; i < 5000; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis(input);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                }
                finally
                {
                    stopwatch.Reset();
                }
            }

            ticks = (accummulatedTime / 5000).Ticks;

            return $"{ticks}|{input}|{output}";
        }

        public static async Task<List<RunnerResult<decimal, string>>> ParseInputFile(string inputFilePath)
        {
            var inputLines = await File.ReadAllLinesAsync(inputFilePath);
            var results = inputLines.Select(x =>
            {
                var split = x.Split("|");
                return new RunnerResult<decimal, string>
                {
                    TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
                    Input = decimal.Parse(split[1]), //This type must be the same as the type in ImplementThis,
                    ExpectedOutput = split[2]
                };
            }).ToList();

            return results;
        }
    }
}