using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetDemos.CSharpExamples
{
	public class AsynchronousProgramming
	{
		private Stopwatch _stopWatch = new Stopwatch();

		private readonly Func<long, int> EllapsedTime = x => (int)x / 1000;

		public void Play()
		{
			//_ = AsyncAndAwait();
			//_ = AsyncAndAwaiteWhenAll();
			//_ = AsyncAndAwaiteWhenAny();
			//_ = AccessWebAsync();
			TaskBasics();
			//TaskFactory();
			//TaskContinuations();
			//DetachedChildTask();
			//AttachedChildTask();
		}

		/// <summary>
		/// 1.The Task asynchronous programming model (TAP) provides an abstraction over asynchronous code.
		/// 2.For a parallel algorithm, you'd need multiple cooks (or threads). One would make the eggs, one the bacon, and so on. 
		///	Each one would be focused on just that one task. Each cook (or thread) would be blocked synchronously waiting for bacon to be 
		///	ready to flip, or the toast to pop.
		/// 3.constructing synchronous code to perform asynchronous operations. As written, this code blocks the thread executing it from 
		///	doing any other work. It won't be interrupted while any of the tasks are in progress. It would be as though you stared at the toaster 
		///	after putting the bread in. You'd ignore anyone talking to you until the toast popped.
		/// 4.The await keyword provides a non-blocking way to start a task, then continue execution when that task completes.
		/// 
		/// This code doesn't block while the milk is boiling or Tea is in progress. 
		/// This code won't start any other tasks though. You'd still Cut Vegetables and wait until is is done. 
		/// But at least, you'd respond to anyone that wanted your attention. 
		/// 
		///  > Async improves responsiveness
		///    - Asynchrony proves especially valuable for applications that access the UI thread because all UI-related activity usually shares one thread.
		///    - If any process is blocked in a synchronous application, all are blocked.Your application stops responding, and you might conclude that it has 
		///    failed when instead it's just waiting.
		///  > Threads
		///    - Async methods are intended to be non-blocking operations. An await expression in an async method doesn’t block the current thread while the awaited 
		///    task is running. Instead, the expression signs up the rest of the method as a continuation and returns control to the caller of the async method.
		///    - The async and await keywords don't cause additional threads to be created. Async methods don't require multithreading because an async method doesn't 
		///      run on its own thread. The method runs on the current synchronization context and uses time on the thread only when the method is active. 
		///  > If you specify that a method is an async method by using the async modifier, you enable the following two capabilities.
		///    - The marked async method can use await to designate suspension points.The await operator tells the compiler that the async method can't continue past 
		///      that point until the awaited asynchronous process is complete. In the meantime, control returns to the caller of the async method.
		///      The suspension of an async method at an await expression doesn't constitute an exit from the method, and finally blocks don’t run.
		///    - The marked async method can itself be awaited by methods that call it.
		///  > Async methods can have the following return types:
		///    - Task<TResult>, for an async method that returns a value.
		///    - Task, for an async method that performs an operation but returns no value.
		///    - void, for an event handler.
		///    - Starting with C# 7.0, any type that has an accessible GetAwaiter method
		/// </summary>
		public async Task AsyncAndAwait()
		{
			_stopWatch.Start();
			Console.WriteLine($"Get the Tea!");
			Console.WriteLine($"Get the Suger!");
			await BoilMilk(_stopWatch);
			Console.WriteLine($"Get the vegies out of fridge!");
			await PrepareTea(_stopWatch);
			Console.WriteLine($"Where is my knife!?");
			await CutVegetables(_stopWatch);
			Console.WriteLine($"Get the peanuts!");
			await FryPoha(_stopWatch);
			Console.WriteLine($"Lets eat now :)");
			_stopWatch.Stop();
		}

		/// <summary>
		/// 1. In many scenarios, you want to start several independent tasks immediately. 
		/// Then, as each task finishes, you can continue other work that's ready
		/// 2. You start a task and hold on to the Task object that represents the work. You'll await each task before working with its result.
		/// </summary>
		public async Task AsyncAndAwaiteWhenAll()
		{
			_stopWatch.Start();
			Console.WriteLine($"Get the Tea!");
			Console.WriteLine($"Get the Suger!");
			Console.WriteLine($"Get the vegies out of fridge!");
			Console.WriteLine($"Where is my knife!?");
			await Task.WhenAll(BoilMilk(_stopWatch), PrepareTea(_stopWatch), CutVegetables(_stopWatch));
			Console.WriteLine($"Get the peanuts!");
			Console.WriteLine($"Get the Kontinbir");
			await FryPoha(_stopWatch);
			Console.WriteLine($"Lets eat now :)");
			_stopWatch.Stop();
		}

		/// <summary>
		/// Following code shows how you could use WhenAny to await the first task to finish and then process its result
		/// After processing the result from the completed task, you remove that completed task from the list of tasks passed to WhenAny.
		/// </summary>
		public async Task AsyncAndAwaiteWhenAny()
		{
			_stopWatch.Start();
			var boilMilkTask = BoilMilk(_stopWatch);
			var prepareTea = PrepareTea(_stopWatch);
			var cutVegies = CutVegetables(_stopWatch);
			var fryPoha = FryPoha(_stopWatch);

			var remainingTask = new List<Task> { boilMilkTask, prepareTea, cutVegies, fryPoha };

			while (remainingTask.Any())
			{
				Task finished = await Task.WhenAny(remainingTask);

				if (finished == boilMilkTask)
					Console.WriteLine($"Get the Suger!");
				else if (finished == prepareTea)
					Console.WriteLine($"Lets have some cookies!");
				else if (finished == cutVegies)
					Console.WriteLine($"Get the Kontinbir!");
				else if (finished == fryPoha)
					Console.WriteLine($"Lets eat now :)");

				remainingTask.Remove(finished);
			}
			_stopWatch.Stop();
		}

		async Task BoilMilk(Stopwatch _stopWatch)
		{
			Console.WriteLine($"Async : Milk is Boiling");
			await Task.Run(() => Thread.Sleep(3000));
			Console.WriteLine($"Milk is Ready! | Time Elapsed {EllapsedTime(_stopWatch.ElapsedMilliseconds)} Secs");
		}

		async Task PrepareTea(Stopwatch _stopWatch)
		{
			Console.WriteLine($"Async : Preparing Tea");
			await Task.Run(() => Thread.Sleep(10000));
			Console.WriteLine($"Tea is Ready! | Time Elapsed {EllapsedTime(_stopWatch.ElapsedMilliseconds)} Secs");
		}

		async Task CutVegetables(Stopwatch _stopWatch)
		{
			Console.WriteLine($"Async : Cutting Vegetables");
			await Task.Run(() => Thread.Sleep(3000));
			Console.WriteLine($"Vegies are Ready! | Time Elapsed {EllapsedTime(_stopWatch.ElapsedMilliseconds)} Secs");
		}

		async Task FryPoha(Stopwatch _stopWatch)
		{
			Console.WriteLine($"Async : Frying Poha");
			await Task.Run(() => Thread.Sleep(5000));
			Console.WriteLine($"Poha is Ready!| Time Elapsed {EllapsedTime(_stopWatch.ElapsedMilliseconds)} Secs");
		}

		public async Task AccessWebAsync()
		{
			var content = await AccessTheWebAsync();
			Console.WriteLine($"Content Length is {content}");
		}

		/// <summary>
		/// The method usually includes at least one await expression, which marks a point where the method can't continue until the awaited asynchronous 
		/// operation is complete. In the meantime, the method is suspended, and control returns to the method's caller.
		/// 
		/// If GetStringAsync (and therefore getStringTask) completes before AccessTheWebAsync awaits it, control remains in AccessTheWebAsync. 
		/// The expense of suspending and then returning to AccessTheWebAsync would be wasted if the called asynchronous process (getStringTask) has already 
		/// completed and AccessTheWebAsync doesn't have to wait for the final result.
		/// </summary>
		async Task<int> AccessTheWebAsync()
		{
			using (HttpClient client = new HttpClient())
			{
				Task<string> getStringTask = client.GetStringAsync("http://www.guimp.com/");

				DoIndependentWork();

				string urlContents = await getStringTask;

				return urlContents.Length;
			}
		}

		private void DoIndependentWork()
		{
			Console.WriteLine($"Doin yes");
			Thread.Sleep(2000);
			Console.WriteLine($"Doin No");
			Thread.Sleep(2000);
			Console.WriteLine($"Doin La La LA!");
			Thread.Sleep(2000);
		}


		/// <summary>
		/// 1. The Task Parallel Library (TPL) is based on the concept of a task, which represents an asynchronous operation. 
		/// 2. In some ways, a task resembles a thread or ThreadPool work item, but at a higher level of abstraction. 
		/// 3. The term task parallelism refers to one or more independent tasks running concurrently. 
		///  Tasks provide two primary benefits:
		///    a. More efficient and more scalable use of system resources.
		///     Behind the scenes, tasks are queued to the ThreadPool, which has been enhanced with algorithms that determine 
		///     and adjust to the number of threads and that provide load balancing to maximize throughput.This makes tasks relatively 
		///     lightweight, and you can create many of them to enable fine-grained parallelism.
		///    b.  More programmatic control than is possible with a thread or work item.
		///     Tasks and the framework built around them provide a rich set of APIs that support waiting, cancellation, continuations, 
		///     robust exception handling, detailed status, custom scheduling, and more.
		/// 4. For both of these reasons, in the.NET Framework, TPL is the preferred API for writing multi-threaded, asynchronous, 
		///    and parallel code.
		/// </summary>
		public void TaskBasics()
		{
			CancellationTokenSource source = new CancellationTokenSource();

			Task task1 = new Task(() => ToSquare(10, 2000));
			task1.Start();

			Task task2 = new Task(() => ToSquare(20, 5000));
			task2.Start();

			Task task3 = new Task(() => ToSquare(30, 3000));
			task3.Start();

			//Task.Run methods to create and start a task in one operation
			//To manage the task, the Run methods use the default task scheduler, regardless of which task scheduler is associated with the current thread. 
			//The Run methods are the preferred way to create and start tasks when more control over the creation and scheduling of the task is not needed
			Task task4 = Task.Run(() => ToSquare(40, 1000));

			//The following example launches a task that includes a call to the Delay(TimeSpan, CancellationToken) method with a 1.5 second delay. 
			//Before the delay interval elapses, the token is cancelled. The output from the example shows that, as a result, a TaskCanceledException 
			//is thrown, and the tasks' Status property is set to Canceled.
			Task task5 = Task.Run(async delegate
			{
				await Task.Delay(TimeSpan.FromSeconds(10), source.Token);
				ToSquare(50, 1000);
			});
			//source.Cancel();

			Task.WaitAll(task1, task2, task3, task4, task5);
		}

		public void ToSquare(int value, int delay)
		{
			Thread.Sleep(delay);
			Console.WriteLine($"Square of {value} is {Math.Pow(value, 2)} from Thread {Thread.CurrentThread.ManagedThreadId}");
		}
		
		class CustomData
		{
			public long CreationTime;
			public int Name;
			public int ThreadNum;
		}

		//You can also use the TaskFactory.StartNew method to create and start a task in one operation. Use this method when creation and 
		//scheduling do not have to be separated and you require additional task creation options or the use of a specific scheduler, or when 
		//you need to pass additional state into the task that you can retrieve through its Task.AsyncState property
		public void TaskFactory()
		{
			Task[] taskArray = new Task[10];
			for (int i = 0; i < taskArray.Length; i++)
			{
				taskArray[i] = Task.Factory.StartNew((Object obj) =>
				{
					CustomData data = obj as CustomData;
					if (data == null)
						return;

					data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
				}, new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
			}
			Task.WaitAll(taskArray);
			foreach (var task in taskArray)
			{
				//This state is passed as an argument to the task delegate, and it can be accessed from the task object by using the Task.AsyncState property.
				var data = task.AsyncState as CustomData;
				if (data != null)
					Console.WriteLine($"Task #{data.Name} created at {data.CreationTime}, ran on thread #{data.ThreadNum}.");
			}
		}

		/// <summary>
		/// The Task.ContinueWith and Task<TResult>.ContinueWith methods let you specify a task to start when the antecedent task finishes. 
		/// The delegate of the continuation task is passed a reference to the antecedent task so that it can examine the antecedent task's status 
		/// and, by retrieving the value of the Task<TResult>.Result property, can use the output of the antecedent as input for the continuation.
		/// </summary>
		public void TaskContinuations()
		{
			var task1 = Task.Factory.StartNew(() =>
			{
				var message = $"Task 1 from Thread {Thread.CurrentThread.ManagedThreadId}";
				return message;
			});

			// Execute the continuation when the antecedent (Task1) finishes
			var task2 = task1.ContinueWith((antecedent) =>
			{
				Console.WriteLine(antecedent.Result);
				Console.WriteLine($"Task 2 after antecedent Task1 finishes from Thread {Thread.CurrentThread.ManagedThreadId}");
				return 22;
			});

			var task3 = Task.Factory.StartNew(() => { Console.WriteLine($"Task 3 from Thread {Thread.CurrentThread.ManagedThreadId}"); return 22; });
			var task4 = Task.Factory.StartNew(() => { Console.WriteLine($"Task 4 from Thread {Thread.CurrentThread.ManagedThreadId}"); return 22; });
			var task5 = Task.Factory.StartNew(() => { Console.WriteLine($"Task 5 from Thread {Thread.CurrentThread.ManagedThreadId}"); return 22; });

			Task all = Task.Factory.ContinueWhenAll(
			new[] { task2, task3, task4, task5 },
			tasks => Console.WriteLine($"All tasks returned with total {tasks.Sum(t => t.Result)}")
			);

			//Call to the Task.Wait method to ensure that the task completes execution before the console mode application or current application ends.
			task1.Wait();

			//If an antecedent throws and the continuation fails to query the antecedent’s Exception property 
			//(and the antecedent isn’t otherwise waited upon), the exception is considered unhandled and the application dies
			//A safe pattern is to rethrow antecedent exceptions. As long as the continuation is Waited upon, 
			//the exception will be propagated and rethrown to the Waiter
			bool error = false;
			var normalTask = Task.Factory.StartNew(() => { if (error) throw null; else return "Success"; });

			var catchTask = normalTask.ContinueWith((antecedent) => Console.WriteLine(antecedent.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);

			var OkayTask = normalTask.ContinueWith((antecedent) => Console.WriteLine(antecedent.Result), TaskContinuationOptions.NotOnFaulted);
		}

		/// <summary>
		/// When user code that is running in a task creates a new task and does not specify the AttachedToParent option, the new task is not synchronized 
		/// with the parent task in any special way. This type of non-synchronized task is called a detached nested task or detached child task.
		/// </summary>
		public void DetachedChildTask()
		{
			var parent = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Parent task beginning."); //This print First
				for (int ctr = 1; ctr <= 10; ctr++)
				{
					var child = Task.Factory.StartNew((x) =>
					{
						Thread.Sleep(2000);
						Console.WriteLine($"Attached child #{x} completed by Thread {Thread.CurrentThread.ManagedThreadId}."); //This prints afterwords
					}, ctr);
				}
			});

			parent.Wait();
			Console.WriteLine("Parent task completed."); //This print second
		}

		/// <summary>
		/// 1. When user code that is running in a task creates a task with the AttachedToParent option, the new task is known as a attached child task of 
		///    the parent task. You can use the AttachedToParent option to express structured task parallelism, because the parent task implicitly waits for 
		///    all attached child tasks to finish
		/// 2. Parent task can use the TaskCreationOptions.DenyChildAttach option to prevent other tasks from attaching to the parent task
		/// </summary>
		public void AttachedChildTask()
		{
			var parent = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Parent task beginning.");
				for (int ctr = 1; ctr <= 10; ctr++)
				{
					Task.Factory.StartNew((x) =>
					{
						Thread.Sleep(2000);
						Console.WriteLine($"Attached child #{x} completed by Thread {Thread.CurrentThread.ManagedThreadId}.");
					},
					ctr, 
					TaskCreationOptions.AttachedToParent);
				}
			});

			parent.Wait();
			Console.WriteLine("Parent task completed.");
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
