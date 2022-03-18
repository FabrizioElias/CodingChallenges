using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SherlockAnagram
{
    public static class ChallengeClass
    {
        public static int ImplementThis(string inputText)
        {
            return 0;
        }

        public static ThreadResult ChallengeRunner(dynamic input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            dynamic? output = null;
            output = ChallengeClass.ImplementThis(input);
            var runs = 50;
            for (int i = 0; i < runs; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ChallengeClass.ImplementThis(input);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                }
                finally
                {
                    Utility.ReportProgress(i, runs, input);
                    stopwatch.Reset();
                }
            }
            return new ThreadResult { Output = output, ElapsedTime = accummulatedTime / runs };
        }
    }
}