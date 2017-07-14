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
            Problem4();
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

            //Compile time error - can not convert type A to Type B
            //Derived obj2 = new Base(); //Compile time error - can not convert type A to Type B 

            //will work
            Derived obj3 = new Derived();
            obj3.Name(); // Prints "Ravan" 
            obj3.Salary(); // Prints "1000 Rs" 

            //will work
            Base obj4 = new Base(); //Will work
            obj4.Name(); // Prints "Ravan" 
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
    }

    class Base
    {
        public void Name()
        {
            Console.WriteLine("Ravan");
        }
    }

    class Derived : Base
    {
        public void Salary()
        {
            Console.WriteLine("1000 Rs");
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
}
