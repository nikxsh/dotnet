using PlayGround.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlayGround
{
	class Collections
	{
		public void Play()
		{
			//ArrayList();
			//Hashtable();
			//SortedList();
			//HashSet();
			//Iterators();
			//SynchronizedQueue(); //same for stack
			//ConcurrentBag();
			ImmutableArrays();
		}

		/// <summary>
		/// - Implements the IList interface using an array whose size is dynamically increased as required.
		/// - The ArrayList class is designed to hold heterogeneous collections of objects. However, it does not always offer the best performance. 
		///   Instead, use the List<Object>
		/// - The ArrayList collection accepts null as a valid value. It also allows duplicate elements.
		/// </summary>
		private void ArrayList()
		{
			ArrayList arrayList = new ArrayList
			{
				"ABX",
				12,
				true
			};
			Console.WriteLine("-- ArrayList --");
			Console.WriteLine($"arrayList.Count is  {arrayList.Count}");
			Console.WriteLine($"arrayList.Capacity is  {arrayList.Capacity}");
			foreach (var item in arrayList)
				Console.Write($"{item,-4}");
		}

		/// <summary>
		/// - Represents a collection of key/value pairs that are organized based on the hash code of the key.
		/// - It is not recommended that you use the Hashtable class for new development. Instead, it is recommended that you 
		///   use the generic Dictionary<TKey,TValue> class.
		/// </summary>
		private void Hashtable()
		{
			Hashtable hashtable = new Hashtable
			{
				// Add some elements to the hash table. There are no duplicate keys, but some of the values are duplicates.
				{ "txt", "notepad.exe" },
				{ "bmp", "paint.exe" },
				{ "dib", "paint.exe" },
				{ "rtf", "wordpad.exe" }
			};
			try
			{
				Console.WriteLine("-- hashtable --");
				foreach (DictionaryEntry item in hashtable)
					Console.WriteLine($"{item.Key,-10}:{item.Value,5}");
				hashtable.Add("txt", "winword.exe");
			}
			catch
			{
				Console.WriteLine("An element with Key = \"txt\" already exists.");
			}
		}

		/// <summary>
		/// - Represents a collection of key/value pairs that are sorted by key based on the associated IComparer<T> implementation.
		/// - The SortedList<TKey,TValue> generic class is an array of key/value pairs with O(log n) retrieval, where n is the number of elements in the dictionary. 
		/// - In this, it is similar to the SortedDictionary<TKey,TValue> generic class. The two classes have similar object models, and both have O(log n) retrieval. 
		/// - Where the two classes differ is in memory use and speed of insertion and removal:
		/// </summary>
		private void SortedList()
		{
			SortedList sortedList = new SortedList
			{
				{ "txt", "notepad.exe" },
				{ "bmp", "paint.exe" },
				{ "dib", "paint.exe" },
				{ "rtf", "wordpad.exe" }
			};
			try
			{
				Console.WriteLine("-- SortedList --");
				foreach (KeyValuePair<string, string> item in sortedList)
					Console.WriteLine($"{item.Key,-10}:{item.Value,5}");
				sortedList.Add("txt", "winword.exe");
			}
			catch
			{
				Console.WriteLine("An element with Key = \"txt\" already exists.");
			}
		}

		/// <summary>
		/// - The HashSet<T> class provides high-performance set operations. A set is a collection that contains no duplicate elements, 
		///   and whose elements are in no particular order.
		/// </summary>
		private void HashSet()
		{
			IEnumerable<int> enumerableRange = Enumerable.Range(1, 9999999);
			Console.WriteLine("-- IEnumerable --");
			Utility.Watch.Start();
			var checkContainInEnumerable = enumerableRange.Contains(9999999);
			Utility.Watch.Stop();
			Console.WriteLine($"hashSet.Contains(8888888): {checkContainInEnumerable} | {Utility.EllapsedTime(Utility.Watch.ElapsedMilliseconds)}s");

			Utility.Watch.Reset();

			Console.WriteLine("-- HashSet --");
			HashSet<int> hashSet = new HashSet<int>(enumerableRange);
			Utility.Watch.Start();
			var checkContainInHash = hashSet.Contains(9999999);
			Utility.Watch.Stop();
			Console.WriteLine($"hashSet.Contains(8888888): {checkContainInHash} | {Utility.EllapsedTime(Utility.Watch.ElapsedMilliseconds)}s");

		}

		/// <summary>
		/// - Represents a thread-safe, unordered collection of objects.
		/// - Bags are useful for storing objects when ordering doesn't matter, and unlike sets, bags support duplicates. 
		/// - ConcurrentBag<T> is a thread-safe bag implementation, optimized for scenarios where the same thread will be both producing and 
		///   consuming data stored in the bag.
		/// - ConcurrentBag<T> accepts null as a valid value for reference types.
		/// </summary>
		private void ConcurrentBag()
		{
			Console.WriteLine("-- ConcurrentBag --");
			// Add to ConcurrentBag concurrently
			ConcurrentBag<int> cb = new ConcurrentBag<int>();
			List<Task> bagAddTasks = new List<Task>();
			for (int i = 0; i < 50; i++)
			{
				var numberToAdd = i;
				bagAddTasks.Add(Task.Run(() =>
				{
					cb.Add(numberToAdd);
				}));
			}

			// Wait for all tasks to complete
			Task.WaitAll(bagAddTasks.ToArray());

			// Consume the items in the bag
			List<Task> bagConsumeTasks = new List<Task>();
			int itemsInBag = 0;
			while (!cb.IsEmpty)
			{
				bagConsumeTasks.Add(Task.Run(() =>
				{
					if (cb.TryTake(out int item))
					{
						Console.WriteLine(item);
						itemsInBag++;
					}
				}));
			}
			Task.WaitAll(bagConsumeTasks.ToArray());

			Console.WriteLine($"There were {itemsInBag} items in the bag");

			// Checks the bag for an item
			// The bag should be empty and this should not print anything
			if (cb.TryPeek(out int unexpectedItem)) //Inline variable declaration
				Console.WriteLine("Found an item in the bag when it should be empty");
		}

		/// <summary>
		/// - Returns a new Queue that wraps the original queue, and is thread safe.
		/// - Thread safe means it is immune to the Deadlocks and race conditions
		///    Deadlocks:
		///     A deadlock occurs when each of two threads tries to lock a resource the other has already locked.Neither thread can make any further progress.
		///    Race conditions
		///     A race condition is a bug that occurs when the outcome of a program depends on which of two or more threads reaches a particular block of code first. 
		///     Running the program many times produces different results, and the result of any given run cannot be predicted.
		/// </summary>
		private void SynchronizedQueue()
		{
			Console.WriteLine("-- SynchronizedQueue --");
			Queue queue = new Queue();
			queue.Enqueue("The");
			queue.Enqueue("quick");
			queue.Enqueue("brown");
			queue.Enqueue("fox");

			//The wrapper returned by this method locks the queue before an operation is performed so that it is performed in a thread-safe manner.
			Queue synchronizedQueue = Queue.Synchronized(queue);
			Console.WriteLine($"queue is {(queue.IsSynchronized ? "synchronized" : "not synchronized")}");
			Console.WriteLine($"synchronizedQueue  is {(synchronizedQueue.IsSynchronized ? "synchronized" : "not synchronized")}");
		}

		/// <summary>
		/// - Provides methods for creating an array that is immutable; meaning it cannot be changed once it is created.
		/// - With immutable collections, you can:
		///    Share a collection in a way that its consumer can be assured that the collection never changes.
		///    Provide implicit thread safety in multi-threaded applications (no locks required to access collections).
		///    Follow functional programming practices.
		///    Modify a collection during enumeration, while ensuring that the original collection does not change.
		///
		/// https://devblogs.microsoft.com/dotnet/please-welcome-immutablearrayt/
		/// </summary>
		private void ImmutableArrays()
		{
			Console.WriteLine("-- ImmutableArrays --");

			ImmutableArray<int> array = ImmutableArray.Create(1, 2, 3);

			IEnumerable<int> numbers = Enumerable.Range(1, 100);
			ImmutableArray<int> intArray = numbers.ToImmutableArray();

			///  Using the ImmutableArray<T> builder can improve performance as the implementation uses a trick to avoid type checks. 
			///  Normally the CLR has to perform additional type checks at runtime to ensure that storing an element in an array is type safe, 
			///  because arrays are covariant which means the correctness can’t be easily checked statically. 
			///  ImmutableArray<T> avoids this by wrapping references in a pointer-sized value type.
			ImmutableArray<int>.Builder immutableArrayBuilder = ImmutableArray.CreateBuilder<int>();
			immutableArrayBuilder.Add(1);
			immutableArrayBuilder.Add(2);
			immutableArrayBuilder.Add(3);
			ImmutableArray<int> oneTwoThree = immutableArrayBuilder.ToImmutable();

			for (var i = 0; i < array.Length; i++)
			{
				Console.Write($"{array[i], -3}");
			}
			Console.WriteLine();
		}

		/// <summary>
		/// - An iterator can be used to step through collections such as lists and arrays.
		/// - An iterator can be a method or a get accessor. An iterator uses a yield return statement to return each element of the 
		///   collection one at a time.
		/// - You call an iterator by using a foreach statement.Each iteration of the foreach loop calls the iterator.When a yield return statement is reached 
		///   in the iterator, an expression is returned, and the current location in code is retained.Execution is restarted from that location the next time  
		///   that the iterator is called.
		/// - Iterators enable you to maintain the simplicity of a foreach loop when you need to use complex code to populate a list sequence
		/// </summary>
		private void Iterators()
		{
			Console.WriteLine("-- Even Numbers --");
			foreach (var item in EvenSequence(5, 18))
				Console.Write($"{item,-4}");

			Console.WriteLine();

			Console.WriteLine("-- Week --");
			foreach (var item in new DaysOfTheWeek())
				Console.Write($"{item,-4}");
		}

		private IEnumerable<int> EvenSequence(int first, int last)
		{
			for (int i = first; i < last; i++)
				if (i % 2 == 0)
					yield return i;
		}

		public class DaysOfTheWeek : IEnumerable
		{
			private string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

			public IEnumerator GetEnumerator()
			{
				for (int index = 0; index < days.Length; index++)
					yield return days[index];
			}
		}
	}
}