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
            var letras = new Dictionary<string, int>();

            if (string.IsNullOrEmpty(s))
                return "NO";

            for (int i = 0; i < s.Length; i++)
            {
                int cont = 0;

                for (int j = 0; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                    {
                        cont++;
                    }
                }
                if (!letras.ContainsKey(s[i].ToString()))
                {
                    letras.Add(s[i].ToString(), cont);
                }

                if (i > 0 && letras[s[i].ToString()] != letras[s[i - 1].ToString()] && letras[s[i].ToString()] != letras[s[i - 1].ToString()] + 1)
                {
                    return "NO";
                }
            }
            return "YES";
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