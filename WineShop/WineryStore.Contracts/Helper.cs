using System;
using System.Collections.Generic;
using System.Text;

namespace WineryStore.Contracts.Helper
{
	public class Helper
	{
		public static Dictionary<int, string> ToEnumArray<T>()
		{
			Dictionary<int, string> enumDictonary = new Dictionary<int, string>();
			var enumValues = Enum.GetValues(typeof(T));

			for (int i = 0; i < enumValues.Length; i++)
				enumDictonary.Add((int)enumValues.GetValue(i), enumValues.GetValue(i).ToString());
			return enumDictonary;
		}
	}
}
