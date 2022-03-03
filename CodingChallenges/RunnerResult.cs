using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenges
{
    internal class RunnerResult
    {
        public dynamic[]? Inputs { get; set; } = null;
        public TimeSpan TargetTime { get; set; }
        public dynamic? Output { get; set; }
        public TimeSpan? ElapsedTime { get; set; }
        public bool Completed { get; set; }

        public string Print()
        {
            return $"[{ElapsedTime?.Ticks ?? 5000} ticks][{(Completed && ElapsedTime != null && ElapsedTime.Value <= TargetTime ? " OK" : "NOK")}]{Output ?? "No output"}";
        }

        public string PrintInput()
        {
            if (Inputs == null || Inputs.Length == 0)
                return "No inputs";
            StringBuilder str = new StringBuilder();
            foreach (var input in Inputs)
            {
                if (input.GetType().IsArray)
                    str.Append(string.Join(", ", input));
                else
                    str.Append($"{input.ToString()} | ");
            }
            return str.ToString();
        }
    }
}
