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
            for (int testLength = 1; testLength < inputText.Length; testLength++)
            {
                int maxLength = inputText.Length - testLength;
                for (int i = 0; i < maxLength; i++)
                {
                    var subStringToTest = inputText.Substring(i, testLength);
                    var table = new HashTable(subStringToTest);
                    for (int j = i + 1; j <= maxLength; j++)
                    {
                        var subString = inputText.Substring(j, testLength);
                        int found = table.CountChars(subString);
                        if (found == subString.Length) foundAnagrams++;
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