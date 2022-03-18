using CodingChallenges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SherlockAnagramSolution
{
    public static class ChallengeClass
    {
        public static int ImplementThis(string inputText)
        {
            //max test length is always length-1 since the full string is not an anagram
            int foundAnagrams = 0;
            for (int testLength = 1; testLength < inputText.Length; testLength++) //Teste a partir da posição 1 (final)
            {
                int maxLength = inputText.Length - testLength; //a substring maxima não pode superar o tamanho dela mesma. Se estou testando 10 chars de uma string com 11, basta 1 teste
                for (int i = 0; i < maxLength; i++)
                {
                    var subStringToTest = inputText.Substring(i, testLength); //montando a substring a testar com a string inteira
                    var table = new HashTable(subStringToTest); //Contando quantas letras tem de cada na substring
                    for (int j = i + 1; j <= maxLength; j++) //Testa a string original do tamanho máx possível (se tamanho substrs==3 e str==5, não precisa testar 4)
                    {
                        var subString = inputText.Substring(j, testLength);
                        int found = table.CountChars(subString); //Conta carateres na substring tirada da string total (aquela que vou comparara com a substring de teste)
                        if (found == subString.Length) foundAnagrams++; //se forem igual, achei, não importa a ordem dos caracteres
                    }
                }
            }
            return foundAnagrams;
        }

        private sealed class HashTable
        {
            private readonly int[] _table;

            public HashTable(string word)
            {
                _table = new int[26];
                foreach (char c in word)
                {
                    _table[Hash(c)]++;
                }
            }

            public int FindOccurrences(char c)
            {
                return _table[Hash(c)];
            }

            public int CountChars(string characters)
            {
                var innerTable = new HashTable(characters);
                int found = 0;
                foreach (char c in characters)
                {
                    if (FindOccurrences(c) == innerTable.FindOccurrences(c))
                    {
                        found++;
                    }
                }
                return found;
            }

            private int Hash(char s)
            {
                return s % _table.Length;
            }
        }

        public static string ProduceInputFile()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(MeasureMethod("aabbc")); //2
            str.AppendLine(MeasureMethod("bbccde")); //2
            str.AppendLine(MeasureMethod("dddddd")); //35
            str.AppendLine(MeasureMethod("acacac")); //21
            str.AppendLine(MeasureMethod("dededede")); //21
            str.AppendLine(MeasureMethod("aabbccdd")); //21
            str.AppendLine(MeasureMethod("acacacacac")); //21
            str.AppendLine(MeasureMethod("bbeeffggij")); //21
            str.AppendLine(MeasureMethod("bbbbbbbbbbbbbbbbbbbbbb")); //21
            str.AppendLine(MeasureMethod("abba")); //4
            str.AppendLine(MeasureMethod("abcd")); //0
            str.AppendLine(MeasureMethod("ifailuhkqq")); //3
            str.AppendLine(MeasureMethod("kkkk")); //10
            str.AppendLine(MeasureMethod("cdcd")); //5
            str.AppendLine(MeasureMethod("dbcfibibcheigfccacfegicigcefieeeeegcghggdheichgafhdigffgifidfbeaccadabecbdcgieaffbigffcecahafcafhcdg")); //1464
            str.AppendLine(MeasureMethod("dfcaabeaeeabfffcdbbfaffadcacdeeabcadabfdefcfcbbacadaeafcfceeedacbafdebbffcecdbfebdbfdbdecbfbadddbcec")); //2452
            str.AppendLine(MeasureMethod("gjjkaaakklheghidclhaaeggllagkmblhdlmihmgkjhkkfcjaekckaafgabfclmgahmdebjekaedhaiikdjmfbmfbdlcafamjbfe")); //873
            str.AppendLine(MeasureMethod("fdbdidhaiqbggqkhdmqhmemgljaphocpaacdohnokfqmlpmiioedpnjhphmjjnjlpihmpodgkmookedkehfaceklbljcjglncfal")); //585
            str.AppendLine(MeasureMethod("bcgdehhbcefeeadchgaheddegbiihehcbbdffiiiifgibhfbchffcaiabbbcceabehhiffggghbafabbaaebgediafabafdicdhg")); //1305
            str.AppendLine(MeasureMethod("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")); //166650
            str.AppendLine(MeasureMethod("mhmgmbbccbbaffhbncgndbffkjbhmkfncmihhdhcebmchnfacdigflhhbekhfejblegakjjiejeenibemfmkfjbkkmlichlkbnhc")); //840
            str.AppendLine(MeasureMethod("fdacbaeacbdbaaacafdfbbdcefadgfcagdfcgbgeafbfbggdedfbdefdbgbefcgdababafgffedbefdecbaabdaafggceffbacgb")); //2134
            str.AppendLine(MeasureMethod("bahdcafcdadbdgagdddcidaaicggcfdbfeeeghiibbdhabdhffddhffcdccfdddhgiceciffhgdibfdacbidgagdadhdceibbbcc")); //1571
            str.AppendLine(MeasureMethod("dichcagakdajjhhdhegiifiiggjebejejciaabbifkcbdeigajhgfcfdgekfajbcdifikafkgjjjfefkdbeicgiccgkjheeiefje")); //1042
            str.AppendLine(MeasureMethod("ifailuhkqqhucpoltgtyovarjsnrbfpvmupwjjjfiwwhrlkpekxxnebfrwibylcvkfealgonjkzwlyfhhkefuvgndgdnbelgruel")); //399
            str.AppendLine(MeasureMethod("gffryqktmwocejbxfidpjfgrrkpowoxwggxaknmltjcpazgtnakcfcogzatyskqjyorcftwxjrtgayvllutrjxpbzggjxbmxpnde")); //471
            str.AppendLine(MeasureMethod("mqmtjwxaaaxklheghvqcyhaaegtlyntxmoluqlzvuzgkwhkkfpwarkckansgabfclzgnumdrojexnrdunivxqjzfbzsodycnsnmw")); //370
            str.AppendLine(MeasureMethod("ofeqjnqnxwidhbuxxhfwargwkikjqwyghpsygjxyrarcoacwnhxyqlrviikfuiuotifznqmzpjrxycnqktkryutpqvbgbgthfges")); //403
            str.AppendLine(MeasureMethod("zjekimenscyiamnwlpxytkndjsygifmqlqibxxqlauxamfviftquntvkwppxrzuncyenacfivtigvfsadtlytzymuwvpntngkyhw")); //428
            str.AppendLine(MeasureMethod("ioqfhajbwdfnudqfsjhikzdjcbdymoecaokeomuimlzcaqkfmvquarmvlnrurdblzholugvwtkunirmnmsatrtbqlioauaxbcehl")); //412
            str.AppendLine(MeasureMethod("kaggklnwxoigxncgxnkrtdjnoeblhlxsblnqitdkoiftxnsafukbdhasdeihlhfrqkfzqhvnsmsgnrayhsyjsniutmgpfjmssfsg")); //472
            str.AppendLine(MeasureMethod("fhithnigqftuzzgpdiquhlsozksxxfreddmsmvqgfgzntphmgggszwtkcbmjsllwtukgqvpvxvmatuanbeossqwtpgzbagaukmta")); //457
            str.AppendLine(MeasureMethod("rqjfiszbigkdqxkfwtsbvksmfrffoanseuenvmxzsugidncvtifqesgreupsamtsyfrsvwlvhtyzgjgnmsowjwhovsmfvwuniasw")); //467
            str.AppendLine(MeasureMethod("dxskilnpkkdxwpeefvgkyohnwxtrrtxtckkdgnawrdjtcpzplywyxmwtemwmtklnclqccklotbpsrkazqolefprenwaozixvlxhu")); //447

            return str.ToString();
        }

        private static string MeasureMethod(string input)
        {
            var stopwatch = new Stopwatch();

            var runs = 50;
            var output = 0;
            var accummulatedTime = TimeSpan.Zero;
            output = ImplementThis(input);
            for (int i = 0; i < runs; i++)
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
                    Utility.ReportProgress(i, runs, input);
                    stopwatch.Reset();
                }
            }

            var ticks = (accummulatedTime / runs).Ticks;

            return $"{ticks}|{input}|{output}";
        }

        public static async Task<List<RunnerResult>> ParseInputFile(string inputFilePath)
        {
            var inputLines = await File.ReadAllLinesAsync(inputFilePath);
            var results = inputLines.Select(x =>
            {
                var split = x.Split("|");
                return new RunnerResult
                {
                    TargetTime = TimeSpan.FromMilliseconds(double.Parse(split[0])),
                    Input = split[1], //This type must be the same as the type in ImplementThis,
                    ExpectedOutput = int.Parse(split[2])
                };
            }).ToList();

            return results;
        }
    }
}