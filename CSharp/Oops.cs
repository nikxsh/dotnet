using System;
using System.Linq;

namespace CSharp
{
	class Oops
	{
		public void Play()
		{
			//MethodHidingExample();
			//Polymorphism();
			//AbstractClassExample();
			Struct();
			//ClassExample();
			//ConstructorsExample();
			//InterfaceExample();
			//LSPExample();
		}

		private void MethodHidingExample()
		{
			Console.WriteLine("-- Method Hiding --");
			Baap obj1 = new Baap();
			obj1.Display();
			Beta obj2 = new Beta();
			obj2.Display();
			Baap obj3 = new Beta();
			obj3.Display();
		}

		private void Polymorphism()
		{
			Console.WriteLine("-- Static Polymorphism (overload) --");
			var calculator = new SimpleCalculator();
			var sqrtOf = calculator.Calculate(155, (x) => Math.Sqrt(x));
			Console.WriteLine($"SQRT of 155 is {sqrtOf}");
			var divideOf = calculator.Calculate(45, 9, (x, y) => x / y);
			Console.WriteLine($"Divide 45 by 9 is {divideOf}");


			Console.WriteLine("-- Dynamic Polymorphism (virtual/abstract) --");

			SalaryCalculator internSalaryCalculator = new InternSalaryCalculator();
			internSalaryCalculator.CalculateTotalPay();
			internSalaryCalculator.Print();

			SalaryCalculator developerSalaryCalculator = new DeveloperSalaryCalculator();
			developerSalaryCalculator.CalculateTotalPay();
			developerSalaryCalculator.Print();

			SalaryCalculator managerSalaryCalculator = new ManagerSalaryCalculator();
			managerSalaryCalculator.CalculateTotalPay();
			managerSalaryCalculator.Print();
		}

		private void LSPExample()
		{
			Console.WriteLine("-- LSP violation --");
			var values = new int[] { 1, 2, 3, 4, 5 };

			Total average = new Total(values);
			Console.WriteLine($" Total is {average.GetTotal()}"); //Prints 15

			EvenTotal evenAverage = new EvenTotal(values);
			Console.WriteLine($" Total is {evenAverage.GetTotal()}"); //Prints 6

			Total evenAverageBase = new EvenTotal(values);
			Console.WriteLine($" Total is {evenAverageBase.GetTotal()}"); //Prints 15, clear LSP violation
		}

		private void AbstractClassExample()
		{
			Console.WriteLine("-- Abstract Class --");
			AbstractAccountBase defualtSavingAccount = new SavingAccount("SAVING0001", 10000);
			defualtSavingAccount.DisplayAmount();
			defualtSavingAccount.LoanApproval();

			AbstractAccountBase savingAccount = new SavingAccount("SAVING0002", 56000, 5);
			savingAccount.DisplayAmount();
			savingAccount.LoanApproval();

			SalaryAccount.TriggerForStaticConstructor = "Get out!";
			AbstractAccountBase salaryAccount = new SalaryAccount("SALARY0001", 25000, 3);
			salaryAccount.DisplayAmount();
			salaryAccount.LoanApproval();
		}

		private void ConstructorsExample()
		{
			Console.WriteLine("-- Constructors --");
			///ProtectedBase protectedBase = new ProtectedBase(); //Will give error becuase not parameterless public constructor
			ProtectedBase protectedChild = new ProtectedChild
			{
				value = "value" //ProtectedBase property
			};//Will work becuase of having internal constructor

			///InternelBase internelBase = new InternelBase();//Will give error becuase not parameterless public constructor
			InternelBase internelChild = new InternelChild
			{
				value = "Test"
			};
		}

		private void Struct()
		{
			var coords1 = new Coords();
			var coords2 = new Coords(10, 10);
			Coords coords3;

			Console.WriteLine("--- Struct with default constructor  --- ");
			coords1.Display(); 

			Console.WriteLine("---  Struct with named constructor  --- ");
			coords2.Display();

			Console.WriteLine("--- Struct without creating instance  --- ");
			coords3.x = 30;
			coords3.y = 50;
			coords3.Display(); //If method;s used variable are unassigned the calling method will give compile time error
		}

