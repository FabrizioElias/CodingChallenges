using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenges
{
    internal class RunnerResult
    {
        public string Input { get; set; } = "";
        public TimeSpan TargetTime { get; set; }
        public object? Output { get; set; }
        public TimeSpan? ElapsedTime { get; set; }
        public bool Completed { get; set; }

        public string Print()
        {
            return $"[{ElapsedTime?.Ticks ?? 5000} ticks][{(Completed && ElapsedTime != null && ElapsedTime.Value <= TargetTime ? " OK" : "NOK")}]{Output ?? "No output"}";
        }
    }
}
