using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AlternatingString
{
    public static class ChallengeClass
    {
        public static int ImplementThis(string input)
        {
            return 0;
        }

        public static ThreadResult<int> ChallengeRunner(string input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            var output = ImplementThis(input);
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
            return new ThreadResult<int> { Output = output, ElapsedTime = accummulatedTime / 5000 };
        }
    }
}