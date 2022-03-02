using System;
using System.Threading;

namespace Challenge
{
	public static class ChallengeClass
	{
		public static object Challenge(object input)
		{
			Console.WriteLine($"Hellow {input}");
			Thread.Sleep(10000);
			return "ABC";
		}
	}
}