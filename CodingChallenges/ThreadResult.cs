using System.Text;

namespace CodingChallenges
{
    public class ThreadResult
    {
        public dynamic? Output { get; set; }
        public bool Completed { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }

    public class ThreadResult<TOutput>
    {
        public TOutput? Output { get; set; }
        public bool Completed { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public void Clear()
        {
            Output = default;
            Completed = false;
            ElapsedTime = TimeSpan.MaxValue;
        }
    }
}