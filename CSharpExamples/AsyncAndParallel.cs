using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetDemos.CSharpExamples
{
    //1. Task: A unit of work; an object denoting ongoing operations or work.
    public class AsyncAndParallel
    {
        public static int taskCount = 0;
        public void Play()
        {
            Console.WriteLine("Enter value to Squre(Press x to quit) :  ");
            var choice = string.Empty;
            do
            {
                choice = Console.ReadLine();

                taskCount++;
                Task t = new Task(() => ToSquare(int.Parse(choice)));
                t.Start();

            }
            while (!choice.Equals("x", StringComparison.InvariantCultureIgnoreCase));
        }

        public void BasicPlay()
        {
            //prepare the task
            var task1 = new Task<string>(() =>
            {
                Thread.Sleep(3000);
                var message = "Task 1 - I'm First!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId;
                return message;
            });

            //Tells the .net that task *can* be started
            //program now forks and 2 code streams are executing concurrently - Current method play() as main thread and T as worker thread. Both thread share cores in the processor
            //task1.Start();

            //Creates and starts a task.
            Task.Factory.StartNew(() => { Console.WriteLine("Task 2 - I'm Second!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId); });

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!

            // Execute the continuation when the antecedent (Task1) finishes
            task1.ContinueWith((antecedent) =>
            {
                Console.WriteLine(antecedent.Result);
                Console.WriteLine("Task 3 - I'm Third!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId);
            });

            task1.Start();

            Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId);

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!
            //Task 3 : I'm Third!!

            //You can also use the TaskFactory.StartNew method to create and start a task in one operation. 
            //Use this method when creation and scheduling do not have to be separated and you require additional task creation options or 
            //the use of a specific scheduler, or when you need to pass additional state into the task through its AsyncState property, 
            //as shown in the following example.
        }

        public void ToSquare(int value)
        {
            Thread.Sleep(5000);
            taskCount--;
            Console.WriteLine("");
            Console.WriteLine(string.Format("Thread {3} of value {0} | Result: {1} | Task Remaining {2} : ", value, value * value, taskCount, Thread.CurrentThread.ManagedThreadId));
        }
    }
}
