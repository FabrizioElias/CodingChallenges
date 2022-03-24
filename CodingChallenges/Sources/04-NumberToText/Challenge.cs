using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NumberToText
{
    public static class ChallengeClass
    {
        public static string ImplementThis(decimal s)
        {
            return "YES";
        }

        public static ThreadResult<string> ChallengeRunner(decimal input)
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
            return new ThreadResult<string> { Output = output, ElapsedTime = accummulatedTime / 5000 };
        }
    }
}