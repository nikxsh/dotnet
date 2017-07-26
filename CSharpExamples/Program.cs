using System;
using System.Collections.Generic;

namespace DotNetDemos.CSharpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Design Pattern Example
            //Console.WriteLine("---- Design Patterns ---- ");
            //Console.WriteLine(" 1. Facade ");
            //Console.WriteLine(" 2. Observer ");
            //Console.WriteLine(" 3. Singleton ");
            //Console.WriteLine(" 4. Adapter");
            //Console.WriteLine(" 5. Abstract Factory");
            //Console.WriteLine(" 6. Builder");
            //Console.WriteLine(" 7. Factory");
            //Console.WriteLine(" 8. Command");
            //Console.WriteLine(" 9. Composite");
            //Console.Write("Enter your Choice: ");
            //var choice = Console.ReadLine();
            //var example = new DesignPatternExamples();
            //example.DoAction(int.Parse(choice));
            #endregion

            #region Action And Func Method
            //ActionAndFucntion actionAndFunction = new ActionAndFucntion();
            //actionAndFunction.Call("Nikhilesh");
            #endregion

            #region Expression
            //var expressions =new ExpressionExamples();
            //expressions.Execute();
            #endregion

            #region Mail Gateway
            //SendAMail objMail = new SendAMail("nikhilesh.shinde@hotmail.com", "Gl@d!@t0r", "smtp-mail.outlook.com");
            //objMail.SendEMail("shinde.nikhilesh90@gmail.com", "Password Reset Link", "Reset your Password <a href='http://localhost.safetychain.com/ForgotPassword?1234-5678-91234'>Here.</a>");
            #endregion

            #region SMS Gateway
            //SendAnSMS sendAnSMS = new SendAnSMS();
            //sendAnSMS.SendSMS();
            #endregion

            #region OOPS Test
            //var test = new OOPBasic.OOPBasics();
            //test.Play();
            #endregion

            #region Linq Test
            //var test = new LinqExamples();
            //test.Examples();
            #endregion

            #region MultiThreading
            //var obj = new MultiThreading();
            //obj.DoAction();
            #endregion

            #region Parallel Programming Test
            //var obj = new Parallelism();
            //obj.DataParallelism();
            //obj.TaskParallelism();
            #endregion

            #region Sorting Algo
            //var obj = new SortExamples();
            //obj.DoAction(); 
            #endregion

            #region Tree Structure Examples 
            //Console.WriteLine("-------- Data Structure ---------");
            //Console.WriteLine(" 1. Binary Tree ");
            //Console.WriteLine(" 2. Heaps ");
            //Console.WriteLine(" 3. Sorting ");
            //Console.WriteLine("---------------------------------");
            //var choice = Console.ReadLine();
            //var example = new DataStructureExamples();
            //example.DoAction(int.Parse(choice));
            #endregion

            #region Inbult Interfaces Examples 
            //var obj1 = new InterfacesExamples();
            //obj1.DoAction();
            #endregion

            #region Garbage Collection
            //var test = new GarbageCollection();
            //test.WithIDisposable();
            #endregion

            #region Performance Test
            //var test = new Performance();
            //test.Test1();

            #endregion

            var obj = new Puzzles.Tricky();
            obj.Play();

            //Console.Write(typeof(string).Assembly.ImageRuntimeVersion);
            Console.ReadKey();
        }

    }


}
