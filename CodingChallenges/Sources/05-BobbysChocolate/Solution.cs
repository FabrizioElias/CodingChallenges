using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace BobbysChocolateSolution
{
    public static class ChallengeClass
    {
        /*
         * Complete the 'chocolateFeast' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. INTEGER n Bobby's initial amount of money 
         *  2. INTEGER c the cost of a chocolate bar 1 <= Bobby's initial amount of money <= 10000
         *  3. INTEGER m the number of wrappers he can turn in for a free bar 2 <= m <= Bobby's initial amount of money 
         */
        public static int ImplementThis_NonRecursive(int n, int c, int m)
        {
            var initialMoney = n;
            var cost = c;
            var wrapperCost = m;
            //Calcular quantos chocolates podemos comprar com dinheiro (divisão de inteiros, trunca -> 5 / 2 = 2)
            var chocolatesBougth = initialMoney / cost;
            //Se nenhum, já retorna
            if (chocolatesBougth == 0)
                return 0;

            //Quantas embalagens temos
            var currentWrappers = chocolatesBougth;
            //Quantos chocolates comemos
            var chocolatesEaten = chocolatesBougth;
            //Podemos comprar mais chocolates com embalagens?
            if (currentWrappers < wrapperCost)
                return chocolatesEaten;

            do
            {
                //Quantos chocolates compramos com embalagens
                var wrapperChocolates = currentWrappers / wrapperCost;
                //Quantas embalagens trocamos pelos chocolates
                currentWrappers -= (wrapperCost * wrapperChocolates);
                //Quantas embalagens ganhamos ao comer os novos chocolates
                currentWrappers += wrapperChocolates;
                chocolatesEaten += wrapperChocolates;
            } while (currentWrappers >= wrapperCost); //Enquanto tempos embalagens, vamos trocando

            return chocolatesEaten;
        }

        public static int ImplementThis(int n, int c, int m)
        {
            var initialMoney = n;
            var cost = c;
            var wrapperCost = m;
            //Calcular quantos chocolates podemos comprar com dinheiro (divisão de inteiros, trunca -> 5 / 2 = 2)
            var chocolatesBougth = initialMoney / cost;
            //Se nenhum, já retorna
            if (chocolatesBougth == 0)
                return 0;

            //Quantas embalagens temos
            var currentWrappers = chocolatesBougth;
            //Quantos chocolates comemos
            var chocolatesEaten = chocolatesBougth;
            //Podemos comprar mais chocolates com embalagens?
            if (currentWrappers < wrapperCost)
                return chocolatesEaten;

            chocolatesEaten += GetWrapperChocolates(currentWrappers, wrapperCost);

            return chocolatesEaten;
        }

        private static int GetWrapperChocolates(int ownedWrappers, int wrapperCost)
        {
            if (ownedWrappers < wrapperCost)
                return 0;
            var wrapperChocolates = ownedWrappers / wrapperCost;
            //Quantas embalagens trocamos pelos chocolates
            ownedWrappers -= (wrapperCost * wrapperChocolates);
            //Quantas embalagens ganhamos ao comer os novos chocolates
            ownedWrappers += wrapperChocolates;
            //Somar quantos chocolates comemos agora com o que comeremos
            return wrapperChocolates + GetWrapperChocolates(ownedWrappers, wrapperCost);
        }

        public static (string resultFile, List<string> wrongs, int corrects) ProduceInputFile(List<RunnerResult<int, int>> inputs)
        {
            StringBuilder str = new StringBuilder();
            var wrongs = new List<string>();
            var corrects = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                var input = inputs[i];
                var inputString = $"{input.Input[0]} {input.Input[1]} {input.Input[2]}";
                Utility.ReportProgress(i, inputs.Count, inputString, 2);
                str.AppendLine(MeasureMethod(input));
                if (input.ExpectedOutput == input.Output)
                    corrects++;
                else
                {
                    wrongs.Add($"{inputString} was {input.Output} | should be {input.ExpectedOutput}");
                }
            }
            Utility.ClearReport(2);
            return (str.ToString(), wrongs, corrects);
        }

        private static string MeasureMethod(RunnerResult<int, int> input)
        {
            var stopwatch = new Stopwatch();

            var accummulatedTime = TimeSpan.Zero;
            var totalRuns = 10;
            var output = ImplementThis(input.Input[0], input.Input[1], input.Input[2]);
            for (int i = 0; i < totalRuns; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis(input.Input[0], input.Input[1], input.Input[2]);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                    Utility.ReportProgress(i, totalRuns, $"{input.Input[0]} {input.Input[1]} {input.Input[2]} -> {output}");
                }
                finally
                {
                    stopwatch.Reset();
                }
            }
            input.Output = output;

            var ticks = (accummulatedTime / totalRuns).Ticks;

            return $"{ticks}|{input.Input[0]} {input.Input[1]} {input.Input[2]}|{output}";
        }

        public static async Task<List<RunnerResult<int, int>>> ParseInputFile(string inputFilePath)
        {
            var inputLines = await File.ReadAllLinesAsync(inputFilePath);
            var results = inputLines.Select(x =>
            {
                var split = x.Split("|");
                var inputs = split[1].Split(" ");
                return new RunnerResult<int, int>
                {
                    TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
                    Input = inputs.Select(i => int.Parse(i.Trim())).ToArray(), //This type must be the same as the type in ImplementThis,
                    ExpectedOutput = int.Parse(split[2])
                };
            }).ToList();

            return results;
        }
    }
}