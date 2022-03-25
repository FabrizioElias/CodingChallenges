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
            string xpto(string input)
            {
                string output = input;
                string tempOutput;
                do
                {
                    tempOutput = output;
                    output = RemoveCaracteresDuplos(output);
                }
                while (tempOutput != output);
                return output;
            }

            var inputUnit = xpto(input);
            int i = -1;

            var count = 0;
            while (count < 10)
            {
                count++;
                inputUnit.Replace(inputUnit[0].ToString(), "");
                if (EhFinal(inputUnit) == 0)
                {
                    i = inputUnit.Length;
                    break;
                }
                inputUnit = xpto(input);
            }

            string RemoveCaracteresDuplos(string input)
            {
                string[] listaDeChar = input.Select(a => a.ToString()).ToArray();
                string temp;
                for (int i = 0; i < listaDeChar.Length - 1; i++)
                {
                    if (listaDeChar[i] == listaDeChar[i + 1])
                    {
                        temp = input.Replace(listaDeChar[i], "");
                        return temp;
                    }
                }
                return input;
            }

            int EhFinal(string input)
            {
                List<string> ListaDeCaracteres = new List<string>();

                for (int i = 0; i < input.Length; i++)
                {
                    if (!ListaDeCaracteres.Contains(input[i].ToString()))
                        ListaDeCaracteres.Add(input[i].ToString());
                }
                string temp = "";

                foreach (var s in ListaDeCaracteres)
                {
                    temp = temp + s;
                }
                var x = input.Replace(temp, "");

                return x.Length;
            }

            return i;
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