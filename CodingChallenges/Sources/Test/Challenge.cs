using System;
using System.Collections.Generic;
using System.Threading;

namespace Challenge
{
	public static class ChallengeClass
	{
		public static string ImplementThis(string[] all)
		{
			return string.Join(", ", all);
		}

		public static dynamic ChallengeRunner(dynamic input)
		{
			var output = ImplementThis(input);
			Console.WriteLine($"Hellow {output}");
			return output;
		}
	}
}