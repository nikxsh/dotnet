using System;
using System.Collections.Generic;

namespace DotNetDemos.CSharpExamples
{
	public class ActionAndFucntion
	{
		private Action<string> printMessage;
		private Func<string, int> printAndReturn;
		public ActionAndFucntion()
		{
		}

		public void Call(string message)
		{
			printMessage = ActionMethod;
			printMessage(message);

			printAndReturn = FuncMethod;
			int returnValue = printAndReturn(message);
			Console.WriteLine("Return Value from Func {0},", returnValue);

			List<Action<string>> actionList = new List<Action<string>>();
			int i = 0;
			for (; i < 10; i++)
				actionList.Add(ActionMethod);

			i = 0;
			foreach (var e in actionList)
			{
				e(String.Format("{0}", i));
				i++;
			}
		}

		public void ActionMethod(string val)
		{
			Console.WriteLine("{0}. Hello, this is Action method", val);
		}


		public int FuncMethod(string val)
		{
			Console.WriteLine("Hello {0}, this is Function method", val);
			return 123;
		}
	}
}