		private void ClassExample()
		{
			Console.WriteLine("-- Classs --");
			Country earth = new Country("Earth");
			earth.Display(); //Welcome  to Earth
			earth.GetData(23); //Country: 23
			earth.GetData(23.56); //Country: 23.56
										 ///earth.GetData(23.5m); //Compile time error- can not convert decimal to int

			Country india = new India();
			india.Display(); //Country: Welcome to India
			india.GetData(23); //Country: 23
			india.GetData(23.56); //Country: 23.56
										 ///india.GetData(23.5m); //Compile time error- can not convert decimal to int

			Country contryMaha = new Maharashtra();
			contryMaha.Display(); //Country: Welcome to Maharashtra
			contryMaha.GetData(23); //Country: 23
			contryMaha.GetData(23.56); //Country: 23.56
												///contryMaha.GetData(23.5m); //Compile time error- can not convert decimal to int

			India indiaIndia = new India();
			indiaIndia.Display(); //With out Method hiding > India: Welcome to India
			indiaIndia.GetData(23); //India: 23
			indiaIndia.GetData(23.56); //Country: 23.56
												//indiaIndia.GetData(23.5m); //Compile time error- can not convert decimal to int

			India indiaMaha = new Maharashtra();
			indiaMaha.Display(); //India: Welcome to Maharashtra
			indiaMaha.GetData(23); //India: 23
			indiaMaha.GetData(23.56); //Country: 23.56
											  ///indiaMaha.GetData(23.5m); //Compile time error- can not convert decimal to int

			Maharashtra mahaMaha = new Maharashtra();
			mahaMaha.Display(); //Method hiding > Maharashtra: Welcome to Maharashtrasss
			mahaMaha.GetData(23); //Maharashtra: 23
			mahaMaha.GetData(23.56); //India: 23.56
			mahaMaha.GetData(23.5m); //Maharashtra: 23.5

			Console.WriteLine("-------------------------------------");

			Country usa = new USA();
			usa.Display();
			///usa.GunShot(); method not found
			USA usaUsa = new USA();
			usaUsa.Display();
			usaUsa.GunShot();
			//USA usCountry = new Country("This is America"); Can not implicitly convert 
		}

		private void InterfaceExample()
		{
			Console.WriteLine("-- Interface --");
			Calculator calculator = new Calculator();
			ISimpleCalculator simple = new Calculator();
			simple.Add(); //will call ISimpleCalculator.Add() 
			simple.Divide();
			IComplexCalculator complex = new Calculator();
			complex.Add(); //will call IComplexCalculator.Add() 
			complex.Base10();

			//You cannot access interface methods from class reference.
			///calculator.Add()  //will give compilation error.
			calculator.Base10();
			calculator.Divide();
		}
	}

	#region Abstraction and Encapsulation
	/// ------------------------------  Abstraction ------------------------------------------------------------------
	///- Means that you only incorporate those features of an entity which are required in your design. 
	///- So, if every bank account has an opening date but your application doesn't need to know an account's opening 
	///  date, then you simply don't add the OpeningDate field in your Object-Oriented Design of the BankAccount class.
	///- Abstraction in OOAD has nothing to do with abstract classes in programming. By this principle, your entities 
	///  are an abstraction of what they actually are. You design an abstraction of Bank Account down to only that level 
	///  of detail that your application's needs
	///- Abstraction defines way to abstract or hide your data and members from outside world.Classes use the concept 
	///  of abstraction and are defined as a list of abstract attributes.


	///------------------------------  Encapsulation ------------------------------------------------------------------
	/// - Means bundling the related functionality together and giving access to only the needful. 
	/// - This principle is the basis of designing classes in Object Oriented Design where: you put related data and 
	///   methods together; and,not all the pieces of data and methods may be public.
	/// - Encapsulation is defined 'as the process of enclosing one or more items within a physical or logical package'
	/// - Encapsulation, in object oriented programming methodology, prevents access to implementation details  
	/// - Abstraction and encapsulation are related features in object oriented programming. Abstraction allows 
	///   making relevant information visible and encapsulation enables a programmer to implement the desired level 
	///   of abstraction.
	/// - Encapsulation is implemented by using access specifiers. An access specifier defines the scope and visibility 
	///   of a class member. C# supports the following access specifiers:
	///
	///   Public, Private, Protected, Internal, Protected internal

	///Exposed Methods to End user
	interface Icar
	{
		string CarName { get; set; }
		void Steering();
		void Break();
	}

	class Car : Icar
	{
		public string CarName { get; set; }

		public void Break()
		{
			Console.WriteLine($"{CarName} Break");
		}

		public void Steering()
		{
			Console.WriteLine($"{CarName} Steering");
		}

		public void Engine()
		{
			Console.WriteLine($"{CarName} Engine");
		}

