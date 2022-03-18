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
            string vs = inputText;
            List<List<string>> list = new List<List<string>>();

            for (int j = 1; j < vs.Length - 1; j++)
            {
                List<string> sublist = new List<string>();
                for (int i = 0; i < vs.Length - 1; i++)
                {
                    if (i + j < vs.Length)
                    {
                        sublist.Add(vs.Substring(i, j));
                    }
                }
                list.Add(sublist);
            }
            int cont = 0;
            foreach (List<string> lis in list) //{mo,om,mo}
            {
                foreach (string si in lis)//{abc}
                {
                    foreach (string ssi in lis) //{cba}
                    {
                        cont = cont + Func(si, ssi);
                    }
                }
            }
            static int Func(string si, string ssi)
            {
                char[] vs = si.ToCharArray();
                char[] vss = ssi.ToCharArray();
                char[] test = new char[vs.Length];
                bool saida = true;
                for (int i = 0; i < vs.Length; i++)
                {
                    for (int j = 0; j < vss.Length; j++)
                    {
                        if (vs[i] == vss[j])
                        {
                            saida = false;
                        }
                    }
                }
                return saida ? 1 : 0;
            }
            return cont;
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