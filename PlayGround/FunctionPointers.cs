using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp
{
	class FunctionPointers
	{
		public delegate int PerformCalculation(int x, int y);

		public void Play()
		{
			//Delegates();
			//Actions();
			Functions();
		}

		/// <summary>
		/// - A delegate is a type that represents references to methods with a particular parameter list and return type. 
		/// - When you instantiate a delegate, you can associate its instance with any method with a compatible signature and return type. 
		/// - You can invoke (or call) the method through the delegate instance.
		/// </summary>
		private void Delegates()
		{
			PerformCalculation handler = (a, b) => a + b;
			Console.WriteLine($"(Using delegate) 10 + 40 = {handler(10, 40)}");
			CalculateWithCallback(10, 50, handler);

			Console.WriteLine();

			//A delegate can call more than one method when invoked. This is referred to as multicasting.
			PerformCalculation calculationHandler = new PerformCalculation(Add);
			calculationHandler += Sub;
			calculationHandler += Divide;
			//All methods are called in FIFO (First in First Out) order.
			Console.WriteLine("Multicast Invokation");
			calculationHandler(100, 20);
			Console.WriteLine("Removing Divide Method.");
			calculationHandler -= Divide;
			calculationHandler(100, 20);
		}

		private void CalculateWithCallback(int param1, int param2, PerformCalculation callback)
		{
			Console.WriteLine($"(Using delegate callback) 10 + 50 = {callback(param1, param2)}");
		}

		private int Add(int param1, int param2)
		{
			Console.WriteLine($"{param1} + {param2} is {param1 + param2}");
			return param1 + param2;
		}

		private int Sub(int param1, int param2)
		{
			Console.WriteLine($"{param1} - {param2} is {param1 - param2}");
			return param1 - param2;
		}
		private int Multiply(int param1, int param2)
		{
			Console.WriteLine($"{param1} * {param2} is {param1 * param2}");
			return param1 / param2;
		}

		private int Divide(int param1, int param2)
		{
			Console.WriteLine($"{param1} / {param2} is {param1 / param2}");
			return param1 / param2;
		}

		/// <summary>
		/// - Encapsulates a method that has no parameters and does not return a value.
		/// - When you use the Action delegate, you do not have to explicitly define a delegate that encapsulates a 
		///   parameterless procedure
		/// </summary>
		private void Actions()
		{
			Action<string> printToConsole2 = PrintMessage;
			printToConsole2("Using Action<String>");

			//Starting with C# 7.0, C# supports local functions. Local functions are private methods of a type that are nested in another member
			void printToConsole3(string message) { PrintMessage(message); }
			printToConsole3("Using Action delegate");

			void printToConsole4(string message) => PrintMessage(message);
			printToConsole4("Using Action Lambda expression");
		}

		private void PrintMessage(string message)
		{
			Console.WriteLine($"Hello {message}!");
		}

		/// <summary>
		/// - Encapsulates a method that has one parameter and returns a value of the type specified by the TResult parameter.
		/// </summary>
		private void Functions()
		{
			//Func<string, string> selector = str => str.ToUpper();
			//Local function
			string selector(string str) => str.ToUpper();

			string[] words = { "orange", "apple", "Article", "elephant" };

			IEnumerable<string> upperCaseWords = words.Select(selector);
			foreach (var word in upperCaseWords)
				Console.WriteLine(word);

			Func<string, int> contentData = GetContentLength;
			Console.WriteLine($"You are Hired!.Length() > { contentData("You are Hired!")}");

			Func<string, string> convert = delegate (string s)
			{
				return s.ToUpper();
			};

			Console.WriteLine($"Hello Brother > {convert("hello brother!")}");
		}

		private int GetContentLength(string content)
		{
			return content.Length;
		}
	}
}