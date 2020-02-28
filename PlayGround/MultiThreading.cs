using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace PlayGround
{
	public class MultiThreading
	{
		public void Play()
		{
			//var obj = new MultiThreadingAndSynchronization();
			//obj.DoSynchronization();

			//var obj = new SignalingwithEvent();
			//obj.DoSignalingwithEvent();

			//var obj = new AsynchronousEvents();
			//obj.DoAsynchronousEvents();

			var obj = new Problems();
			obj.Problem1();
		}

		class Problems
		{
			private int msum = 0;
			public void Problem1()
			{
				Thread t1 = new Thread(() => Loop());
				Thread t2 = new Thread(() => Loop());
				t1.Start();
				t2.Start();

				Console.WriteLine("Sum : {0}", msum);
			}

			public void Loop(int length = 5)
			{
				for (int i = 0; i < length; i++)
				{
					msum++;
					Console.WriteLine("{0}", i);
				}
			}

			public void Problem2()
			{
				Thread t1 = new Thread(() => Loop());
				Thread t2 = new Thread(() => Loop());
				t1.Start();
				t2.Start();
			}
		}

		class MultiThreadingAndSynchronization
		{

			private static Semaphore _sem = new Semaphore(3, 3);
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

				for (int i = 0; i < 100; i++)	Console.Write(" Main ");
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

				Thread t1 = new Thread(SimpleSemaphoreExample);
				Thread t2 = new Thread(SimpleSemaphoreExample);
				Thread t3 = new Thread(SimpleSemaphoreExample);
				Thread t4 = new Thread(SimpleSemaphoreExample);
				Thread t5 = new Thread(SimpleSemaphoreExample);
				Thread t6 = new Thread(SimpleSemaphoreExample);
				Thread t7 = new Thread(SimpleSemaphoreExample);
				Thread t8 = new Thread(SimpleSemaphoreExample);

				t1.Name = "Thread 1";
				t2.Name = "Thread 2";
				t3.Name = "Thread 3";
				t4.Name = "Thread 4";
				t5.Name = "Thread 5";
				t6.Name = "Thread 6";
				t7.Name = "Thread 7";
				t8.Name = "Thread 8";
				t1.Start();
				t2.Start();
				t3.Start();
				t4.Start();
				t5.Start();
				t6.Start();
				t7.Start();
				t8.Start();


				//Reader/Writer Locks
				//Thread t6 = new Thread(() => Write(10));
				//Thread t7 = new Thread(() => Write(20));

				//Thread t1 = new Thread(() => Read());
				//Thread t2 = new Thread(() => Read());
				//Thread t3 = new Thread(() => Read());
				//Thread t4 = new Thread(() => Read());
				//Thread t5 = new Thread(() => Read());

				//Thread t8 = new Thread(() => Write(30));
				//Thread t9 = new Thread(() => Write(40));

				//t1.Name = "Thread 1";
				//t2.Name = "Thread 2";
				//t3.Name = "Thread 3";
				//t4.Name = "Thread 4";
				//t5.Name = "Thread 5";
				//t6.Name = "Thread 6";
				//t7.Name = "Thread 7";
				//t8.Name = "Thread 8";
				//t9.Name = "Thread 9";

				//t6.Start();
				//t7.Start();
				//t1.Start();
				//t2.Start();
				//t3.Start();
				//t4.Start();
				//t5.Start();
				//t8.Start();
				//t9.Start();
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
			/// Like the lock keyword, monitors prevent blocks of code from simultaneous execution by multiple threads. 
			/// The Enter method allows one and only one thread to proceed into the following statements; 
			/// all other threads are blocked until the executing thread calls Exit. This is just like using the lock keyword.
			/// Monitors are higher level abstraction of Locks. Internally is uses locks.
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
			/// 
			/// A Mutex is like a C# lock, but it can work across multiple processes of same operating system. In other words, 
			/// Mutex can be computer-wide as well as application-wide.
			/// 
			/// A common use for a cross-process Mutex is to ensure that only one instance of a program can access at a time. 
			/// 
			/// A mutex is locking mechanism used to synchronize access to a resource. Only one task (can be a thread or process based on OS abstraction) can acquire the mutex. It means there is ownership associated with mutex, and only the owner can release the lock (mutex).
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
			/// Use the Semaphore class to control access to a pool of resources.
			/// Threads enter the semaphore by calling the WaitOne method, which is inherited from the WaitHandle class, and 
			/// release the semaphore by calling the Release method.
			/// 
			/// The count on a semaphore is decremented each time a thread enters the semaphore, and incremented when
			/// a thread releases the semaphore.
			/// When the count is zero, subsequent requests block until other threads release the semaphore.When all 
			/// threads have released the semaphore, the count is at the maximum value specified when the semaphore 
			/// was created.
			/// 
			/// There is no guaranteed order, such as FIFO or LIFO, in which blocked threads enter the semaphore.
			/// A thread can enter the semaphore multiple times, by calling the WaitOne method repeatedly. To release some 
			/// or all of these entries, the thread can call the parameterless Release() method overload multiple times, or 
			/// it can call the Release(Int32) method overload that specifies the number of entries to be released.
			/// 
			/// It's often described as a nightclub (the semaphore) where the visitors (threads) stands in a queue outside 
			/// the nightclub waiting 
			/// for someone to leave in order to gain entrance.
			/// 
			/// Semaphore is signaling mechanism (“I am done, you can carry on” kind of signal). 
			/// For example, if you are listening songs (assume it as one task) on your mobile and at the same time
			/// your friend calls you, an interrupt is triggered upon which an interrupt service routine (ISR) signals
			/// the call processing task to wakeup.
			/// </summary>
			private void SimpleSemaphoreExample()
			{
				Console.WriteLine("{0} wants to enter", Thread.CurrentThread.Name);
				_sem.WaitOne();
				Console.WriteLine("{0} has entered Critical section.", Thread.CurrentThread.Name);
				Thread.Sleep(TimeSpan.FromSeconds(4));
				_sem.Release();
				Console.WriteLine("{0} left", Thread.CurrentThread.Name);
			}

			/// <summary>
			/// A thread pool is a collection of threads that can be used to perform several tasks in the background. 
			/// This leaves the primary thread free to perform other tasks asynchronously.
			///
			/// Each incoming request is assigned to a thread from the thread pool, so that the request can be
			/// processed asynchronously, without tying up the primary thread or delaying the processing of subsequent
			/// requests.
			///
			/// Once a thread in the pool completes its task, it is returned to a queue of waiting threads, Thread pools 
			/// typically have a maximum number of threads. If all the threads are busy, additional tasks are put in queue 
			/// until they can be serviced once threads become available.
			/// </summary>
			public void ThreadPoolExample()
			{
				// Queue a task.  
				ThreadPool.QueueUserWorkItem(new WaitCallback(SomeLongTask));
				// Queue another task.  
				ThreadPool.QueueUserWorkItem(new WaitCallback(AnotherLongTask));
			}

			private void SomeLongTask(Object state)
			{
				// Insert code to perform a long task.  
				Thread.Sleep(10000);
			}

			private void AnotherLongTask(Object state)
			{
				// Insert code to perform a long task.  
				Thread.Sleep(1000);
			}

			/// <summary>
			/// Quite often, instances of a type are thread-safe for concurrent read operations, but not for
			/// concurrent updates (nor for a concurrent read and update). This can also be true with resources
			/// such as a file. 
			/// 
			/// Although protecting instances of such types with a simple exclusive lock for all modes of access usually 
			/// does the trick, it can unreasonably restrict concurrency if there are many readers and just occasional
			/// updates. 
			/// 
			/// An example of where this could occur is in a business application server, where commonly used data is 
			/// cached for fast retrieval in static fields.The ReaderWriterLockSlim class is designed to provide
			/// maximum-availability locking in just this scenario.
			/// </summary>
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

		/// <summary>
		/// Event wait handles are used for signaling. Signaling is when one thread waits until it 
		/// receives notification from another.
		/// </summary>
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
}