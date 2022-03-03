using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace LargestNumberSolution
{
    public static class ChallengeClass
    {
        public static string ImplementThis(int[] numbers)
        {
            var returnList = new string[numbers.Length];
            for (int i = 0; i < returnList.Length; i++)
                returnList[i] = numbers[i].ToString();
            Array.Sort(returnList, new DigitComparer());

            return string.Join("", returnList);
        }

        public class DigitComparer : IComparer<string>
        {
            public int Compare(string A, string B)
            {
                return (A + B).CompareTo(B + A) <= 0 ? 1 : -1; //If A + B is bigger, it comes first, so return 1
            }
        }

        public static string ProduceInputFile()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(MeasureMethod(new int[] { 10, 2 }));
            str.AppendLine(MeasureMethod(new int[] { 3, 30, 34, 5, 9 }));
            str.AppendLine(MeasureMethod(new int[] { 3, 30, 34, 563, 125, 5, 9, 99, 874, 100, 247 }));
            str.AppendLine(MeasureMethod(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }));

            return str.ToString();
        }

        private static string MeasureMethod(int[] input)
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

            return $"{ticks}|{string.Join(",", input)}|{output}";
        }
    }
}