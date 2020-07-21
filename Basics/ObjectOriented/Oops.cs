using System;

namespace ObjectOriented
{
    internal class Oops
	{
		public Oops()
		{
            MethodHidingExample();
            Polymorphism();
            AbstractClassExample();
            ClassExample();
            ConstructorsExample();
            InterfaceExample();
        }


		#region Abstraction and Encapsulation
		/// -------------------------------------------- Abstraction -------------------------------------------------------------------------
		/// 
		/// - The essence of abstractions is preserving information that is relevant in a given context, and forgetting information that 
		///   is irrelevant in that context.
		/// 
		/// - Abstraction hides complexity by giving you a more abstract picture, In other words, Abstraction hides details at the design
		///   level
		/// 
		/// - For example, when you first describe an object, you talk in more abstract term e.g. a Vehicle which can move, you don't tell
		///   how Vehicle will move, whether it will move by using tires or it will fly or it will sell. It just moves. This is called 
		///   Abstraction. We are talking about a most essential thing, which is moving, rather than focusing on details like moving in plane,
		///   sky, or water.
		///   
		/// - In general, An abstraction is a type that describes a contract but does not provide a full implementation of the contract. 
		///   Abstractions are usually implemented as abstract classes or interfaces, and they come with a well-defined set of reference 
		///   documentation describing the required semantics of the types implementing the contract.
		///   
		/// - Abstraction can be achieved using abstract classes in C#. C# allows you to create abstract classes that are used to provide a 
		///   partial class implementation of an interface. Implementation is completed when a derived class inherits from it. Abstract classes
		///   contain abstract methods, which are implemented by the derived class. The derived classes have more specialized functionality.
		///   
		///  - Some of the most important abstractions in the .NET Framework include Stream, IEnumerable<T>, and Object.
		///   
		///  - Advantages of Abstraction
		///    > It reduces the complexity of viewing the things.
		///    > Avoids code duplication and increases reusability.
		///    > Helps to increase security of an application or program as only important details are provided to the user
		///   
		/// -------------------------------------------- Encapsulation -------------------------------------------------------------------------
		/// 
		/// - Means bundling the related functionality together and giving access to only the needful.Encapsulation hides internal working so 
		///   that you can change it later
		/// 
		/// - Encapsulation hides details at the implementation level, Encapsulation is all about implementation. Its sole purpose is to hide 
		///   internal working of objects from outside world so that you can change it later without impacting outside clients.
		///   
		/// - For example, we have a Dictionary which allows you to store the object using Add() method and retrieve the object using the 
		///   GetValue() method. How Dictionary implements this method is an internal detail of Dictionary, the client only cares that put 
		///   stores the object and get return it back, they are not concerned Dictionary is using an array, how it is resolving the collision, 
		///   whether it is using linked list or binary tree to store object landing on same bucket etc. Because of Encapsulation, you can change
		///   the internal implementation of Dictionary with ease without impacting clients who are using it.
		///   
		/// - Encapsulation means that a group of related properties, methods, and other members are treated as a single unit or object. 
		///   Encapsulation is implemented by using access specifiers. An access specifier defines the scope and visibility of a class member. 
		///   E.g. Public, Private, Protected, Internal, Protected internal
		///   
		/// --------------------------------------------------------------------------------------------------------------------------------------

		// Another way to achieve abstraction in C#, is with interfaces.
		interface ISpecialAbilities
		{
			void ShowAbilities();
		}

		// Abstract class
		abstract class AnimalBase
		{
			// Abstract method (does not have a body)
			public abstract void AnimalSound();
			// Regular method
			public void Sleep()
			{
				Console.WriteLine("Zzz");
			}
		}

		// Derived class (inherit from Animal)
		class Wolf : AnimalBase, ISpecialAbilities
		{
			public override void AnimalSound()
			{
				Console.WriteLine("The Wolf Says: Hello there!");
			}

			public void ShowAbilities()
			{
				Console.WriteLine("Wolf Can Fly");
			}
		}

		class EndUser
		{
			public void Play()
			{
				Wolf wolfGang = new Wolf(); // Create a Pig object
				wolfGang.AnimalSound();  // Call the abstract method
				wolfGang.Sleep();  // Call the regular method
				wolfGang.ShowAbilities();

				ISpecialAbilities specialAbilities = wolfGang;
				specialAbilities.ShowAbilities(); //Can access the only contract methods
			}
		}

