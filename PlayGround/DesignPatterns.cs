using System;
using System.Collections.Generic;

namespace PlayGround
{
	/// <summary>
	///  Creational Patterns
	///  Abstract Factory - Creates an instance of several families of classes
	///  Builder - Separates object construction from its representation
	///  Factory -  Method Creates an instance of several derived classes
	///  Prototype - A fully initialized instance to be copied or cloned
	///  Singleton - A class of which only a single instance can exist
	///  --------------------------------------------------------------------
	///  Structural Patterns
	///  Adapter -  Match interfaces of different classes
	///  Bridge  -  Separates an object’s interface from its implementation
	///  Composite - A tree structure of simple and composite objects
	///  Decorator - Add responsibilities to objects dynamically
	///  Facade  -  A single class that represents an entire subsystem
	///  Flyweight - A fine-grained instance used for efficient sharing
	///  Proxy - An object representing another object
	///  --------------------------------------------------------------------
	///  Behavioral Patterns
	///  Chain of Resp - A way of passing a request between a chain of objects
	///  Command - Encapsulate a command request as an object
	///  Interpreter - A way to include language elements in a program
	///  Iterator - Sequentially access the elements of a collection
	///  Mediator - Defines simplified communication between classes
	///  Memento - Capture and restore an object's internal state
	///  Observer - A way of notifying change to a number of classes
	///  State -  Alter an object's behavior when its state changes
	///  Strategy - Encapsulates an algorithm inside a class
	///  Template - Method Defer the exact steps of an algorithm to a subclass
	///  Visitor - Defines a new operation to a class without change
	/// </summary>
	class DesignPatterns
	{
		public void Play()
		{
			new FacadeExample();
			new ObserverExample();
			new SingletonExample();
			new AdapterExample();
			new AbstractFactoryExample();
			new BuilderExample();
			new FactoryExample();
			new CommandExample();
			new CompositeExample();
			new DecoratorExample();
		}

		/// <summary>
		/// - Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative 
		///   to subclassing for extending functionality.
		/// - This structural code demonstrates the Decorator pattern which dynamically adds extra functionality to 
		///   an existing object.  
		/// </summary>
		class DecoratorExample
		{
			public DecoratorExample()
			{
				// Create book
				Book book = new Book("Worley", "Inside ASP.NET", 10);
				book.Display();

				// Create video
				Video video = new Video("Spielberg", "Jaws", 23, 92);
				video.Display();

				// Make video borrowable, then borrow and display
				Console.WriteLine("\nMaking video borrowable:");

				Borrowable borrowvideo = new Borrowable(video);
				borrowvideo.BorrowItem("Customer #1");
				borrowvideo.BorrowItem("Customer #2");

				borrowvideo.Display();

				// Wait for user
				Console.ReadKey();
			}

			/// <summary>
			/// The 'Component' abstract class
			/// </summary>
			abstract class LibraryItem
			{
				private int _numCopies;

				// Property
				public int NumCopies
				{
					get { return _numCopies; }
					set { _numCopies = value; }
				}

				public abstract void Display();
			}

			/// <summary>
			/// The 'ConcreteComponent' class
			/// </summary>
			class Book : LibraryItem
			{
				private string _author;
				private string _title;

				// Constructor
				public Book(string author, string title, int numCopies)
				{
					this._author = author;
					this._title = title;
					this.NumCopies = numCopies;
				}

				public override void Display()
				{
					Console.WriteLine("\nBook ------ ");
					Console.WriteLine(" Author: {0}", _author);
					Console.WriteLine(" Title: {0}", _title);
					Console.WriteLine(" # Copies: {0}", NumCopies);
				}
			}

			/// <summary>
			/// The 'ConcreteComponent' class
			/// </summary>
			class Video : LibraryItem
			{
				private string _director;
				private string _title;
				private int _playTime;

				// Constructor
				public Video(string director, string title,
				  int numCopies, int playTime)
				{
					this._director = director;
					this._title = title;
					this.NumCopies = numCopies;
					this._playTime = playTime;
				}

