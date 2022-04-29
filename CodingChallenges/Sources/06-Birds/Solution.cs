using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace BirdsSolution
{
    public static class ChallengeClass
    {
        /*
         * Given an array of bird sightings where every element represents a bird type id,
         * determine the id of the most frequently sighted type. If more than 1 type has been spotted that maximum amount,
         * return the smallest of their ids.
         *
         * Example: arr = [1, 1, 2, 2, 3] (entre 1 e 5, contém de 5 até 10000)
         * There are two each of types 1 and 2, and one sighting of type 3. Pick the lower of the two types seen twice: type 1.
         */

        public static int ImplementThis_NoLinq(int[] arr)
        {
            var sigthings = new Bird[5];
            for (int i = 0; i < sigthings.Length; i++)
            {
                sigthings[i] = new Bird { Id = i + 1 };
            }
            foreach (var bird in arr)
            {
                sigthings[bird - 1].Count++;
            }
            var highSparrow = new Bird();
            foreach (var sight in sigthings)
            {
                if (highSparrow.Count < sight.Count)
                    highSparrow = sight;
            }
            return highSparrow.Id;
        }

        public class Bird
        {
            public int Id { get; set; }
            public int Count { get; set; }
        }

        public static int ImplementThis(int[] arr)
        {
            var counter = new Dictionary<int, int>();
            foreach (var bird in arr)
            {
                if (counter.ContainsKey(bird))
                    counter[bird] += 1;
                else
                    counter.Add(bird, 1);
            }

            var maxSigthings = counter.Max(kvp => kvp.Value);
            return counter.Where(kvp => kvp.Value == maxSigthings).Min(kvp => kvp.Key);
        }

        public static (string resultFile, List<string> wrongs, int corrects) ProduceInputFile(List<RunnerResult<int, int>> inputs)
        {
            StringBuilder str = new StringBuilder();
            var wrongs = new List<string>();
            var corrects = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                var input = inputs[i];
                var inputString = string.Join(", ", input.Input);
                Utility.ReportProgress(i, inputs.Count, inputString, 2);
                str.AppendLine(MeasureMethod(input));
                if (input.ExpectedOutput == input.Output)
                    corrects++;
                else
                {
                    wrongs.Add($"{inputString} was {input.Output} | should be {input.ExpectedOutput}");
                }
            }
            Utility.ClearReport(2);
            return (str.ToString(), wrongs, corrects);
        }

        private static string MeasureMethod(RunnerResult<int, int> input)
        {
            var stopwatch = new Stopwatch();

            var accummulatedTime = TimeSpan.Zero;
            var totalRuns = 1;
            var output = ImplementThis_NoLinq(input.Input);
            for (int i = 0; i < totalRuns; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis_NoLinq(input.Input);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                    Utility.ReportProgress(i, totalRuns, $"{string.Join(", ", input.Input)} -> {output}");
                }
                finally
                {
                    stopwatch.Reset();
                }
            }
            input.Output = output;

            var ticks = (accummulatedTime / totalRuns).Ticks;

            return $"{ticks}|{string.Join(", ", input.Input)}|{output}";
        }

        public static async Task<List<RunnerResult<int, int>>> ParseInputFile(string inputFilePath)
        {
            var inputLines = await File.ReadAllLinesAsync(inputFilePath);
            var results = inputLines.Select(x =>
            {
                var split = x.Split("|");
                var inputs = split[1].Split(" ");
                return new RunnerResult<int, int>
                {
                    TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
                    Input = inputs.Select(i => int.Parse(i.Trim())).ToArray(), //This type must be the same as the type in ImplementThis,
                    ExpectedOutput = int.Parse(split[2])
                };
            }).ToList();

            return results;
        }
    }
}