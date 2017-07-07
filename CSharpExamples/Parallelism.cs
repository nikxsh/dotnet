using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetDemos.CSharpExamples
{
    /// <summary>
    /// There are two strategies for partitioning work among threads: data parallelism and task parallelism.
    /// 
    /// When a set of tasks must be performed on many data values, we can parallelize by having each thread perform the(same) set of tasks on a subset of values.
    /// This is called data parallelism because we are partitioning the data between threads.
    /// 
    /// In contrast, with task parallelism we partition the tasks; in other words, we have each thread perform a different task.
    /// </summary>
    public class Parallelism
    {
        public void TaskParallelism()
        {
            var obj = new TaskParallelism();
            //obj.TaskBasics();
            //obj.TaskFactory();
            //obj.AsyncAndAwait();
            obj.Continuations();
        }

        public void DataParallelism()
        {
            var obj = new DataParallelism();
            obj.Plinq();
        }
    }

    /// <summary>
    /// Essentially, a task is a lightweight object for managing a parallelizable unit of work.
    /// </summary>
    class TaskParallelism
    {
        public static int taskCount = 0;

        /// <summary>
        /// Task: A unit of work; an object denoting ongoing operations or work.
        /// </summary>
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
            //program now forks and 2 code streams are executing concurrently - Current method TaskFactory() as main thread and T as worker thread. Both thread share cores in the processor
            task1.Start();

            //You can also use the TaskFactory.StartNew method to create and start a task in one operation. 
            //Use this method when creation and scheduling do not have to be separated and you require additional task creation options or 
            //the use of a specific scheduler, or when you need to pass additional state into the task through its AsyncState property, 
            //as shown in the following example.
            //Creates and starts a task.
            Task.Factory.StartNew(() => { Console.WriteLine("Task 2 - I'm Second!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId); });

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!

            Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId);

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!
            //Main Thread

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

            //Waits for the Parent Task to complete execution
            Parent.Wait();

            Console.WriteLine("Parent task completed.");
            // The example displays the following output:
            //    Outer task beginning.
            //    Detached task #1 completed.
            //    Parent task completed.

        }

        public void Continuations()
        {
            var task1 = Task.Factory.StartNew<string>(() =>
            {
                var message = "Task 1 - I'm First!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId;
                return message;
            });

            // Execute the continuation when the antecedent (Task1) finishes
            var task2 = task1.ContinueWith((antecedent) =>
            {
                Console.WriteLine(antecedent.Result);
                Console.WriteLine("Task 2 - I'm Second!! | Worker Thread " + Thread.CurrentThread.ManagedThreadId);
                return 22;
            });

            var task3 = Task.Factory.StartNew(() => { Console.WriteLine("Task 3| Worker Thread Id: " + Thread.CurrentThread.ManagedThreadId); return 22; });
            var task4 = Task.Factory.StartNew(() => { Console.WriteLine("Task 4| Worker Thread Id: " + Thread.CurrentThread.ManagedThreadId); return 22; });
            var task5 = Task.Factory.StartNew(() => { Console.WriteLine("Task 5| Worker Thread Id: " + Thread.CurrentThread.ManagedThreadId); return 22; });

            Task all = Task.Factory.ContinueWhenAll(new[] { task2, task3, task4, task5 }, tasks => Console.WriteLine("All task returned : {0}", tasks.Sum(t => t.Result)));

            //Call to the Task.Wait method to ensure that the task completes execution before the console mode application or current application ends.
            task1.Wait();

            //If an antecedent throws and the continuation fails to query the antecedent’s Exception property 
            //(and the antecedent isn’t otherwise waited upon), the exception is considered unhandled and the application dies
            //
            //A safe pattern is to rethrow antecedent exceptions. As long as the continuation is Waited upon, 
            //the exception will be propagated and rethrown to the Waiter
            bool error = false;
            var normalTask = Task.Factory.StartNew(() => { if (error) throw null; else return "Success"; });

            var catchTask = normalTask.ContinueWith((antecedent) => Console.WriteLine(antecedent.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);

            var OkayTask = normalTask.ContinueWith((antecedent) => Console.WriteLine(antecedent.Result), TaskContinuationOptions.NotOnFaulted);
        }

        public void AsyncAndAwait()
        {
            for (int i = 0; i < 4; i++)
            {
                //int result = await AccessTheWebAsync();
                Task<int> result = AccessTheWebAsync();
                //Do your work
                Console.WriteLine(result);
            }
        }

        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        //
        // - The Async modifier indicates that the method or lambda expression that it modifies is asynchronous.
        //  Such methods are referred to as async methods.
        // - An async method provides a convenient way to do potentially long-running work without blocking the 
        //  caller's thread. The caller of an async method can resume its work without waiting for the async method to finish.
        // - The marked async method can use Await or await to designate suspension points.The await operator tells the compiler that 
        //   the async method can't continue past that point until the awaited asynchronous process is complete. In the meantime, control 
        //   returns to the caller of the async method.
        // - The suspension of an async method at an await expression doesn't constitute an exit from the method, and finally blocks don’t run.
        // - The marked async method can itself be awaited by methods that call it.
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

    class DataParallelism
    {
        /// <summary>
        /// Calculate prime numbers using a simple (unoptimized) algorithm
        /// 
        /// PLINQ runs your query on parallel threads, you must be careful not to perform thread-unsafe operations. 
        /// </summary>
        public void Plinq()
        {

            Console.WriteLine("*********** Calculate Prime Numbers of range (3 t0 100000) *********");
            var numbers = Enumerable.Range(3, 100000);
            var _watch = new Stopwatch();

            _watch.Start();
            var sequentialQuery = from n in numbers.AsParallel()
                                  where Enumerable.Range(2, (int)Math.Sqrt(n)).All(x => n % x > 0)
                                  select n;
            _watch.Stop();
            Console.WriteLine("Total Prime Numbers: {0}", sequentialQuery.Count());
            Console.WriteLine("Sequential Execution took : " + _watch.Elapsed.TotalMilliseconds + " ms");

            _watch.Reset();
            _watch.Start();
            var parallerQuery = from n in numbers.AsParallel()
                                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(x => n % x > 0)
                                select n;
            _watch.Stop();
            Console.WriteLine("Total Prime Numbers: {0}", parallerQuery.Count());
            Console.WriteLine("Parallel Execution took : " + _watch.Elapsed.TotalMilliseconds + " ms");


            //Parallel.Invoke executes an array of Action delegates in parallel, and then waits for them to complete
            Parallel.Invoke(() => { Thread.Sleep(2000); Console.WriteLine("Invoke 1"); },
                             () => { Thread.Sleep(1000); Console.WriteLine("Invoke 2"); });

            //Parallel.For and Parallel.ForEach perform the equivalent of a C# for and foreach loop, 
            //but with each iteration executing in parallel instead of sequentially.
            _watch.Reset();
            _watch.Start();
            for (int i = 0; i < 100; i++) { Console.Write(i + " "); }
            _watch.Stop();
            Console.WriteLine("");
            Console.WriteLine("Sequential for loop took : {0}", _watch.Elapsed.TotalMilliseconds + " ms");

            Console.WriteLine("");

            _watch.Reset();
            _watch.Start();
            Parallel.For(0, 100, i => Console.Write(i + " "));
            _watch.Stop();
            Console.WriteLine("");
            Console.WriteLine("Parallel for loop took : {0}", _watch.Elapsed.TotalMilliseconds + " ms");

        }
    }
}
