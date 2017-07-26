using System;
using System.Collections.Generic;

namespace DotNetDemos.CSharpExamples.Puzzles
{
    public class Tricky
    {
        public void Play()
        {
            //Problem1();
            //Problem2();
            //Problem3();
            //Problem4();
            Problem5();
        }

        public string Problem1()
        {
            try
            {
                return "a";
            }
            catch (Exception)
            {
                return "b";
            }
            finally
            {
                //Compile time error: controll can not leave the body of Finally clause
                //return "c";
            }
        }

        private void Problem2()
        {
            //Will work
            Base obj1 = new Derived();
            obj1.Name(); // Prints "Ravan" 
            obj1.Discount(); //50%

            //Compile time error - can not convert type A to Type B
            //Derived obj2 = new Base(); //Compile time error - can not convert type A to Type B 

            //will work
            Derived obj3 = new Derived();
            obj3.Name(); // Prints "Ravan" 
            obj3.Salary(); // Prints "1000 Rs" 
            obj3.Discount(); //50%

            //will work
            Base obj4 = new Base(); //Will work
            obj4.Name(); // Prints "Ravan" 
            obj4.Discount(); //10%

        }

        private void Problem3()
        {
            I1 obj1 = new ABC();
            obj1.Display("Hello");

            I2 obj2 = new ABC();
            obj1.Display("Hola");

            I1 obj3 = new XYZ();
            obj3.Display("Hello");

            I2 obj4 = new XYZ();
            obj4.Display("Hola");

            var obj5 = new XYZ();
            //obj5.Display("Hola"); Won't work
        }

        private void Problem4()
        {
            var obj = new Test();
            obj.Id = 2000;
            //SimplePass(obj); //Prints 1000
            //PassAndInstantiation(obj); //Prints 2000
            PassByRef(ref obj); //Prints 1000
            Console.WriteLine(obj.Id);
        }

        //Pass by refrence
        private void SimplePass(Test obj)
        {
            obj.Id = 1000;
        }

        //When you assign new object it wont change callee's reference
        private void PassAndInstantiation(Test obj)
        {
            obj = new Test();
            obj.Id = 1000;
        }

        //When you use ref then it will returns new refernce and modification to the callee
        private void PassByRef(ref Test obj)
        {
            obj = new Test();
            obj.Id = 1000;
        }

        void Problem5()
        {
            Rectangle rect = new Rectangle();
            rect.Height = 12;
            rect.Width = 10;
            var rectArea = CalculateArea(rect); // Prints 120

            Rectangle sqr = new Square();
            sqr.Height = 4;
            sqr.Width = 10;
            var sqrArea = CalculateArea(sqr); // Will print 40 instead of 16 - lsp voilation
        }

        public int CalculateArea(Rectangle r) => r.Height * r.Width;

        public int CalculateArea(Square s) => s.Height * s.Width;

        void Problem6()
        {
            //Design problem

            //let say you have tank that shoot and drive and plane that fly and shoot, after that you have to add new class Truck that can only drive. implement design

            Plane plane = new Plane();
            plane.Fly();
            plane.Shoot(new Tank());

            Tank tank = new Tank();
            tank.Drive();
            tank.Shoot(new Plane());

            Truck truck = new Truck();
            //truck.Shoot(new Plane()); //Truck cant shoot - this is design problem
            truck.Drive();           
        }
    }

    class Base
    {
        public void Name()
        {
            Console.WriteLine("Ravan");
        }

        public virtual void Discount()
        {
            Console.WriteLine("10%");
        }
    }

    class Derived : Base
    {
        public void Salary()
        {
            Console.WriteLine("1000 Rs");
        }

        public override void Discount()
        {
            Console.WriteLine("50%");
        }
    }

    interface I1
    {
        void Display(string a);
    }

    interface I2
    {
        void Display(string b);
    }

    class ABC : I1, I2
    {
        public void Display(string x)
        {
            Console.WriteLine(x);
        }
    }

    class XYZ : I1, I2
    {
        void I1.Display(string a)
        {
            //Print A
            Console.WriteLine(a);
        }

        void I2.Display(string b)
        {
            Console.WriteLine(b);
        }
    }

    class Test
    {
        public int Id { get; set; }
    }

    public class Rectangle
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }
    }

    public class Square : Rectangle
    {
        private int _height;
        private int _width;
        public override int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                _width = value;
            }
        }
        public override int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                _height = value;
            }
        }
    }

    public class Coordinates
    {
        public int Longitude { get; set; }
        public int Lattitude { get; set; }
    }

    public abstract class Machine
    {
        public Coordinates position { get; set; }

        public void Shoot(Machine target)
        {
            //Shoot the target
        }
    }
    
    public class Plane : Machine
    {
        public void Fly()
        {
            
        }
    }
    
    interface IDrive
    {
        void Drive();
    }

    public class Tank : Machine, IDrive
    {
        public void Drive()
        {

        }
    }

    public class Truck : IDrive
    {
        public void Drive()
        {

        }
    }
}
