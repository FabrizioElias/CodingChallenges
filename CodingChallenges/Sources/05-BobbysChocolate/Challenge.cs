using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BobbysChocolate
{
    public static class ChallengeClass
    {
        public static int ImplementThis(int n, int c, int m)
        {
            return CalcularChocolatesComidosR(n, c, m);
        }

        public static int CalcularChocolatesComidos(int dinheiro, int custoPorChocolate, int custoPorPacote)
        {
            int quantidadeDeChocolatesComidos = dinheiro / custoPorChocolate;
            int pacotesDisponiveis = quantidadeDeChocolatesComidos;
            do
            {
                //  5 => 6                     =   5              /        3        = 1
                quantidadeDeChocolatesComidos += pacotesDisponiveis / custoPorPacote;
                //       2         =        5          resto    3             +       1
                pacotesDisponiveis = (pacotesDisponiveis % custoPorPacote) + (pacotesDisponiveis / custoPorPacote);
            } while (pacotesDisponiveis >= custoPorPacote);
            return quantidadeDeChocolatesComidos;
        }

        public static int CalcularChocolatesComidosR(int dinheiro, int custoPorChocolate, int custoPorPacote)
        {
            int quantidadeDeChocolatesComidos = 0;
            int pacotesDisponiveis = 0;
            if (dinheiro <= 0)
            {
                pacotesDisponiveis = -dinheiro;
            }
            if (dinheiro > 0)
            {
                quantidadeDeChocolatesComidos = dinheiro / custoPorChocolate;
                pacotesDisponiveis = quantidadeDeChocolatesComidos;
                dinheiro = 0;
            }
            quantidadeDeChocolatesComidos += pacotesDisponiveis / custoPorPacote;
            pacotesDisponiveis = (pacotesDisponiveis % custoPorPacote) + (pacotesDisponiveis / custoPorPacote);
            dinheiro = -pacotesDisponiveis;
            return pacotesDisponiveis >= custoPorPacote ? quantidadeDeChocolatesComidos + CalcularChocolatesComidosR(dinheiro, custoPorChocolate, custoPorPacote) : quantidadeDeChocolatesComidos;
        }

        public static ThreadResult<int> ChallengeRunner(int[] input)
        {
            var accummulatedTime = TimeSpan.Zero;
            var stopwatch = new Stopwatch();
            var output = ImplementThis(input[0], input[1], input[2]);
            var totalRuns = 20;
            for (int i = 0; i < totalRuns; i++)
            {
                try
                {
                    stopwatch.Start();
                    output = ImplementThis(input[0], input[1], input[2]);
                    stopwatch.Stop();
                    accummulatedTime += stopwatch.Elapsed;
                }
                finally
                {
                    stopwatch.Reset();
                }
            }
            return new ThreadResult<int> { Output = output, ElapsedTime = accummulatedTime / totalRuns };
        }
    }
}