		#endregion

		#region Inheritance

		/// - Inheritance is one of the fundamental attributes of object-oriented programming. It allows you to define a child class that reuses (inherits), 
		///   extends, or modifies the behavior of a parent class
		/// - Conceptually, a derived class is a specialization of the base class.  
		/// - A Mammal is an Animal, and a Reptile is an Animal, but each derived class represents different specializations of the base class.
		/// - Ordinarily, inheritance is used to express an "is a" relationship between a base class and one or more derived classes, where the derived classes 
		///   are specialized versions of the base class; the derived class is a type of the base class.
		/// - https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/inheritance
		/// - “A Cat is a type of Animal” is a parent-child relationship. This is an inheritance relationship. The inheritance relationship is 
		///   identified by the words “is a”.
		///    > Generalization is the term that we use to denote abstraction of common properties into a base class in UML. The UML diagram's Generalization 
		///      association is also known as Inheritance
		///    > Specialization is the reverse process of Generalization means creating new sub-classes from an existing class.
		private void Inheritance()
		{
			var cat = new Cat("Tiger", new Classification
			{
				Kingdom = "Animalia",
				Phylum = "Chordata",
				Class = "Mammalia",
				Order = "Carnivora",
				Family = "Felidae",
				Genus = "Panthera",
				Species = "Panthera tigris"
			});
		}

		/// <summary>
		/// - Realization is a relationship between the blueprint class and the object containing its respective implementation level details.
		/// - This object is said to realize the blueprint class. In other words, you can understand this as the relationship between the interface 
		///   and the implementing class.
		/// </summary>
		interface IOrganism
		{
			string Name { get; set; }
		}

		abstract class Organism : IOrganism
		{
			public string Name { get; set; }

			/// <summary>
			/// - Composition implies a relationship where the child cannot exist independent of the parent
			/// - In a more specific manner, a restricted aggregation is called composition
			/// - Here Classification could not exists if there is no Organism
			/// </summary>
			public Classification Classification { get; set; }

			public Organism(string name)
			{
				Name = name;
			}
		}

		/// <summary>
		/// - 
		/// </summary>
		class Cat : Organism
		{
			/// <summary>
			/// - Aggregation implies a relationship where the child can exist independently of the parent
			/// - When an object ‘has-a’ another object, then you have got an aggregation between them
			/// - Here tail can exists without Cat. So Delete the Cat and tail can stil exists
			/// </summary>
			public Tail tail { get; set; }

			/// <summary>
			/// - Change in structure or behaviour of a class affects the other related class, then there is a dependency between those two classes. 
			/// - It need not be the same vice-versa. When one class contains the other class it this happens (Classification).
			/// </summary>
			public Cat(string name, Classification classification) : base(name)
			{
				this.Classification = classification;
			}

			/// <summary>
			/// - We talk about association between two objects when each one of them can use the other one, but also each one of them can exist without 
			///   the other one. There is no dependency between them.
			///   Eg. Owners feed pets, pets please owners (association), we can see the relationship “has a”. That means a Owner can exist without his Pet, 
			///   and his Pet can also be assigned to another Owner. 
			/// - If two classes in a model need to communicate with each other, there must be a link between them, and that can be represented by an 
			///   association (connector)
			/// - Association can be represented by a line between these classes with an arrow indicating the navigation direction. In case an arrow is on 
			///   both sides, the association is known as a bidirectional association.
			/// - You may be aware of one-to-one, one-to-many, many-to-one, many-to-many all these words define an association between objects.
			/// </summary>
			public void PetOf(Owner owner)
			{
				owner.FeedsTo(this);
			}
		}

		class Tail
		{
			public string Type { get; set; }
		}

		class Classification
		{
			public string Kingdom;
			public string Phylum;
			public string Class;
			public string Order;
			public string Family;
			public string Genus;
			public string Species;
		}

		class Owner
		{
			public string Name { get; set; }

