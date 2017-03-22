using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace DotNetDemos.CSharpExamples
{
    public class MultiThreading
    {
        public void DoAction()
        {
            var obj = new MultiThreadingAndSynchronization();
            obj.DoSynchronization();

            //var obj = new SignalingwithEvent();
            //obj.DoSignalingwithEvent();

            //var obj = new AsynchronousEvents();
            //obj.DoAsynchronousEvents();
        }

    }

    public class MultiThreadingAndSynchronization
    {

        private static SemaphoreSlim _sem = new SemaphoreSlim(3);
        private static object _locker = new object();
        private bool isDone = false;
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        private static List<int> _items = new List<int>();
        private static Random _rand = new Random();

        public void SimpleExample()
        {
            Thread t1 = new Thread(() => SimpleLoop("Thread 1"));
            Thread t2 = new Thread(() => SimpleLoop("Thread 2"));
            t1.Start();
            t1.Join();
            Console.WriteLine("Thread 1 Complete execution");
            t2.Start();

            t1.Priority = ThreadPriority.Highest;

            for (int i = 0; i < 100; i++)
                Console.Write(" Main ");
        }

        /// <summary>
        /// Ensures just one thread can access a resource, or section of code at a time
        /// </summary>
        public void DoSynchronization()
        {
            //Thread t1 = new Thread(() => SimpleLockExample());
            //Thread t2 = new Thread(() => SimpleLockExample());

            //Thread t1 = new Thread(() => SimpleMonitorExample());
            //Thread t2 = new Thread(() => SimpleMonitorExample());

            //Thread t1 = new Thread(() => SimpleMutexExample());
            //Thread t2 = new Thread(() => SimpleMutexExample());

            //Thread t1 = new Thread(() => SimpleSemaphoreExample());
            //Thread t2 = new Thread(() => SimpleSemaphoreExample());
            //Thread t3 = new Thread(() => SimpleSemaphoreExample());
            //Thread t4 = new Thread(() => SimpleSemaphoreExample());
            //Thread t5 = new Thread(() => SimpleSemaphoreExample());

            //t1.Name = "Thread 1";
            //t2.Name = "Thread 2";
            //t3.Name = "Thread 3";
            //t4.Name = "Thread 4";
            //t5.Name = "Thread 5";
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t4.Start();
            //t5.Start();


            //Reader/Writer Locks
            Thread t6 = new Thread(() => Write(10));
            Thread t7 = new Thread(() => Write(20));

            Thread t1 = new Thread(() => Read());
            Thread t2 = new Thread(() => Read());
            Thread t3 = new Thread(() => Read());
            Thread t4 = new Thread(() => Read());
            Thread t5 = new Thread(() => Read());

            Thread t8 = new Thread(() => Write(30));
            Thread t9 = new Thread(() => Write(40));

            t1.Name = "Thread 1";
            t2.Name = "Thread 2";
            t3.Name = "Thread 3";
            t4.Name = "Thread 4";
            t5.Name = "Thread 5";
            t6.Name = "Thread 6";
            t7.Name = "Thread 7";
            t8.Name = "Thread 8";
            t9.Name = "Thread 9";

            t6.Start();
            t7.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t8.Start();
            t9.Start();
        }


        /// <summary>
        /// Ensures just one thread can access a resource, or section of code at a time
        /// </summary>
        private void SimpleLockExample()
        {
            lock (_locker)
            {
                if (!isDone)
                {
                    SimpleLoop(Thread.CurrentThread.Name);
                    isDone = true;
                }
            }
        }

        /// <summary>
        /// Ensures just one thread can access a resource, or section of code at a time
        /// </summary>
        private void SimpleMonitorExample()
        {
            bool lockToken = false;
            try
            {
                Monitor.Enter(_locker, ref lockToken);
                SimpleLoop(Thread.CurrentThread.Name);
            }
            finally
            {
                if (lockToken)
                    Monitor.Exit(_locker);
            }
        }

        /// <summary>
        /// Ensures just one thread can access a resource, or section of code at a time
        /// A Mutex is like a C# lock, but it can work across multiple processes. In other words, Mutex can be computer-wide as well as application-wide.
        /// A common use for a cross-process Mutex is to ensure that only one instance of a program can run at a time. 
        /// </summary>
        private void SimpleMutexExample()
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).
            using (var mutex = new Mutex(false, "Checking Mutex"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.
                mutex.WaitOne(TimeSpan.FromSeconds(5));
                Console.WriteLine("--- {0} has entered Critical section ---", Thread.CurrentThread.Name);
                SimpleLoop(Thread.CurrentThread.Name);
                mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// A semaphore is like a nightclub: it has a certain capacity, enforced by a bouncer. 
        /// Once it’s full, no more people can enter, and a queue builds up outside. 
        /// Then, for each person that leaves, one person enters from the head of the queue. 
        /// The constructor requires a minimum of two arguments: the number of places currently available 
        /// in the nightclub and the club’s total capacity.
        /// </summary>
        private void SimpleSemaphoreExample()
        {
            Console.WriteLine("{0} wants to enter", Thread.CurrentThread.Name);
            _sem.Wait();
            Console.WriteLine("{0} has entered Critical section.", Thread.CurrentThread.Name);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            _sem.Release();
            Console.WriteLine("{0} left", Thread.CurrentThread.Name);
        }


        private void Read()
        {
            if (_items.Count > 0)
            {
                _rw.EnterReadLock();
                Console.WriteLine("{0} is Reading...", Thread.CurrentThread.Name);
                foreach (var item in _items)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("{0} Reading Completed!!!", Thread.CurrentThread.Name);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                _rw.ExitReadLock();
            }
        }

        private void Write(int newNum)
        {

            _rw.EnterWriteLock();
            _items.Add(newNum);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine("{0} has inserted {1}", Thread.CurrentThread.Name, newNum);
            _rw.ExitWriteLock();
        }

        private void SimpleLoop(object threadName)
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine("Loop{0}: {1}", i + 1, (string)threadName);
        }
    }

    public class SignalingwithEvent
    {
        static EventWaitHandle _ready = new AutoResetEvent(false);
        static EventWaitHandle _go = new AutoResetEvent(false);
        static readonly object _locker = new object();
        static string _message = string.Empty;

        public void DoSignalingwithEvent()
        {
            TwoWaySignaling();
        }

        private void TwoWaySignaling()
        {
            Thread t1 = new Thread(() => Work());
            t1.Name = "Thread 1";
            t1.Start();

            _ready.WaitOne(); // First wait until worker is ready
            lock (_locker) _message = "Step 1";
            _go.Set(); // Tell worker to go

            _ready.WaitOne(); // Wait until worker is ready
            lock (_locker) _message = "Step 2";  // Give the worker another message
            _go.Set(); // Tell worker to go

            _ready.WaitOne(); // Wait until worker is ready
            lock (_locker) _message = string.Empty;  // Signal the worker to exit
            _go.Set(); // Tell worker to go

        }

        private static void Work()
        {
            while (true)
            {
                //Sets the state of the event to signaled, allowing one or more waiting threads
                _ready.Set();
                //Blocks the current thread until the current thread receives a signal.
                _go.WaitOne();

                lock (_locker)
                {
                    if (string.IsNullOrEmpty(_message)) return;
                    Console.WriteLine("{0} : {1}", Thread.CurrentThread.Name, _message);
                }
            }
        }
    }

    public class AsynchronousEvents
    {
        static BackgroundWorker _bw;
        public void DoAsynchronousEvents()
        {
            BackgroundWorkerExample();
        }

        private void BackgroundWorkerExample()
        {
            _bw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

            _bw.DoWork += DoBgWork;
            _bw.RunWorkerCompleted += BgRunWorkerCompleted;
            _bw.ProgressChanged += BgProgressChanged;

            _bw.RunWorkerAsync("Hello to Worker");

            Console.WriteLine("Press Enter in the next 5 seconds to cancel");
            Console.ReadLine();
            if (_bw.IsBusy) _bw.CancelAsync();
        }

        private static void DoBgWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i = i + 10)
            {
                if (_bw.CancellationPending) { e.Cancel = true; return; }
                _bw.ReportProgress(i);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            e.Result = "Completed!!!";
        }

        private static void BgRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                Console.WriteLine("You cancelled!");
            else if (e.Error != null)
                Console.WriteLine("Worker exception: " + e.Error.ToString());
            else
                Console.WriteLine(e.Result);
        }

        private static void BgProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Reached {0} %", e.ProgressPercentage);
        }
    }
}