				public override void Display()
				{
					Console.WriteLine("\nVideo ----- ");
					Console.WriteLine(" Director: {0}", _director);
					Console.WriteLine(" Title: {0}", _title);
					Console.WriteLine(" # Copies: {0}", NumCopies);
					Console.WriteLine(" Playtime: {0}\n", _playTime);
				}
			}

			/// <summary>
			/// The 'Decorator' abstract class
			/// </summary>
			abstract class Decorator : LibraryItem
			{
				protected LibraryItem libraryItem;

				// Constructor
				public Decorator(LibraryItem libraryItem)
				{
					this.libraryItem = libraryItem;
				}

				public override void Display()
				{
					libraryItem.Display();
				}
			}

			/// <summary>
			/// The 'ConcreteDecorator' class
			/// </summary>
			class Borrowable : Decorator
			{
				protected List<string> borrowers = new List<string>();

				// Constructor
				public Borrowable(LibraryItem libraryItem)
				  : base(libraryItem)
				{
				}

				public void BorrowItem(string name)
				{
					borrowers.Add(name);
					libraryItem.NumCopies--;
				}

				public void ReturnItem(string name)
				{
					borrowers.Remove(name);
					libraryItem.NumCopies++;
				}

				public override void Display()
				{
					base.Display();

					foreach (string borrower in borrowers)
					{
						Console.WriteLine(" borrower: " + borrower);
					}
				}
			}
		}

		/// <summary>
		/// Composite pattern is used when we need to treat a group of objects and a single object in the same way. 
		/// Composite pattern composes objects in term of a tree structure to represent part as well as whole hierarchies.
		///This pattern creates a class contains group of its own objects.This class provides ways to modify its group of same objects.
		/// </summary>
		class CompositeExample
		{
			public CompositeExample()
			{
				var root = new CompositeElement("Picture");
				root.Add(new PrimitiveElement("Red Line"));
				root.Add(new PrimitiveElement("Blue Circle"));
				root.Add(new PrimitiveElement("Green Box"));

				var compositeNode = new CompositeElement("Two Circles");
				compositeNode.Add(new PrimitiveElement("Black Circle"));
				compositeNode.Add(new PrimitiveElement("White Circle"));

				root.Add(compositeNode);

				var pnode = new PrimitiveElement("Yellow Line");
				root.Add(pnode);
				root.Remove(pnode);

				root.Display(1);
			}

			/// <summary>
			/// The 'Component' Treenode
			/// </summary>
			abstract class DrawingElement
			{
				protected string _name;

				protected DrawingElement(string name)
				{
					_name = name;
				}


				public abstract void Add(DrawingElement d);
				public abstract void Remove(DrawingElement d);
				public abstract void Display(int indent);
			}

			/// <summary>
			/// The 'Leaf' class
			/// </summary>
			class PrimitiveElement : DrawingElement
			{
				public PrimitiveElement(string name) : base(name)
				{
				}

				public override void Add(DrawingElement c)
				{
					Console.WriteLine("Cannot add to a PrimitiveElement");
				}

				public override void Remove(DrawingElement c)
				{
					Console.WriteLine("Cannot remove from a PrimitiveElement");
				}

				public override void Display(int indent)
				{
					Console.WriteLine(new String('-', indent) + " " + _name);
				}
			}

			/// <summary>
			/// The 'Composite' class. It contains leaf objects
			/// </summary>
			class CompositeElement : DrawingElement
			{
				private List<DrawingElement> elements = new List<DrawingElement>();

				public CompositeElement(string name) : base(name)
				{
				}

				public override void Add(DrawingElement d)
				{
					elements.Add(d);
				}

				public override void Remove(DrawingElement d)
				{
					elements.Remove(d);
				}

				public override void Display(int indent)
				{
					Console.WriteLine(new String('-', indent) + "+ " + _name);

					// Display each child element on this node
					foreach (DrawingElement d in elements)
					{
						d.Display(indent + 2);
					}
				}
			}
		}


