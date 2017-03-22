using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DotNetDemos.CSharpExamples
{
    public class ParallelProgramming
    {
        public void DoAction()
        {

            //DoLoopAction();
            //DoTaskLoopAction();
            //TaskWorkLoad();
            TreadPoolExample();
        }        

        private void SynchronisationOfTasks()
        {
            var obj = new ParallelLooping();
            Task task1 = new Task((x) => obj.MethodB(x), "Parallel => Task 2");
            task1.Start();
        }

        private void TaskWorkLoad()
        {
            var obj = new ParallelLooping();

            #region Wait for Task
            //Task task1 = new Task(new Action<object>(obj.MethodA), "Sequential => Task 1");
            //task1.Start();
            //Console.WriteLine("Waiting for Task1 to Complete");
            //task1.Wait();
            //Console.WriteLine("Task1 Completed");


            //task1 = new Task((x) => obj.MethodB(x), "Parallel => Task 2");
            //task1.Start();
            //Console.WriteLine("Waiting 2 sec for Task2 to Complete");
            //Thread.Sleep(2000);
            //Console.WriteLine("Wait ended - task2 completed.");
            //Console.WriteLine("Main method complete. Press any key to finish."); 
            #endregion

            #region Wait for All Task
            //Task task1 = new Task(new Action<object>(obj.MethodA), "Sequential => Task 1");
            //Task task2 = new Task((x) => obj.MethodB(x), "Parallel => Task 2");
            //task1.Start();
            //task2.Start();
            //Console.WriteLine("Waiting All Task to Complete");
            //Task.WaitAll(task1, task2);
            //Console.WriteLine("Tasks completed.");
            //Console.WriteLine("Main method complete. Press any key to finish."); 
            #endregion

            #region Wait any Task to Complete
            Task task1 = new Task(new Action<object>(obj.MethodA), "Sequential => Task 1");
            Task task2 = new Task((x) => obj.MethodB(x), "Parallel => Task 2");
            task1.Start();
            task2.Start();
            Console.WriteLine("Waiting Any of Task to Complete");
            Task.WaitAny(task1, task2);
            Console.WriteLine("Tasks completed.");
            Console.WriteLine("Main method complete. Press any key to finish.");
            #endregion

        }

        private void DoTaskLoopAction()
        {
            var obj = new ParallelLooping();
            //Action Delegate
            Task task1 = new Task(new Action<object>(obj.MethodA), "Sequential => Task 1");
            //Lambda Expression
            Task task2 = new Task((x) => obj.MethodB(x), "Parallel => Task 2");
            //Anonymous
            //Task task3 = new Task(delegate { obj.MethodA(); });

            task1.Start();
            task2.Start();
            //task3.Start();
        }

        private void DoLoopAction()
        {
            var obj = new ParallelLooping();
            obj.MethodA("Sequential");
            obj.MethodB("Parallel");
        }

        private void TreadPoolExample()
        {
            var obj = new ParallelLooping();
            Console.WriteLine("With TPL");
            Task.Factory.StartNew(obj.GotoPool, "TPL");
            Thread.Sleep(2000);
            Console.WriteLine("With Out TPL");
            ThreadPool.QueueUserWorkItem(obj.GotoPool);
            ThreadPool.QueueUserWorkItem(obj.GotoPool, "Nik");
        }
    }
  
    public class ParallelLooping
    {
        public void MethodA(object taskName)
        {
            var watch = Stopwatch.StartNew();
            for (int i = 2; i < 20; i++)
            {
                Console.WriteLine("{0} Method  => root {1} : {2} ", taskName, i, SumOfRootN(i));
            }
            Console.WriteLine(watch.Elapsed);
        }

        public void MethodB(object taskName)
        {
            var watch = Stopwatch.StartNew();
            Parallel.For(2, 20, (i) =>
             {
                 Console.WriteLine("{0} Method  => root {1} : {2} ", taskName, i, SumOfRootN(i));
             });
            Console.WriteLine(watch.Elapsed);
        }

        private double SumOfRootN(int root)
        {
            double result = 0;
            for (int i = 0; i < 10000000; i++)
                result += Math.Exp(Math.Log(i) / root);
            return result;
        }

        public void GotoPool(object message)
        {
            Console.WriteLine("Hello from the thread pool! : {0}",message);
        }
    }
}
