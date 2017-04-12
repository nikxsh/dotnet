using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            //TaskBasics();
            //TaskFactory();
            AsyncAndAwait();
        }
            
        public void TaskBasics()
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

        public void ToSquare(int value)
        {
            Thread.Sleep(5000);
            taskCount--;
            Console.WriteLine("");
            Console.WriteLine(string.Format("Thread {3} of value {0} | Result: {1} | Task Remaining {2} : ", value, value * value, taskCount, Thread.CurrentThread.ManagedThreadId));
        }


        public void TaskFactory()
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

            //You can also use the TaskFactory.StartNew method to create and start a task in one operation. 
            //Use this method when creation and scheduling do not have to be separated and you require additional task creation options or 
            //the use of a specific scheduler, or when you need to pass additional state into the task through its AsyncState property, 
            //as shown in the following example.
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

            //Call to the Task.Wait method to ensure that the task completes execution before the console mode application or current application ends.
            task1.Wait();


            var Parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task beginning.");

                var child = Task.Factory.StartNew((x) =>
                {
                    Thread.SpinWait(5000000);
                    Console.WriteLine("Detached task #{0} completed.", x);
                },
                1,
                TaskCreationOptions.AttachedToParent);

            });

            Parent.Wait();
            Console.WriteLine("Parent task completed.");
            // The example displays the following output:
            //    Outer task beginning.
            //    Detached task #1 completed.
            //    Parent task completed.

        }

        public async void AsyncAndAwait()
        {
            for (int i = 0; i < 4; i++)
            {
                //int result = await AccessTheWebAsync();
                Task<int> result = AccessTheWebAsync();
                //Do your work
                Console.WriteLine(await result);
            }
        }

        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.  
            for (int i = 1; i < 10; i++)
            {
                //Do Something
            }

            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            string urlContents = await getStringTask;

            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return urlContents.Length;
        }
    }
}
