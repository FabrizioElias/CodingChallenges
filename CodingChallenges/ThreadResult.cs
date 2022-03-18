using System.Text;

namespace CodingChallenges
{
    public class ThreadResult
    {
        public dynamic? Output { get; set; }
        public bool Completed { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }
}