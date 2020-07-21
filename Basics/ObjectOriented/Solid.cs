using System;
using System.Linq;

namespace ObjectOriented
{
    public class Solid
    {
		/// <summary>
		///  “S”- SRP(Single responsibility principle)
		/// - So SRP says that a class should have only one responsibility and that resposibility should be encapsulated 
		///   by the class.
		/// - So if we apply SRP we can move that logging activity to some other class who will only look after logging 
		///   activities.
		/// </summary>
		class SRP
		{
			private class Logger
			{
				public static void Log(string message)
				{
					System.IO.File.WriteAllText(@"c:\Error.txt", message);
				}
			}

			internal class DBOperations
			{
				private readonly Logger _logger = new Logger();
				public void SaveData()
				{
					try
					{
						//DB class

					}
					catch (Exception ex)
					{
						Logger.Log(ex.InnerException.ToString());
					}
				}
			}
		}

		/// <summary>
		/// “O” - Open closed principle
		/// States that software application source codes should be open for extension but should be closed for modification
		/// </summary>
		class OCP
		{
			class Customer
			{
				public virtual double GetDiscount(double TotalSales)
				{
					return TotalSales;
				}
			}

			class SilverCustomer : Customer
			{
				public override double GetDiscount(double TotalSales)
				{
					return base.GetDiscount(TotalSales) - 50;
				}
			}
			class GoldCustomer : SilverCustomer
			{
				public override double GetDiscount(double TotalSales)
				{
					return base.GetDiscount(TotalSales) - 100;
				}
			}
		}

		/// <summary>
		/// “L” - Liskov Substitution Principle
		/// - This principle is just an extension of the Open Close Principle.It means that we must make sure that new derived classes are extending
		///    the base classes without changing their behavior.
		/// - The Liskov Substitution Principle says that the object of a derived class should be able to replace an object  of the base class without 
		///    bringing any errors in the system or modifying the behavior of the base class. 
		/// - In short: if S is subset of T, an object of T could be replaced by object of S without impacting the program and
		///    bringing any error in the system.
		/// </summary>

		public void LSPExample()
		{
			Console.WriteLine("-- LSP violation --");
			var values = new[] { 1, 2, 3, 4, 5 };

			Total average = new Total(values);
			Console.WriteLine($" Total is {average.GetTotal()}"); //Prints 15

			EvenTotal evenAverage = new EvenTotal(values);
			Console.WriteLine($" Total is {evenAverage.GetTotal()}"); //Prints 6

			Total evenAverageBase = new EvenTotal(values);
			Console.WriteLine($" Total is {evenAverageBase.GetTotal()}"); //Prints 15, clear LSP violation
		}

		public class Total
		{
			protected readonly int[] numbers;

			public Total(int[] numbers)
			{
				this.numbers = numbers;
			}

			public double GetTotal() => numbers.Sum();
		}

		public class EvenTotal : Total
		{
			public EvenTotal(int[] numbers) : base(numbers)
			{ }

			public new double GetTotal() => numbers.Where(x => (x % 2 == 0)).Sum();
		}

		/// <summary>
		/// “I” - ISP (Interface Segregation principle)
		///  ISP states that no clients should be forced to implement methods which it does not want to use and the contracts 
		///  should be broken down to thin ones.
		/// </summary>
		class ISP
		{
			interface IDiscount
			{
				double GetDiscount(double TotalSales);
			}

			interface IDatabase
			{
				void Add();
			}

			class Customer : IDiscount, IDatabase
			{
				public void Add()
				{
					//ADD
				}

				public double GetDiscount(double TotalSales)
				{
					//Discount
					return TotalSales - 2;
				}
			}

			class Enquiry : IDiscount
			{
				public double GetDiscount(double TotalSales)
				{
					return TotalSales - 5;
				}
			}
		}

		/// <summary>
		/// “D” - DIP (Dependency Inversion Principle)
		///  DIP states that the higher level modules should be coupled with the lower level modules with complete 
		///  abstraction  
		///  The general idea of this principle is as simple as it is important: High-level modules, which provide complex logic, should be easily 
		///  reusable and unaffected by changes in low-level modules, which provide utility features.
		///  To achieve that, you need to introduce an abstraction that decouples the high-level and low-level modules from each other.
		/// </summary>
		class DIP
		{
			interface ILogger
			{
				void Log(string error);
			}

			class FileLogger : ILogger
			{
				public void Log(string error)
				{
					System.IO.File.WriteAllText(@"c:\Error.txt", error);
				}
			}

			class DatabaseOperations
			{
				private ILogger _logger;
				public DatabaseOperations(ILogger logger)
				{
					_logger = logger;
				}

				public void SaveData()
				{
					try
					{
					}
					catch (Exception ex)
					{
						_logger.Log(ex.InnerException.ToString());
					}
				}
			}

			public void Action()
			{
				//If you watch closely the biggest problem is the “NEW” keyword.He is taking extra responsibilities of which object needs to be created.
				//So if we INVERT / DELEGATE this responsibility to someone else rather the customer class doing it that would really solve the problem to a certain extent.

				var op = new DatabaseOperations(new FileLogger());
				op.SaveData();
			}
		}
	}

}
