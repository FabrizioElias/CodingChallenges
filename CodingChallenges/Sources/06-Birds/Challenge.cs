using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BirdsChocolate
{
    public static class ChallengeClass
    {
        public static int ImplementThis(List<int> arr)
        {
            return 0;
        }

        public static ThreadResult<int> ChallengeRunner(List<int> input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            var output = ImplementThis(input);
            var totalRuns = 20;
            for (int i = 0; i < totalRuns; i++)
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
            return new ThreadResult<int> { Output = output, ElapsedTime = accummulatedTime / totalRuns };
        }
    }
}