			public void FeedsTo(Organism organism)
			{
				Console.WriteLine($"{organism.Name} feeds by owner {Name}");
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

		private void Polymorphism()
		{
			Console.WriteLine("-- Static Polymorphism (overload) --");

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
		private void MethodHidingExample()
		{
			Console.WriteLine("-- Method Hiding --");
			Baap obj1 = new Baap();
			obj1.Display(); //"Display Baap"
			Beta obj2 = new Beta();
			obj2.Display(); //"Display Beta"
			Baap obj3 = new Beta();
			obj3.Display(); //"Display Baap"
		}

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

		#region Abstract Class
		/// <summary>
		/// - The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
		/// - An abstract class cannot be instantiated. The purpose of an abstract class is to provide a common definition of a base class that 
		///   multiple derived classes can share.
		/// - You can consider an abstract class to be an interface, which already has some implementation
		/// - Inshort Abstract classes are used for Modelling a class hierarchy of similar looking classes (For example Animal can be abstract class 
		///   and Human , Lion, Tiger can be concrete derived classes)
		///	- Whereas Interface is used for Communication between 2 unrelated classes which does not care about type of the class implementing
		///	  Interface (e.g. Height can be interface property and it can be implemented by Human , Building , Tree. It does not matter if you can eat , 
		///	  you can swim you can die or anything.. it matters only a thing that you need to have Height)
		/// </summary>
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

			//Not Pure virtual function
			public virtual void DisplayAmount()
			{
				Console.WriteLine($"AbstractAccountBase: Amount {amount} | Account {accountNumber} | Opened at {OpeningDate}");
			}

			//Pure virtual function
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

			public override void LoanApproval()
			{
				if (amount > 50000)
					Console.WriteLine($"Loan Approved");
				else
					Console.WriteLine($"Loan Rejcted");
			}
		}

		public class XAccount : SalaryAccount
        {
            public XAccount(string accountNumber, decimal amount, int duration) : base(accountNumber, amount, duration)
            {
            }

			//public override void DisplayAmount() {} //error as sealer in parent class

			public override void LoanApproval()
			{
				if (amount > 50000)
					Console.WriteLine($"Loan Approved");
				else
					Console.WriteLine($"Loan Rejcted");
			}
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

		private void ConstructorsExample()
		{
			Console.WriteLine("-- Constructors --");
			///ProtectedBase protectedBase = new ProtectedBase(); //Will give error becuase not parameterless public constructor
			ProtectedBase protectedChild = new ProtectedChild
			{
				value = "value" //ProtectedBase property
			};//Will work becuase of having internal constructor

			///InternelBase internelBase = new InternelBase(); //Will give error becuase not parameterless public constructor
			InternelBase internelChild = new InternelChild
			{
				value = "Test"
			};
		}

		#endregion

		#region Interface Examples
		/// <summary>
		/// - An interface defines a contract. Any class or struct that implements that contract must provide an implementation of the members 
		///   defined in the interface.
		/// - It says that a class which implements the interface agrees to implement all of the functions declared 
		///   (as signatures only; no function definition) by that interface.
		/// - Function declarations within an interface are implicitly pure virtual.
		/// - Why cannot you have static method in Interface? 
		///     > Because Interface methods does not have implementation and can be overriden.There is no point in having 
		///     > static methods as they should have a body and cannot be overriden.
		/// - Using interface-based design concepts provides 
		///     > Loose coupling
		///     > Extensibility
		///     > Implementation Hiding 
		///     > Accessing object through interfaces
		///     > Easier maintainability 
		///     > Makes your code  base more scalable
		///     > Makes code reuse much more accessible because the implementation is separated from the interface
		/// - Interfaces add a plug and play like architecture into your applications. Interfaces help define a contract 
		///   (agreement or blueprint, however you chose to define it), between your application and other objects. This indicates what sort of 
		///   methods, properties, and events are exposed by an object.
		/// </summary>
		public interface ITestClass
		{
			double X { get; set; }
			void Method1();
			void Method2();
			void Method3();
			void Method4();
		}

		public class TestClass : ITestClass
		{
			public double X
			{
				get => throw new NotImplementedException();
				set => throw new NotImplementedException();
			}

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

		/// <summary>
		/// - In practical terms, the difference between the abstract class & interface is that an interface defines only pure virtual functions, 
		///   while an abstract class may also include concrete functions, members, or any other aspect of a class.
		/// </summary>
		abstract class AbstractTestClass : ITestClass
		{
			public double X
			{
				get => throw new NotImplementedException();
				set => throw new NotImplementedException();
			}

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
	}
}