		public void Oil()
		{
			Console.WriteLine($"{CarName} Oil");
		}
	}

	class EndUser
	{
		public void Play()
		{
			//Only Methods and properties declared in Interface will be accessible to end user
			Icar abstraction = new Car();
			abstraction.CarName = "Hello";
			abstraction.Steering();
			abstraction.Break();
			//abstraction.Oil();Not Accessible
		}
	}

	#endregion

	#region Inheritance

	/// - Inheritance enables you to create new classes that reuse, extend, and modify the behavior that
	///  is defined in other classes.
	/// - Conceptually, a derived class is a specialization of the base class.  
	/// - A Mammal is an Animal, and a Reptile is an Animal, but each derived class represents different 
	///   specializations of the base class.
	class Animal
	{
		public void Name()
		{
			Console.WriteLine("Common Name");
		}
	}

	class Mammal : Animal
	{
		public void Mammal_has()
		{
			Console.WriteLine("Additional Method for Mammal");
		}
	}

	class Reptile : Animal
	{
		public void Reptile_has()
		{
			Console.WriteLine("Additional Method for Reptile");
		}
	}

	#endregion

	#region Polymorphism 
	/// - Polymorphism is a consequence of inheritance. Inheriting a method from parent is useful, but being able to modify a method if 
	///   the situation demands, is polymorphism. 
	/// - You may implement a method in the subclass with exactly the same signature as in parent class so that when called, the method 
	///   from child class is executed. This is polymorphism.
	/// - The word polymorphism means having many forms. In object-oriented programming paradigm, polymorphism is often expressed as 
	///   'one interface, multiple functions'. 
	/// - Static Polymorphism: 
	///    The mechanism of linking a function with an object during compile time is called early binding. It is also called static binding. 
	///    C# provides two techniques to implement static polymorphism. 
	///    They are: 
	///     1. Function overloading
	///     2. Operator overloading
	/// - Dynamic  Polymorphism:  
	///    C# allows you to create abstract classes that are used to provide partial class implementation of an interface.
	///    Implementation is completed when a derived class inherits from it. Abstract classes contain abstract methods,which are implemented by 
	///    the derived class. The derived classes have more specialized functionality.
	///    Here are the rules about abstract classes:
	///     1. You cannot create an instance of an abstract class
	///     2. You cannot declare an abstract method outside an abstract class
	///     3. When a class is declared sealed, it cannot be inherited, abstract classes cannot be declared sealed.

	public delegate double SingleCalculation(double x);
	public delegate double DoubleCalculation(double x, double y);

	class SimpleCalculator
	{
		public double Calculate(int x, SingleCalculation singleCalculation)
		{
			return singleCalculation(x);
		}

		public double Calculate(int x, int y, DoubleCalculation doubleCalculation)
		{
			return doubleCalculation(x, y);
		}

		//Wont work as overloading does not diffentiate based on return type
		//public double Calculate(int x, int y)
		//{
		//    return x + y;
		//}

		//Will work as it having different params
		public double Calculate(int x, double y, DoubleCalculation doubleCalculation)
		{
			return doubleCalculation(x, y);
		}
	}

	abstract class SalaryCalculator
	{
		//Fields cannot be virtual; only methods, properties, events and indexers can be virtual.
		protected const double BasePay = 5000.5;
		protected double TotalPay { get; set; }

		public virtual void CalculateTotalPay()
		{
			TotalPay = BasePay + 2000;
		}

		public abstract void Print();
	}

	class InternSalaryCalculator : SalaryCalculator
	{
		//A derived class can override a base class member only if the base class member is declared as virtual or abstract. 
		public override void Print()
		{
			Console.WriteLine($"Intern salary is {TotalPay}");
		}
	}

	class DeveloperSalaryCalculator : SalaryCalculator
	{

		public override void CalculateTotalPay()
		{
			TotalPay = BasePay + 20000;
		}

		public override void Print()
		{
			Console.WriteLine($"Developer salary is {TotalPay}");
		}
	}

	class SeniorDeveloperSalaryCalculator : DeveloperSalaryCalculator
	{
		//A derived class can stop virtual inheritance by declaring an override as sealed. 
		public sealed override void CalculateTotalPay()
		{
			base.CalculateTotalPay();
		}

		public override void Print()
		{
			Console.WriteLine($"Developer salary is {TotalPay}");
		}
	}

