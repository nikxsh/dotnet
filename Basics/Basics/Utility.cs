using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Basics.Common
{
	public static class Utility
	{
		public static Stopwatch Watch = new Stopwatch();
		public static Random random = new Random();
		public static double EllapsedTime(double milliseconds) => TimeSpan.FromMilliseconds(milliseconds).TotalSeconds;
		public static IEnumerable<int> Numbers(int max) => Enumerable.Range(1, max);

		public static IEnumerable<Employee> GetEmployeeMockArray(int length = 10)
		{
			var employees = new List<Employee>();
			foreach (var item in Enumerable.Range(1, length))
			{
				var employee = new Employee
				{
					EmployeeId = item,
					Rank = GetUniqueRandomValue(employees.Select(x => x.Rank), 1, 50),
					Name = GetUniqueRandomValue(employees.Select(x => x.Name), 0, MockData.Names.Length),
					Salary = random.Next(30000, 90000)
				};
				employees.Add(employee);
			}
			return employees;
		}

		public static void PrintployeeMockArray(IEnumerable<Employee> employees)
		{
			Console.WriteLine($"------------------------");
			foreach (var item in employees)
				Console.WriteLine($"{item.Rank,3}:{item.Name,-20}:{item.Salary,3:C}");
			Console.WriteLine();
		}

		public static string GetUniqueRandomValue(IEnumerable<string> existingValues, int start, int max)
		{
			var randomName = GetRandomName(start, max);
			while (existingValues.Any(x => x.Equals(randomName, StringComparison.InvariantCultureIgnoreCase)))
				randomName = GetRandomName(start, max);
			return randomName;
		}

		public static int GetUniqueRandomValue(IEnumerable<int> existingValues, int start, int max)
		{
			var randomNumber = random.Next(start, max);
			while (existingValues.Any(x => x == randomNumber))
				randomNumber = random.Next(start, max);
			return randomNumber;
		}

		private static readonly Func<int, int, string> GetRandomName = (start, max) => MockData.Names[random.Next(start, max)];

		public static T Cast<T>(object referenceObject)
		{
			Type objectType = referenceObject.GetType();
			Type target = typeof(T);
			var instance = Activator.CreateInstance(target, false);
			var memberInfos = from source in target.GetMembers().ToList()
									where source.MemberType == MemberTypes.Property
									select source;
			List<MemberInfo> members = memberInfos.Where(memberInfo => memberInfos.Select(c => c.Name)
				.ToList().Contains(memberInfo.Name)).ToList();
			PropertyInfo propertyInfo;
			object value;
			foreach (var memberInfo in members)
			{
				propertyInfo = typeof(T).GetProperty(memberInfo.Name);
				value = referenceObject.GetType().GetProperty(memberInfo.Name).GetValue(referenceObject, null);
				propertyInfo.SetValue(instance, value, null);
			}
			return (T)instance;
		}
	}
}