using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SherlockString
{
    public static class ChallengeClass
    {
        public static string ImplementThis(string s)
        {
            return "solution";
        }

        public static ResultClass ChallengeRunner(dynamic input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            dynamic? output = null;
            output = ChallengeClass.ImplementThis(input);
            for (int i = 0; i < 5000; i++)
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
                    stopwatch.Reset();
                }
            }
            return new ResultClass { Output = output, ElapsedTime = accummulatedTime / 5000 };
        }
    }
}