	class JuniorDeveloperSalaryCalculator : SeniorDeveloperSalaryCalculator
	{
		//In this case, if CalculateTotalPay is called on using a variable of type JuniorDeveloperSalaryCalculator, 
		//the new DoWork is called.
		public new void CalculateTotalPay()
		{
			TotalPay = BasePay + 10000;
		}

		public override void Print()
		{
			Console.WriteLine($"Developer salary is {TotalPay}");
		}
	}

	class ManagerSalaryCalculator : SalaryCalculator
	{

		public override void CalculateTotalPay()
		{
			TotalPay = BasePay + 50000;
		}

		public override void Print()
		{
			Console.WriteLine($"Manager salary is {TotalPay}");
		}
	}
	#endregion

	#region MethodHiding
	class Baap
	{
		public void BaseMethod()
		{
			Console.WriteLine("Baap's Method");
		}

		public void Display()
		{
			Console.WriteLine("Display Baap");
		}
	}

	class Beta : Baap
	{
		public new void Display()
		{
			Console.WriteLine("Display Beta");
		}
	}
	#endregion

	#region SOLID

	/// <summary>
	///  “S”- SRP(Single responsibility principle)
	/// - So SRP says that a class should have only one responsibility and that resposibility should be encapsulated 
	///   by the class.
	/// - So if we apply SRP we can move that logging activity to some other class who will only look after logging 
	///   activities.
	/// </summary>
	class SRP
	{
		internal class Logger
		{
			public void Log(string message)
			{
				System.IO.File.WriteAllText(@"c:\Error.txt", message);
			}
		}

		internal class DBOperations
		{
			private Logger _logger = new Logger();
			public void SaveData()
			{
				try
				{
					//DB class

				}
				catch (Exception ex)
				{
					_logger.Log(ex.InnerException.ToString());
				}
			}
		}
	}

	/// <summary>
	/// “O” - Open closed principle
	//    States that software application source codes should be open for extension but should be closed for modification
	/// </summary>
	class OCP
	{
		class Customer
		{
			public virtual double getDiscount(double TotalSales)
			{
				return TotalSales;
			}
		}

		class SilverCustomer : Customer
		{
			public override double getDiscount(double TotalSales)
			{
				return base.getDiscount(TotalSales) - 50;
			}
		}
		class goldCustomer : SilverCustomer
		{
			public override double getDiscount(double TotalSales)
			{
				return base.getDiscount(TotalSales) - 100;
			}
		}
	}

	/// <summary>
	/// “L” - Liskov Substitution Principle
	///  - This principle is just an extension of the Open Close Principle.It means that we must make sure that new
	///    derived classes are extending the base classes without changing their behavior.
	///  - The Liskov Substitution Principle says that the object of a derived class should be able to replace an object
	///    of the base class without bringing any errors in the system or modifying the behavior of the base class. In
	///    short: if S is subset of T, an object of T could be replaced by object of S without impacting the program and
	///     bringing any error in the system.
	/// </summary>
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
			double getDiscount(double TotalSales);
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

