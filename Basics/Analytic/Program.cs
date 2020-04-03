using System;

namespace Analytic
{
	class Program
	{
		static void Main(string[] args)
		{
			var matching = new Matching();
			matching.PatternMatching1("xyz", "abcd"); //0
			matching.PatternMatching1("abcd", "abcd"); //1
			matching.PatternMatching1("12abcdop", "abcd"); //2
			matching.PatternMatching1("ab123cd", "*abcd"); //0
			matching.PatternMatching1("ab123cd", "abcd*"); //0
			matching.PatternMatching1("ab123cd", "a*bcd"); //0
			matching.PatternMatching1("xab123cdyzxab987cd", "ab*cd"); //2
			matching.PatternMatching1("xyzab123cd", "ab*cd"); //4
			matching.PatternMatching1("xyzab123cxyzab678cd", "ab*cd"); //13

			Console.ReadKey();
		}
	}
}
