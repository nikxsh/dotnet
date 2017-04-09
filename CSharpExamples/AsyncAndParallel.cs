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
        public void BasicPlay()
        {
            //prepare the task
            var task1 = new Task(() => { Thread.Sleep(3000);  Console.WriteLine("Task 1 : I'm First!!");  });

            //Tells the .net that task *can* be started
            //program now forks and 2 code streams are executing concurrently - Current method play() as main thread and T as worker thread. Both thread share cores in the processor
            //task1.Start();

            var task2 = new Task(() => { Console.WriteLine("Task 2 : I'm Second!!"); });
            task2.Start();

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!

            //Task 3 will execute after task 1 complete
            task1.ContinueWith((antecedent) => { Console.WriteLine("Task 3 : I'm Third!!"); });
            task1.Start();

            //Result will be
            //Task 2 : I'm Second!!
            //Task 1 : I'm First!!
            //Task 3 : I'm Third!!

        }
    }
}