		/// <summary>
		/// In this pattern, a request is wrapped under an object as a command and passed to invoker object. 
		/// Invoker object pass the command to the appropriate object which can handle it and that object executes the command. 
		/// This handles the request in traditional ways like as queuing and callbacks.
		/// This pattern is commonly used in the menu systems of many applications such as Editor, IDE etc.
		/// </summary>
		class CommandExample
		{

			public CommandExample()
			{
				// Create user and let her compute
				User user = new User();

				// User presses calculator buttons
				user.Compute('+', 100);
				user.Compute('-', 50);
				user.Compute('*', 10);
				user.Compute('/', 2);

				// Undo 4 commands
				user.Undo(4);

				// Redo 3 commands
				user.Redo(3);

				// Wait for user
				Console.ReadKey();
			}

			/// <summary>
			/// The 'Command' abstract class
			/// </summary>
			interface ICommand
			{
				void Execute();
				void UnExecute();
			}

			/// <summary>
			/// The 'ConcreteCommand' class
			/// </summary>
			class CalculatorCommand : ICommand
			{
				private char _operator;
				private int _operand;
				private Calculator _calculator;
				public CalculatorCommand(Calculator calculator, char @operator, int operand)
				{
					this._calculator = calculator;
					this._operator = @operator;
					this._operand = operand;
				}

				// Gets operator
				public char Operator
				{
					set { _operator = value; }
				}

				// Get operand
				public int Operand
				{
					set { _operand = value; }
				}

				// Execute new command
				public void Execute()
				{
					_calculator.Operation(_operator, _operand);
				}

				// Unexecute last command
				public void UnExecute()
				{
					_calculator.Operation(Undo(_operator), _operand);
				}

				// Returns opposite operator for given operator
				private char Undo(char @operator)
				{
					switch (@operator)
					{
						case '+': return '-';
						case '-': return '+';
						case '*': return '/';
						case '/': return '*';
						default:
							throw new ArgumentException("@operator");
					}
				}
			}

			/// <summary>
			/// The 'Receiver' class
			/// </summary>
			class Calculator
			{
				private int _curr = 0;

				public void Operation(char @operator, int operand)
				{
					switch (@operator)
					{
						case '+': _curr += operand; break;
						case '-': _curr -= operand; break;
						case '*': _curr *= operand; break;
						case '/': _curr /= operand; break;
					}
					Console.WriteLine("Current value = {0,3} (following {1} {2})", _curr, @operator, operand);
				}
			}

			/// <summary>
			/// The 'Invoker' class
			/// </summary>
			class User
			{
				private Calculator _calculator = new Calculator();
				private List<ICommand> _commands = new List<ICommand>();
				private int _current = 0;

				public void Redo(int levels)
				{
					Console.WriteLine("\n---- Redo {0} levels ", levels);
					for (int i = 0; i < levels; i++)
					{
						if (_current < _commands.Count - 1)
						{
							ICommand command = _commands[_current++];
							command.Execute();
						}
					}
				}

				public void Undo(int levels)
				{
					Console.WriteLine("\n---- Undo {0} levels ", levels);
					for (int i = 0; i < levels; i++)
					{
						if (_current > 0)
						{
							ICommand command = _commands[--_current];
							command.UnExecute();
						}
					}
				}

				public void Compute(char @operator, int operand)
				{
					// Create command operation and execute it
					ICommand command = new CalculatorCommand(
					  _calculator, @operator, operand);
					command.Execute();

					// Add command to undo list
					_commands.Add(command);
					_current++;
				}
			}
		}

		/// <summary>
		/// In Factory pattern, we create object without exposing the creation logic. In this pattern, an interface is used for creating an object,
		/// but let subclass decide which class to instantiate. 
		/// The creation of object is done when it is required. The Factory method allows a class later instantiation to subclasses.
		/// </summary>
		class FactoryExample
		{
			public FactoryExample()
			{
				Document[] documents = new Document[2];

				documents[0] = new Resume();
				documents[1] = new Report();

				foreach (var item in documents)
				{
					Console.WriteLine("\n" + item.GetType().Name + "--");
					foreach (IPage page in item.Pages)
					{
						Console.WriteLine(page.GetType().Name + " " + page.Title);
					}
				}

			}

