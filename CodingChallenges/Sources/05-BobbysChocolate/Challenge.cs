using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BobbysChocolate
{
    public static class ChallengeClass
    {
        public static int ImplementThis(int n, int c, int m)
        {
            return 0;
        }

        public static ThreadResult<int> ChallengeRunner(int[] input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            var output = ImplementThis(input[0], input[1], input[2]);
            var totalRuns = 200;
            for (int i = 0; i < totalRuns; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis(input[0], input[1], input[2]);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                }
                finally
                {
                    stopwatch.Reset();
                }
            }
            return new ThreadResult<int> { Output = output, ElapsedTime = accummulatedTime / totalRuns };
        }
    }
}