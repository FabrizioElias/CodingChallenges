using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LargestNumber
{
    public static class ChallengeClass
    {
        public static string ImplementThis(int[] numbers)
        {
            var resultList = new string[numbers.Length];
            for (int i = 0; i < resultList.Length; i++)
                resultList[i] = numbers[i].ToString();
            Array.Sort(resultList, new DigitComparer());
            if (resultList[0] == "0")
                return "0";
            return string.Join("", resultList);
        }

        public class DigitComparer : IComparer<string>
        {
            public int Compare(string A, string B)
            {
                return (A + B).CompareTo(B + A) <= 0 ? 1 : -1;
            }
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