			public double getDiscount(double TotalSales)
			{
				//Discount
				return TotalSales - 2;
			}
		}

		class Enquiry : IDiscount
		{
			public double getDiscount(double TotalSales)
			{
				return TotalSales - 5;
			}
		}
	}

	/// <summary>
	/// “D” - DIP (Dependency Inversion Principle)
	///  DIP states that the higher level modules should be coupled with the lower level modules with complete 
	///  abstraction
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
	#endregion

	#region Multiple Inheritance 

	interface ICar
	{
		bool CanRun { get; set; }
	}

	interface IPlane
	{
		bool CanFly { get; set; }
	}

	class SuperCar : ICar, IPlane
	{
		public bool CanFly
		{
			get { return true; }

			set { }
		}

		public bool CanRun
		{
			get { return true; }

			set { }
		}
	}
	#endregion

	#region Abstract Class
	public abstract class AbstractAccountBase
	{
		private static readonly DateTime OpeningDate;

		protected const int defaultDuration = 1;

		protected readonly string accountNumber;

		protected readonly decimal amount;

		protected readonly decimal possibleReturns;

		protected virtual decimal interestRate { get; set; } = 0.045m;

		static AbstractAccountBase()
		{
			OpeningDate = DateTime.Now;
			Console.WriteLine($"AbstractAccountBase Static Constructor: Account Opened At {OpeningDate}");
		}

		public AbstractAccountBase(string accountNumber, decimal amount, int duration) : this(duration)
		{
			this.accountNumber = accountNumber;
			this.amount = amount;
			possibleReturns = (amount * interestRate) * duration;
			Console.WriteLine($"AbstractAccountBase Public Constructor: Amount {amount} deposited to {accountNumber}");
		}

		private AbstractAccountBase(int _private)
		{
			Console.WriteLine($"AbstractAccountBase Private Constructor: Money will be invested for {_private} years");
		}

		internal AbstractAccountBase(string _internal)
		{
			Console.WriteLine($"AbstractAccountBase Internal Constructor");
		}

		protected AbstractAccountBase(double _protected)
		{
			Console.WriteLine($"AbstractAccountBase  Protected");
		}

		public virtual void DisplayAmount()
		{
			Console.WriteLine($"AbstractAccountBase: Amount {amount} | Account {accountNumber} | Opened at {OpeningDate}");
		}

		public abstract void LoanApproval();
	}

	public class SavingAccount : AbstractAccountBase
	{
		static SavingAccount()
		{
			Console.WriteLine($"SavingAccount Static Constructor");
		}

		public SavingAccount(string accountNumber, decimal amount) : this(accountNumber, amount, defaultDuration)
		{
			Console.WriteLine($"SavingAccount public overload Constructor");
		}

		public SavingAccount(string accountNumber, decimal amount, int duration) : base(accountNumber, amount, duration)
		{
			Console.WriteLine($"SavingAccount Public Constructor : Possible Return {possibleReturns}");
		}

		//Can call public, internal & protected constructor of base
		private SavingAccount(int _private) : base(12.4) //:base("")
		{
			Console.WriteLine($"SavingAccount Private Constructor");
		}

		//Can call public, internal & protected constructor of base
		internal SavingAccount(string _internal) : base(12.4) //:base("")
		{
			Console.WriteLine($"SavingAccount Internal Constructor");
		}

		//Can call public, internal & protected constructor of base
		protected SavingAccount(double _protected) : base(12.4) //:base("")
		{
			Console.WriteLine($"SavingAccount  Protected");
		}

		public sealed override void LoanApproval()
		{
			if (amount > 10000)
				Console.WriteLine($"Loan Approved");
			else
				Console.WriteLine($"Loan Rejcted");
		}
	}

	public class SalaryAccount : AbstractAccountBase
	{
		public static string TriggerForStaticConstructor;
		static SalaryAccount()
		{
			Console.WriteLine($"SalaryAccount Static Constructor: Message won't be displayed here {TriggerForStaticConstructor}");
		}

		public SalaryAccount(string accountNumber, decimal amount, int duration) : base(accountNumber, amount, duration)
		{
			Console.WriteLine($"SalaryAccount Public Constructor: Possible Return {possibleReturns}");
		}

		public sealed override void DisplayAmount()
		{
			Console.WriteLine($"SalaryAccount: Amount {amount} | Account {accountNumber}");
		}

		public sealed override void LoanApproval()
		{
			if (amount > 50000)
				Console.WriteLine($"Loan Approved");
			else
				Console.WriteLine($"Loan Rejcted");
		}
	}


	#endregion

	#region Struct 
	/// <summary>
	/// - A struct type is a value type that is typically used to encapsulate small groups of related variables, such as the 
	///   coordinates of a rectangle or the characteristics of an item in an inventory.
	/// - Structs can also contain constructors, constants, fields, methods, properties, indexers, operators, events, and nested 
	///   types, although if several such members are required, you should consider making your type a class instead.
	/// - When you create a struct object using the new operator, it gets created and the appropriate constructor is called according 
	///   to the constructor's signature. Unlike classes, structs can be instantiated without using the new operator.
	/// - There is no inheritance for structs as there is for classes. A struct cannot inherit from another struct or class, and it 
	///   cannot be the base of a class. 
	/// - However, inherit from the base class Object. A struct can implement interfaces, and it does that exactly as classes do.
	/// - You can cast a struct type to any interface type that it implements; this causes a boxing operation to wrap the struct inside a 
	///   reference type object on the managed heap. Boxing operations occur when you pass a value type to a method that takes a System.Object 
	///   or any interface type as an input parameter.
	/// - You use the struct keyword to create your own custom value types
	/// 
	/// When to use Struct
	/// ✓ CONSIDER defining a struct instead of a class if instances of the type are small and commonly short-lived or are commonly embedded 
	///    in other objects.
	/// X AVOID defining a struct unless the type has all of the following characteristics:
	///    It logically represents a single value, similar to primitive types(int, double, etc.).
	///    It has an instance size under 16 bytes.
	///    It is immutable.
	///    It will not have to be boxed frequently.
	/// </summary>
	interface IScreen
	{
		void Display();
	}
	struct Coords : IScreen
	{
		public int x, y;

		public Coords(int p1, int p2)
		{
			x = p1;
			y = p2;
		}

		public void Display()
		{
			Console.WriteLine($"x = {x}, y = {y}");
			Console.WriteLine();
		}
	}
	#endregion

	#region Class Examples
	//Base class must have atleast one parameter less construtor to inherit (except private)
	public class ProtectedBase
	{
		public string value;
		private ProtectedBase(int _protected)
		{ }
		protected ProtectedBase()
		{ }
	}

	public class ProtectedChild : ProtectedBase
	{ }

	public class InternelBase
	{
		public string value;
		private InternelBase(int _protected)
		{ }
		protected InternelBase()
		{ }
	}
	public class InternelChild : InternelBase
	{ }

	internal class Country
	{
		public string name { get; }

		public Country(string name)
		{
			this.name = name;
		}

		public void Display()
		{
			Console.WriteLine($"Country: Welcome to {name}");
		}

		public void GetData(double number)
		{
			Console.WriteLine($"Country: {number}");
		}

		public virtual string GetTimeCulture()
		{
			return "UTC";
		}
	}

	internal class India : Country
	{
		//	public India() { } Will give compile time error
		public India() : base("India")
		{
		}

		protected India(string protectedName) : base(protectedName)
		{
		}

		public void Display()
		{
			Console.WriteLine($"India: Welcome to {name}");
		}

		public void GetData(int number)
		{
			Console.WriteLine($"India: {number}");
		}
	}

	internal class Maharashtra : India
	{
		//Can not call base("Maharashtra") without having costructor in base class with string parameter
		public Maharashtra() : base("Maharashtra")
		{
		}

		public new void Display()
		{
			Console.WriteLine($"Maharashtra: Welcome to {name}");
		}

		public void GetData(decimal number)
		{
			Console.WriteLine($"Maharashtra: {number}");
		}
	}

	internal class USA : Country
	{
		//	public India() { } Will give compile time error, work only if base class has empty public constructor
		public USA() : base("USA")
		{
		}

		public void GunShot()
		{
			Console.WriteLine($"lol, we can shoot anybody!");
		}

		public override string GetTimeCulture() //Override can't be declared as virtual
		{
			return base.GetTimeCulture();
		}
	}

	internal class Hawaii : Country
	{
		public Hawaii() : base("Hawaii")
		{
		}

		public virtual new string GetTimeCulture() //Sealed can only be called on override
		{
			return "HST";
		}
	}
	#endregion

	#region Interface Examples

	//Elements defined in a namespace cannot be explicitly declared as private, protected, or protected internal (internal allowed)
	//because these modifiers only make sense for members of a class.
	public interface ITestClass
	{
		void Method1();
		void Method2();
		void Method3();
		void Method4();
	}

	public class TestClass : ITestClass
	{
		public void Method1()
		{
			Console.WriteLine("Method1");
		}

		public void Method2()
		{
			Console.WriteLine("Method2");
		}

		public virtual void Method3() { }

		public virtual void Method4() { }
	}

	abstract class AbstractTestClass : ITestClass
	{
		public void Method1()
		{
			Console.WriteLine("Method1");
		}

		public void Method2()
		{
			Console.WriteLine("Method2");
		}

		public abstract void Method3();
		public abstract void Method4();
	}

	public class ChildTest : TestClass
	{
		public override void Method3() { Console.WriteLine("Method3"); }

		public override void Method4() { Console.WriteLine("Method4"); }
	}

	interface ISimpleCalculator
	{
		void Add();
		void Divide();
	}
	interface IComplexCalculator
	{
		void Add();
		void Base10();
	}

	class Calculator : ISimpleCalculator, IComplexCalculator
	{
		//This is explicit implementation
		void ISimpleCalculator.Add()
		{
			Console.WriteLine("Simple Calculations");
		}
		void IComplexCalculator.Add()
		{
			Console.WriteLine("Complex Calculations");
		}

		public void Divide()
		{
			Console.WriteLine("Divide Calculations");
		}

		public void Base10()
		{
			Console.WriteLine("Base10 Calculations");
		}
	}

	//Why cannot you have static method in Interface? 
	//Because Interface methods does not have implementation and can be overriden.Thera is no point in having 
	//static methods as they should have a body and cannot be overriden.
	#endregion
}
