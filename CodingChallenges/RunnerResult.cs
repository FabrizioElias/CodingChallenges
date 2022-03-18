using System.Text;

namespace CodingChallenges
{
    public class RunnerResult
    {
        public dynamic? Input { get; set; } = null;
        public TimeSpan TargetTime { get; set; }
        public dynamic? ExpectedOutput { get; set; }
        public dynamic? Output { get; set; }
        public TimeSpan? ElapsedTime { get; set; }
        public bool Completed { get; set; }
        public bool IsOk => Completed && ElapsedTime != null && ElapsedTime.Value <= TargetTime * 1.2 && ExpectedOutput == Output;

        public static string PrintSummary(IEnumerable<RunnerResult> results)
        {
            var count = Convert.ToDouble(results.Count());
            var ok = results.Count(r => r.IsOk);
            if (ok == 0)
                return "No OK results.";
            return $"[{results.Sum(r => (r.ElapsedTime ?? TimeSpan.Zero).Ticks) / ok} average OK ticks]\n" +
                $"[{ok} Oks and {count - ok} Noks]\n" +
                $"[{ok / count:P2} of OK results]";
        }

        public string Print()
        {
            return $"[{ElapsedTime?.Ticks ?? -500} ticks][target {TargetTime.Ticks}][Result: {Output ?? "No output"} == {ExpectedOutput} :Expected][{(IsOk ? " OK" : "NOK")}]";
        }

        public string PrintInput()
        {
            if (Input == null)
                return "No inputs";
            StringBuilder str = new StringBuilder();
            if (Input.GetType().IsArray)
                str.Append(string.Join(", ", Input));
            else
                str.Append($"{Input}");
            return str.ToString();
        }
    }
}