using System;
using System.Collections.Generic;

namespace DotNetDemos.CSharpExamples.OOPBasic
{
    class OOPBasics
    {
        public void Play()
        {
            //Construtor
            //Child obj = new Child();
            //Child obj = new Child(23);

            //Method Hiding
            Baap obj1 = new Baap();
            obj1.Display();
            Beta obj2 = new Beta();
            obj2.Display();
            Baap obj3 = new Beta();
            obj3.Display();
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
            //Break
        }

        public void Steering()
        {
            //Steer
        }

        public void Engine()
        {
            //Engine
        }

        private void Oil()
        {
            //Oil capacity
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
    /// - Polymorphism is a consequence of inheritance. Inheriting a method from parent is useful, but being able to 
    ///   modify a method if the situation demands, is polymorphism. 
    /// - You may implement a method in the subclass with exactly the same signature as in parent class so that when
    ///   called, the method from child class is executed. This is polymorphism.
    /// - The word polymorphism means having many forms. In object-oriented programming paradigm, polymorphism is 
    ///   often expressed as 'one interface, multiple functions'. 
    /// - Static Polymorphism: 
    ///    The mechanism of linking a function with an object during compile time is called early
    ///    binding. It is also called static binding. C# provides two techniques to implement static polymorphism. 
    ///    They are: 
    ///     1. Function overloading
    ///     2. Operator overloading
    /// - Dynamic  Polymorphism:  
    ///    C# allows you to create abstract classes that are used to provide partial class implementation of an interface.
    ///    Implementation is completed when a derived class inherits from it. Abstract classes contain abstract methods,
    ///    which are implemented by the derived class. The derived classes have more specialized functionality.
    ///    Here are the rules about abstract classes:
    ///     1. You cannot create an instance of an abstract class
    ///     2. You cannot declare an abstract method outside an abstract class
    ///     3. When a class is declared sealed, it cannot be inherited, abstract classes cannot be declared sealed.

    class StaticPolymorphism
    {
        public StaticPolymorphism()
        {

        }

        public int Calculate(int x, int y)
        {
            return x + y;
        }

        //Wont work as overloading does not diffentiate based on return type
        //public double Calculate(int x, int y)
        //{
        //    return x + y;
        //}

        //Will work as it having different params
        public double Calculate(int x, double y)
        {
            return x + y;
        }
    }

    abstract class DynamicPolymorphism
    {
        public virtual double calculate(int x, int y)
        {
            return x + y;
        }
        public abstract void print();
    }

    class DynamicPolymorphismTest1 : DynamicPolymorphism
    {
        public override double calculate(int x, int y)
        {
            return x + y * 10;
        }

        public override void print()
        {
            //Sent to Printer
        }
    }

    class DynamicPolymorphismTest2 : DynamicPolymorphism
    {
        public override void print()
        {
            //Print to Console
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
    ///  -  This principle is just an extension of the Open Close Principle.It means that we must make sure that new
    ///    derived classes are extending the base classes without changing their behavior.
    ///  - The Liskov Substitution Principle says that the object of a derived class should be able to replace an object
    ///    of the base class without bringing any errors in the system or modifying the behavior of the base class. In
    ///    short: if S is subset of T, an object of T could be replaced by object of S without impacting the program and
    ///     bringing any error in the system.
    /// </summary>
    class LSP
    {
        public void Play()
        {
            var shapes = new List<Shape>{
                new Rectangle{ Height=4, Width=6 },
                new Square{ Side=3 }
            };
            var areas = new List<int>();

            foreach (Shape shape in shapes)
            {
                areas.Add(shape.Area());
            }
        }


        /// <summary>
        /// - In this way we can create relationship between the sub class and the base class by adhering to the 
        ///   Liskov Substitution principle. 
        /// - Common ways to identify violations of LS principles are as follows:
        ///   1. Not implemented method in the sub class.
        ///   2. Sub class function overrides the base class method to give it new meaning.
        /// </summary>
        public abstract class Shape
        {
            public abstract int Area();
        }

        public class Rectangle : Shape
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public override int Area()
            {
                return Height * Width;
            }
        }

        public class Square : Shape
        {
            public int Side { get; set; }

            public override int Area()
            {
                return Side * Side;
            }
        }
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

    interface IAbstract
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class AbstractBase
    {
        public void Display()
        {

        }
    }

    public abstract class AbstractClass : AbstractBase, IAbstract
    {
        public static string staticValue = "Static";

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public AbstractClass() : this("Private")
        {
            Console.WriteLine("This is Public Constructor of Base class ");
        }

        private AbstractClass(string value)
        {
            Console.WriteLine("This is {0} Constructor of Base Class", value);
        }

        static AbstractClass()
        {
            Console.WriteLine("This is {0} Constructor of Base Class", staticValue);
        }

        public abstract void Display(string name);
    }

    internal class DerivedAbstract : AbstractClass
    {
        public DerivedAbstract()
        {
            Console.WriteLine("This is Public Constructor of Derived class ");
        }

        static DerivedAbstract()
        {
            Console.WriteLine("This is {0} Constructor of Derived Class", staticValue);
        }

        public override void Display(string name)
        {
            Console.WriteLine("This is Concret Display Method of Derived class ");
        }
    }

    internal abstract class AccountBase
    {
        private double InterestRate = 0.1;
        public AccountBase(int amount, int years)
        {
            CalculatedInterest = amount * years * InterestRate;
        }

        protected double CalculatedInterest { get; set; }

        public virtual void DisplayAmount()
        {
            Console.WriteLine("Total Interest: {0}", CalculatedInterest);
        }

        public abstract void ApprovedLoan();
    }

    internal class SavingAccount : AccountBase
    {
        private double amount;
        public SavingAccount(int amount, int years) : base(amount, years)
        {
            this.amount = amount;
        }

        public override void ApprovedLoan()
        {
            if ((CalculatedInterest + amount) > 0)
                Console.WriteLine("Load Approved!!");
            else
                Console.WriteLine("Load Declined!!");
        }
    }

    #endregion

    #region Constructor examples
    public class PrivateConstructorExample
    {
        private PrivateConstructorExample(string value) 
        {
            Console.WriteLine("This is {0} Constructor", value);
        }

        public static string staticValue = "";

        public PrivateConstructorExample()
            : this(staticValue)
        {
            Console.WriteLine("This is Public Constructor");
        }

        public PrivateConstructorExample(string s1, string s2): this(s1 + s2)
        {

        }
    }

    internal class StaticTest
    {
        public static string check;
        static StaticTest()
        {
            check = "Static Constructor";
        }
        public StaticTest() : this(check)
        {
        }

        private StaticTest(string m)
        {

        }
    }

    internal class ChildOfStaticTest : StaticTest
    {
        public ChildOfStaticTest() : base()
        {

        }
    }

    internal class Base
    {
        public Base()
        {
            Console.WriteLine("Base " + "Empty" + " Constructor");
        }

        public Base(int a)
        {
            Console.WriteLine("Base " + a + " Constructor");
        }

        public Base(string xy)
        {
            Console.WriteLine(xy);
        }

        public virtual void SayHello()
        {
            Console.WriteLine("Base : Hola Amigos!!");
        }
    }

    internal class Child : Base
    {
        public Child()
        {
            Console.WriteLine("Child  " + "Empty" + " Constructor");
        }

        public Child(int a)
        {
            Console.WriteLine("Child  " + a + " Constructor");
        }

        public Child(string xy)
        {
            Console.WriteLine("Child : {0}", xy);
        }

        public Child(string x, string y) : base(x + " " + y)
        {
            Console.WriteLine("Child : {0}", x + y);
        }

        public Child(int x, int y) : this(x + " " + y)
        {
            Console.WriteLine("Child : {0}", x + y);
        }

        public override void SayHello()
        {
            Console.WriteLine("Child: Ni Hao!!");

            base.SayHello();
        }
    }

    internal class GrandChild : Child
    {
        public GrandChild()
        {
            Console.WriteLine("Grand Child's EmptyConstructor");
        }

        public sealed override void SayHello()
        {
            base.SayHello(); //Call Base method

            Console.WriteLine("Grand Child: Che chio che che!!");
        }
    }

    internal class SuperGrandChild : GrandChild
    {
        public SuperGrandChild()
        {
            Console.WriteLine("Super Grand Child's EmptyConstructor");
        }

        //can not override SayHello() as it is sealed
        //public override void SayHello()
        //{
        //    base.SayHello();
        //}
    }
    #endregion

    #region Interface Examples

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

    public class ChildTest : TestClass
    {
        public override void Method3() { Console.WriteLine("Method3"); }

        public override void Method4() { Console.WriteLine("Method4"); }
    }
    #endregion
}
