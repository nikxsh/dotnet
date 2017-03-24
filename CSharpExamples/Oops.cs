using System;
using System.Collections.Generic;

namespace DotNetDemos.CSharpExamples
{
    public class Oops
    {
        public void OOPSTest()
        {
            #region Test1
            //ITestClass obj = new ChildTest();
            //obj.Method1();
            //obj.Method2();
            //obj.Method3();
            //obj.Method4();

            //var anonymousObject = new { Name = "Ram" };
            //Console.WriteLine("Hello " + anonymousObject.Name); 
            #endregion


            //It will call Base() first
            //Child objChild1 = new Child();
            //It will call Base() first
            //Child objChild2 = new Child(10);

            //It will call Base() first
            //Base objChild3 = new Child();
            //It will call Base() first
            //Base objChild4 = new Child(10);

            //It will call Base() only
            //Base objChild5 = new Base();
            //It will call Base(int a) Only
            //Base objChild6 = new Base(10);

            //Call Base(int a) first then Child(string x, string y)
            //Child objChild7 = new Child("hello", "world");

            //Call Base() then Child(string xy) then Child(int x, int y)
            //Child objChild8 = new Child(11,21);

            //Child objChild9 = new Child();
            //objChild9.SayHello();

            //GrandChild objChild10 = new SuperGrandChild();
            //objChild10.SayHello();

            //AccountBase objAccount = new SavingAccount(20000,2);
            //objAccount.DisplayAmount();
            //objAccount.ApprovedLoan();

            //var obj = new StaticTest();
            //var val = new ChildOfStaticTest();

            //PrivateConstructorExample.staticValue = "Private";
            //var exp = new PrivateConstructorExample();
            //DerivedAbstract.staticValue = "LOL";
            //var obj  = new DerivedAbstract();

            Beta objBeta = new Beta();
            objBeta.BaseMethod();
            objBeta.Display();

            Baap objBaapWithBeta = new Beta();
            objBaapWithBeta.BaseMethod();
            objBaapWithBeta.Display();
        }

    }

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
            Console.WriteLine("Display Baap");
        }
    }
    #endregion

    #region SOLID

    //1. “S”- SRP (Single responsibility principle) 
    //    So SRP says that a class should have only one responsibility and not multiple.
    //    So if we apply SRP we can move that logging activity to some other class who will only look after logging activities.
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
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.InnerException.ToString());
                }
            }
        }
    }

    //2. “O” - Open closed principle
    //    States that software application source codes should be open for extension but should be closed for modification
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

    //3. “L” - Liskov Substitution Principle
    //   Likov's Substitution Principle states that if a program module is using a Base class, then the reference to the Base class can be replaced with a Derived class 
    //   without affecting the functionality of the program module.
    //   This principle is just an extension of the Open Close Principle and it means that we must make sure that new derived classes are extending the base classes without changing their behavior.
    class LSP
    {
        public void CaluculateArea()
        {
            var shapes = new List<Shape>
            {
                new Rectangle { Height =12, Width =4 },
                new Square { Side =8 }
            };

            shapes.ForEach(x => Console.WriteLine(x.Area()));
        }

        abstract class Shape
        {
            public abstract int Area();
        }

        class Rectangle : Shape
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public override int Area()
            {
                return Height * Width;
            }
        }

        class Square : Shape
        {
            public int Side { get; set; }
            public override int Area()
            {
                return Side * Side;
            }
        }
    }

    //4. “I” - ISP (Interface Segregation principle)
    //   ISP states that no clients should be forced to implement methods which it does not use and the contracts should be broken down to thin ones.
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
                throw new NotImplementedException();
            }

            public double getDiscount(double TotalSales)
            {
                throw new NotImplementedException();
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

    //5. “D” - DIP (Dependency Inversion Principle)
    //   DIP states that the higher level modules should be coupled with the lower level modules with complete abstraction
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