			/// <summary>
			/// The 'Product' abstract class
			/// </summary>
			interface IPage
			{
				string Title { get; }
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class SkillsPage : IPage
			{
				public string Title
				{
					get
					{
						return "Dot Net";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class EducationPage : IPage
			{
				public string Title
				{
					get
					{
						return "BE - IT";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class ExperiencePage : IPage
			{
				public string Title
				{
					get
					{
						return "3.7 Years";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class IntroductionPage : IPage
			{
				public string Title
				{
					get
					{
						return "My self Blah-Blah";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class ResultsPage : IPage
			{
				public string Title
				{
					get
					{
						return "Fucking awesome";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class ConclusionPage : IPage
			{
				public string Title
				{
					get
					{
						return "lol.. No conclusion, if you're engineer! :D";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class SummaryPage : IPage
			{
				public string Title
				{
					get
					{
						return "In summer you will get summary!";
					}
				}
			}

			/// <summary>
			/// A 'ConcreteProduct' class
			/// </summary>
			class BibliographyPage : IPage
			{
				public string Title
				{
					get
					{
						return "Ohh I see...";
					}
				}
			}

			/// <summary>
			/// The 'Creator' abstract class
			/// </summary>
			abstract class Document
			{
				private List<IPage> _pages = new List<IPage>();

				public Document()
				{
					this.CreatePages();
				}

				public List<IPage> Pages
				{
					get { return _pages; }
				}

				public abstract void CreatePages();
			}

			/// <summary>
			/// A 'ConcreteCreator' class
			/// </summary>
			class Resume : Document
			{
				// Factory Method implementation
				public override void CreatePages()
				{
					Pages.Add(new SkillsPage());
					Pages.Add(new EducationPage());
					Pages.Add(new ExperiencePage());
				}
			}

			/// <summary>
			/// A 'ConcreteCreator' class
			/// </summary>
			class Report : Document
			{
				// Factory Method implementation
				public override void CreatePages()
				{
					Pages.Add(new IntroductionPage());
					Pages.Add(new ResultsPage());
					Pages.Add(new ConclusionPage());
					Pages.Add(new SummaryPage());
					Pages.Add(new BibliographyPage());
				}
			}
		}


		/// <summary>
		/// Builder pattern builds a complex object by using a step by step approach. Builder interface defines the steps to build the final object. 
		/// This builder is independent from the objects creation process. A class that is known as Director, controls the object creation process.
		/// Moreover, builder pattern describes a way to separate an object from its construction.The same construction method can create different representation 
		/// of the object.
		/// </summary>
		class BuilderExample
		{
			public BuilderExample()
			{
				VehicleBuilder _builder;

				var shop = new Shop();

				_builder = new ScooterBuilder();
				shop.Construct(_builder);
				_builder.Vehicle.show();

				_builder = new MotorCycleBuilder();
				shop.Construct(_builder);
				_builder.Vehicle.show();

				_builder = new CarBuilder();
				shop.Construct(_builder);
				_builder.Vehicle.show();

			}

			/// <summary>
			/// The 'Director' class
			/// </summary>
			class Shop
			{
				public void Construct(VehicleBuilder vehicleBuilder)
				{
					vehicleBuilder.BuildFrame();
					vehicleBuilder.BuildEngine();
					vehicleBuilder.BuildWheels();
					vehicleBuilder.BuildDoors();
				}
			}

			/// <summary>
			/// The 'Builder' abstract class
			/// </summary>
			abstract class VehicleBuilder
			{
				protected Vehicle vehicle;

				// Gets vehicle instance
				public Vehicle Vehicle
				{
					get { return vehicle; }
				}

				// Abstract build methods
				public abstract void BuildFrame();
				public abstract void BuildEngine();
				public abstract void BuildWheels();
				public abstract void BuildDoors();
			}

			/// <summary>
			/// The 'ConcreteBuilder1' class
			/// </summary>
			class MotorCycleBuilder : VehicleBuilder
			{
				public MotorCycleBuilder()
				{
					vehicle = new Vehicle("Motor Cycle");
				}

				public override void BuildFrame()
				{
					vehicle["frame"] = "MotorCycle Frame";
				}

				public override void BuildEngine()
				{
					vehicle["engine"] = "500 cc";
				}

				public override void BuildWheels()
				{
					vehicle["wheels"] = "2";
				}

				public override void BuildDoors()
				{
					vehicle["doors"] = "0";
				}
			}

			/// <summary>
			/// The 'ConcreteBuilder2' class
			/// </summary>
			class CarBuilder : VehicleBuilder
			{
				public CarBuilder()
				{
					vehicle = new Vehicle("Car");
				}

				public override void BuildFrame()
				{
					vehicle["frame"] = "Car Frame";
				}

				public override void BuildEngine()
				{
					vehicle["engine"] = "2500 cc";
				}

				public override void BuildWheels()
				{
					vehicle["wheels"] = "4";
				}

				public override void BuildDoors()
				{
					vehicle["doors"] = "4";
				}
			}

			/// <summary>
			/// The 'ConcreteBuilder3' class
			/// </summary>
			class ScooterBuilder : VehicleBuilder
			{
				public ScooterBuilder()
				{
					vehicle = new Vehicle("Scooter");
				}

				public override void BuildFrame()
				{
					vehicle["frame"] = "Scooter Frame";
				}

				public override void BuildEngine()
				{
					vehicle["engine"] = "50 cc";
				}

				public override void BuildWheels()
				{
					vehicle["wheels"] = "2";
				}

				public override void BuildDoors()
				{
					vehicle["doors"] = "0";
				}
			}


			/// <summary>
			///  The 'Product' class
			/// </summary>
			class Vehicle
			{
				private string _VehicleType;
				private Dictionary<string, string> _parts = new Dictionary<string, string>();

				public Vehicle(string vehicalType)
				{
					_VehicleType = vehicalType;
				}

				public string this[string key]
				{
					get { return _parts[key]; }
					set { _parts[key] = value; }
				}

				public void show()
				{
					Console.WriteLine("\n---------------------------");
					Console.WriteLine("Vehicle Type: {0}", _VehicleType);
					Console.WriteLine(" Frame : {0}", _parts["frame"]);
					Console.WriteLine(" Engine : {0}", _parts["engine"]);
					Console.WriteLine(" #Wheels: {0}", _parts["wheels"]);
					Console.WriteLine(" #Doors : {0}", _parts["doors"]);
				}
			}
		}

		/// <summary>
		/// Provide an interface for creating families of related or dependent objects without specifying their concrete.
		/// Abstract Factory patterns acts a super-factory which creates other factories. This pattern is also called as Factory of factories.
		/// In Abstract Factory pattern an interface is responsible for creating a set of related objects, or dependent objects without specifying 
		/// their concrete classes.
		/// </summary>
		class AbstractFactoryExample
		{

			public AbstractFactoryExample()
			{
				var asia = new AsianFactory();
				var world = new AnimalWorld(asia);
				world.RunFoodChain();

				Console.WriteLine("");

				var africa = new AfricanFactory();
				world = new AnimalWorld(africa);
				world.RunFoodChain();
			}

			/// <summary>
			/// AbstractProductA
			/// </summary>
			interface IHerbivore { void Eat(); }

			/// <summary>
			/// AbstractProductB
			/// </summary>
			interface ICarnivore { void Eat(IHerbivore food); }

			/// <summary>
			/// AbstractFactory
			/// </summary>
			interface IContinentFactory
			{
				IHerbivore CreateHerbivore();
				ICarnivore CreateCarnivore();
			}

			/// <summary>
			/// ConcreteFactory1
			/// </summary>
			class AsianFactory : IContinentFactory
			{
				public ICarnivore CreateCarnivore()
				{
					return new Wolf();
				}

				public IHerbivore CreateHerbivore()
				{
					return new Bison();
				}
			}

			/// <summary>
			/// ConcreteFactory2
			/// </summary>
			class AfricanFactory : IContinentFactory
			{
				public ICarnivore CreateCarnivore()
				{
					return new Lion();
				}

				public IHerbivore CreateHerbivore()
				{
					return new WildBeast();
				}
			}

			/// <summary>
			/// AbstractProductA's ProductA1
			/// </summary>
			class WildBeast : IHerbivore
			{
				public void Eat()
				{
					Console.WriteLine(this.GetType().Name + " eats vegan");
				}
			}

			/// <summary>
			/// AbstractProductB's ProductB1
			/// </summary>
			class Lion : ICarnivore
			{
				public void Eat(IHerbivore food)
				{
					Console.WriteLine(this.GetType().Name + " eats " + food.GetType().Name);
				}
			}

			/// <summary>
			/// AbstractProductA's ProductA2
			/// </summary>
			class Bison : IHerbivore
			{
				public void Eat()
				{
					Console.WriteLine(this.GetType().Name + " eats vegan");
				}
			}

			/// <summary>
			/// AbstractProductB's ProductB2
			/// </summary>
			class Wolf : ICarnivore
			{
				public void Eat(IHerbivore food)
				{
					Console.WriteLine(this.GetType().Name + " eats " + food.GetType().Name);
				}

			}

			/// <summary>
			/// Client
			/// </summary>
			class AnimalWorld
			{
				private IHerbivore _herbivore;
				private ICarnivore _carnivore;

				public AnimalWorld(IContinentFactory factory)
				{
					_herbivore = factory.CreateHerbivore();
					_carnivore = factory.CreateCarnivore();
				}

				public void RunFoodChain()
				{
					_herbivore.Eat();
					_carnivore.Eat(_herbivore);
				}
			}
		}

		/// <summary>
		/// Convert the interface of a class into another interface that clients expect. 
		/// Adapter lets classes work together that couldn't otherwise because of incompatible interfaces
		/// To understand this definition, let's use a  real-world simple example. You know a language, "A". 
		/// You visit a place where language "B" is spoken. But you don't know how to speak that language. 
		/// So you meet a person who knows both languages, in other words A and B. 
		/// So he can act as an adapter for you to talk with the local people of that place and help you communicate with them.
		/// </summary>
		class AdapterExample
		{
			public AdapterExample()
			{
				var targetAdapter1 = new GreetingsInSpanish(new Spanish());
				targetAdapter1.Speak();

				var targetAdapter2 = new GreetingsChinese(new China());
				targetAdapter2.Speak();
			}

			public class Spanish
			{
				public void hablar()
				{
					Console.WriteLine("Hola Amigos!");
				}
			}

			public class China
			{
				public void Shuo()
				{
					Console.WriteLine("Ni Hao pengyou!!!");
				}
			}

			/// <summary>
			/// We can use class as well
			/// </summary>
			public interface ITargetAdapter
			{
				void Speak();
			}

			public class GreetingsInSpanish : ITargetAdapter
			{
				private Spanish _spanish { get; set; }

				public GreetingsInSpanish(Spanish _spanish)
				{
					this._spanish = _spanish;
				}

				public void Speak()
				{
					_spanish.hablar();
				}
			}

			public class GreetingsChinese : ITargetAdapter
			{
				private China _chinese { get; set; }

				public GreetingsChinese(China _chinese)
				{
					this._chinese = _chinese;
				}

				public void Speak()
				{
					_chinese.Shuo();
				}
			}
		}


		/// <summary>
		/// Ensure a class has only one instance and provide a global point of access to it.
		/// </summary>
		class SingletonExample
		{
			public SingletonExample()
			{
				LoadBalancer b1 = LoadBalancer.GetInstance();
				LoadBalancer b2 = LoadBalancer.GetInstance();
				LoadBalancer b3 = LoadBalancer.GetInstance();
				LoadBalancer b4 = LoadBalancer.GetInstance();

				if (b1 == b2 && b2 == b3 && b3 == b4)
					Console.WriteLine("Same innstance");

				LoadBalancer balancer = LoadBalancer.GetInstance();

				for (int i = 0; i < 20; i++)
				{
					string server = balancer.Server;
					Console.WriteLine("Dispatch Request no {0}", server);
				}
			}

			class LoadBalancer
			{
				private static LoadBalancer _loadBalancer;
				private List<string> _servers = new List<string>();
				private Random _random = new Random();

				private static object syncLock = new object();
				protected LoadBalancer()
				{
					_servers.Add("Server 1");
					_servers.Add("Server 2");
					_servers.Add("Server 3");
					_servers.Add("Server 4");
					_servers.Add("Server 5");
				}

				public static LoadBalancer GetInstance()
				{
					if (_loadBalancer == null)
					{
						lock (syncLock)
						{
							if (_loadBalancer == null)
								_loadBalancer = new LoadBalancer();
						}
					}

					return _loadBalancer;
				}

				public string Server
				{
					get
					{
						int r = _random.Next(_servers.Count);
						return _servers[r].ToString();
					}
				}
			}
		}

		/// <summary>
		/// Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
		/// The Observer pattern is used when the change of a state in one object must be reflected in another object without keeping the objects tightly coupled.
		/// </summary>
		class ObserverExample
		{
			public ObserverExample()
			{
				IBM ibm = new IBM("IBM", 120.00);
				ibm.Attach(new Observer("Daddy yanky"));
				ibm.Attach(new Observer("Sean Paul"));
				ibm.Attach(new Observer("Nikhilesh shinde"));

				ibm.Price = 120.11;
				ibm.Price = 120.01;
				ibm.Price = 120.61;
			}

			/// <summary>
			/// The 'Observer' interface
			/// </summary>
			public interface IInvestor
			{
				void Update(Stock stock);
			}

			/// <summary>
			/// The 'ConcreteObserver' class
			/// </summary>
			public class Observer : IInvestor
			{
				private string _name;
				private Stock _stock;

				public Observer(string name)
				{ _name = name; }

				public void Update(Stock stock)
				{
					Console.WriteLine("Notified {0} of {1}'s " + "change to {2:C}", _name, stock.Symbol, stock.Price);
				}

				public Stock Stock
				{
					get { return _stock; }
					set { _stock = value; }
				}
			}

			/// <summary>
			/// The 'Subject' abstract class  
			/// </summary>
			public abstract class Stock
			{
				private string _symbol;

				private double _price;

				private List<IInvestor> investorList = new List<IInvestor>();

				public Stock(string symbol, double price)
				{
					_symbol = symbol;
					_price = price;
				}

				public void Attach(IInvestor investor)
				{
					investorList.Add(investor);
				}

				public void Detach(IInvestor investor)
				{
					investorList.Remove(investor);
				}

				public void Notify()
				{
					foreach (IInvestor o in investorList)
					{
						o.Update(this);
					}
				}

				public string Symbol
				{
					get { return _symbol; }
				}
				public double Price
				{
					get { return _price; }
					set
					{
						if (_price != value)
						{
							_price = value;
							Notify();
						}
					}
				}
			}


			/// <summary>
			/// The 'ConcreteSubject' class
			/// </summary>
			public class IBM : Stock
			{
				public IBM(string symbol, double price)
					 : base(symbol, price)
				{ }
			}

		}


		/// <summary>
		/// Provide a unified interface to a set of interfaces in a subsystem. Façade defines a higher-level interface that makes the subsystem easier to use.
		/// Facade pattern hides the complexities of the system and provides an interface to the client using which the client can access the system.
		/// The facade design pattern is particularly used when a system is very complex or difficult to understand because system has a large number of 
		/// interdependent classes or its source code is unavailable.
		/// </summary>
		class FacadeExample
		{
			public FacadeExample()
			{
				List<Customer> customerList = new List<Customer>()
				{
					 new Customer() { CustomerID = 111, CustomerName = "Henrich himmler", CustomerSaving = 0, CustomerCredit = 2500, CustomerLoan = 0 },
					 new Customer() { CustomerID = 112, CustomerName = "Adolf Hitler", CustomerSaving = 1000, CustomerCredit = -500, CustomerLoan = 0 },
					 new Customer() { CustomerID = 113, CustomerName = "Martin Borman", CustomerSaving = 1200, CustomerCredit = 1000, CustomerLoan = 120 },
					 new Customer() { CustomerID = 114, CustomerName = "Eva brown", CustomerSaving = 1500, CustomerCredit = 1500, CustomerLoan = 0 }
				};

				Mortgage mortgage = new Mortgage();

				foreach (Customer cust in customerList)
				{
					if (mortgage.IsEligible(cust))
						Console.WriteLine("Customer {0}/{1} is Eligible", cust.CustomerID, cust.CustomerName);
					else
						Console.WriteLine("Customer {0}/{1} is not Eligible", cust.CustomerID, cust.CustomerName);
				}
			}

			/// <summary>
			/// Customer class
			/// </summary>
			class Customer
			{
				private string _CustomerName;
				private int _CustomerID;
				private double _CustomerCredit;
				private double _CustomerLoan;
				private double _CustomerSaving;

				public Customer()
				{
				}

				public int CustomerID
				{
					get { return _CustomerID; }
					set { _CustomerID = value; }
				}

				public string CustomerName
				{
					get { return _CustomerName; }
					set { _CustomerName = value; }
				}

				public double CustomerLoan
				{
					get { return _CustomerLoan; }
					set { _CustomerLoan = value; }
				}

				public double CustomerCredit
				{
					get { return _CustomerCredit; }
					set { _CustomerCredit = value; }
				}

				public double CustomerSaving
				{
					get { return _CustomerSaving; }
					set { _CustomerSaving = value; }
				}
			}

			/// <summary>
			/// The 'Subsystem ClassA' class
			/// </summary>
			class Bank
			{
				public bool HasEnoughSaving(Customer cust)
				{
					Console.WriteLine("Customer {0} has saving of {1}", cust.CustomerName, cust.CustomerSaving);
					if (cust.CustomerSaving > 500)
						return true;
					else
						return false;
				}
			}

			/// <summary>
			/// The 'Subsystem ClassB' class
			/// </summary>
			class Credit
			{
				public bool HasGoodCredit(Customer cust)
				{
					Console.WriteLine("Customer {0} has Credit of {1}", cust.CustomerName, cust.CustomerCredit);
					if (cust.CustomerCredit > 0)
						return true;
					else
						return false;
				}
			}

			/// <summary>
			/// The 'Subsystem ClassC' class
			/// </summary>
			class Loan
			{
				public bool HasGoodLoan(Customer cust)
				{
					Console.WriteLine("Customer {0} has Loan of {1}", cust.CustomerName, cust.CustomerLoan);
					if (cust.CustomerLoan == 0)
						return true;
					else
						return false;
				}
			}

			/// <summary>
			/// The 'Facade' class
			/// This pattern involves a single wrapper class which contains a set of members which are required by client. 
			/// These members access the system on behalf of the facade client and hide the implementation details.
			/// </summary>
			class Mortgage
			{
				private Bank _bank = new Bank();
				private Credit _credit = new Credit();
				private Loan _loan = new Loan();
				private bool eligible = false;

				public Mortgage()
				{
				}

				public bool IsEligible(Customer cust)
				{
					eligible = _bank.HasEnoughSaving(cust);
					eligible = _credit.HasGoodCredit(cust) && eligible;
					eligible = _loan.HasGoodLoan(cust) && eligible;

					return eligible;
				}
			}
		}
	}
}