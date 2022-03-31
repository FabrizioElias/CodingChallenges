using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AlternatingStringSolution
{
    public static class ChallengeClass
    {
        public static int ImplementThis(string input)
        {
            if (input.Length == 1) //Se o tamanho é 1, nunca terá caracteres repetidos
                return 0;

            var allPairs = new HashSet<string>();
            //Vamos coletar todos os pares de letras usando o poder do HashSet percorrendo a string inteira com 2 ponteiros
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 1; j < input.Length; j++)
                {
                    allPairs.Add(input[i].ToString() + input[j].ToString());
                }
            }

            //Valor a ser retornado
            var maxLength = 0;
            foreach (var pair in allPairs)
            {
                //Vamos checar cada par no HashSet para determinar qual a maior string.
                var replaced = input;
                foreach (var letter in input)
                {
                    if (!pair.Contains(letter)) //Removemos todos os caracteres que não estão no par
                        replaced = replaced.Replace(letter.ToString(), "");
                }

                //Se o que sobra tem 1 ou nenhuma letra, passamos para o próximo par
                //Assim como se o que sobrar for menor que a maior string ja identificada, não precisamos checar pois mesmo que seja válida, o tamanho nunca será maior.
                if (replaced.Length <= 1 || replaced.Length <= maxLength)
                {
                    continue;
                }

                var lastChecked = replaced[0];
                var invalid = false;
                for (int i = 1; i < replaced.Length; i++)
                {
                    //Como replaced contém apenas letras que estão no par sendo checado, podemos
                    //checar cada letra para saber se ela é igual a anterior.
                    //Se for, a string não é valida e podemos checar o próximo par (ou retornar o que temos no maxLength)
                    if (replaced[i] == lastChecked)
                    {
                        invalid = true;
                        break;
                    }
                    else
                    {
                        //Atualizamos a última letra checada poís a próxima deve ser diferente
                        lastChecked = replaced[i];
                    }
                }

                //Caso a string seja válida, apenas checamos se ela é a maior ou não
                if (!invalid)
                    maxLength = replaced.Length > maxLength ? replaced.Length : maxLength;
            }

            return maxLength;
        }

        public static string ProduceInputFile(List<RunnerResult<string, int>> inputs)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < inputs.Count; i++)
            {
                var input = inputs[i];
                Utility.ReportProgress(i, inputs.Count, input.Input, 2);
                str.AppendLine(MeasureMethod(input.Input));
            }

            Utility.ClearReport(2);
            return str.ToString();
        }

        private static string MeasureMethod(string[] input)
        {
            var stopwatch = new Stopwatch();

            var ticks = 0L;
            var output = 0;
            var accummulatedTime = TimeSpan.Zero;
            var totalRuns = 250;
            output = ImplementThis(input[0]);
            for (int i = 0; i < totalRuns; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis(input[0]);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                    Utility.ReportProgress(i, totalRuns, input);
                }
                finally
                {
                    stopwatch.Reset();
                }
            }

            ticks = (accummulatedTime / totalRuns).Ticks;

            return $"{ticks}|{input}|{output}";
        }

        public static async Task<List<RunnerResult<string, int>>> ParseInputFile(string inputFilePath)
        {
            var inputLines = await File.ReadAllLinesAsync(inputFilePath);
            var results = inputLines.Select(x =>
            {
                var split = x.Split("|");
                return new RunnerResult<string, int>
                {
                    TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
                    Input = new string[] { split[1] }, //This type must be the same as the type in ImplementThis,
                    ExpectedOutput = int.Parse(split[2])
                };
            }).ToList();

            return results;
        }
    }
}