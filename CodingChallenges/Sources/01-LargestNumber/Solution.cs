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
            if (numbers.All(x => x == 0))
                return "0";

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
            str.AppendLine(MeasureMethod(new int[] { 1 }));
            str.AppendLine(MeasureMethod(new int[] { 1000000000 }));
            str.AppendLine(MeasureMethod(new int[] { 1000000000, 1000000000 }));
            str.AppendLine(MeasureMethod(new int[] { 0, 1000000000 }));
            str.AppendLine(MeasureMethod(new int[] { 10, 2 }));
            str.AppendLine(MeasureMethod(new int[] { 3, 30, 34, 5, 9 }));
            str.AppendLine(MeasureMethod(new int[] { 3, 30, 34, 563, 125, 5, 9, 99, 874, 100, 247 }));
            str.AppendLine(MeasureMethod(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }));
            str.AppendLine(MeasureMethod(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 }));
            str.AppendLine(MeasureMethod(new int[] { 50, 49, 48, 47, 46, 45, 44, 43, 42, 41, 40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }));
            str.AppendLine(MeasureMethod(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
            str.AppendLine(MeasureMethod(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
            str.AppendLine(MeasureMethod(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 }));
            str.AppendLine(MeasureMethod(new int[] { 5634825, 83695202, 32696564, 26398721, 23214056, 23012502 }));
            str.AppendLine(MeasureMethod(new int[] { 569, 965 }));
            str.AppendLine(MeasureMethod(new int[] { 569, 965, 678, 876 }));
            str.AppendLine(MeasureMethod(new int[] { 3, 30, 34, 5, 9 }));
            str.AppendLine(MeasureMethod(new int[] { 9, 9, 9, 12358232 }));
            str.AppendLine(MeasureMethod(new int[] { 1, 1, 1, 92358232 }));

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