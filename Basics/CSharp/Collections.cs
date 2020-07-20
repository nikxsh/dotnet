using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace CSharp
{
    class Collections
    {
        public void Play()
        {
            ArrayList();
            Hashtable();
            Dictionary();
            SortedList();
            HashSet();
            Iterators();
            SynchronizedQueue();
            ConcurrentBag();
            ImmutableArrays();
        }

        /// <summary>
        /// - Inheritance: Object -> ArrayList
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
        /// - Inheritance: Object -> Hashtable
        /// - A Hashtable object consists of buckets that contain the elements of the collection. A bucket is a virtual subgroup of elements within the Hashtable, 
        ///   which makes searching and retrieving easier and faster than in most collections. Each bucket is associated with a hash code, which is generated using 
        ///   a hash function and is based on the key of the element.
        /// - When an object is added to a Hashtable, it is stored in the bucket that is associated with the hash code that matches the object's hash code. When a value 
        ///   is being searched for in the Hashtable, the hash code is generated for that value, and the bucket associated with that hash code is searched.
        ///   
        ///   For example, a hash function for a string might take the ASCII codes of each character in the string and add them together to generate a hash code.The string 
        ///   "picnic" would have a hash code that is different from the hash code for the string "basket"; therefore, the strings "picnic" and "basket" would be in different 
        ///   buckets.In contrast, "stressed" and "desserts" would have the same hash code and would be in the same bucket.
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
                hashtable.Add("txt", "winword.exe");

                //DictionaryEntry Struct: Defines a dictionary key/value pair that can be set or retrieved.
                //Enumeration: O(n)
                foreach (DictionaryEntry item in hashtable)
                    Console.WriteLine($"{item.Key,-10}:{item.Value,5}");
            }
            catch
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");
            }
        }

        /// <summary>
        /// - Represents a collection of keys and values.
        /// - The Dictionary<TKey,TValue> clas have the same functionality as the Hashtable class. A Dictionary<TKey,TValue> of a specific type (other than Object) 
        ///   provides better performance than a Hashtable for value types. This is because the elements of Hashtable are of type Object; therefore, boxing and unboxing 
        ///   typically occur when you store or retrieve a value type
        /// - The difference between a dictionary and a hashtable is an implementation detail.  In a hashtable, the key of an item is the result of calling GetHashCode() 
        ///   on the value, whereas with a dictionary, the key must be supplied by you.
        /// </summary>
        private void Dictionary()
        {
            //Space: Average O(n) Worst O(n)
            Dictionary<string, string> openWith = new Dictionary<string, string>();

            try
            {
                //Insert: Average O(1) Worst O(n)
                openWith.Add("txt", "notepad.exe");
                openWith.Add("bmp", "paint.exe");
                openWith.Add("txt", "winword.exe");

                //Delete: Average O(1)
                openWith.Remove("doc");

                //Search by key: Average O(1)
                if (!openWith.ContainsKey("doc"))
                {
                    Console.WriteLine("Key \"doc\" is not found.");
                }

                //Search by value: Average O(1) Worst O(n)
                if (!openWith.ContainsValue("paint.exe"))
                {
                    Console.WriteLine("value \"paint.exe\" is not found.");
                }

                //Enumeration: O(n)
                foreach (KeyValuePair<string, string> kvp in openWith)
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");

            }
        }

        /// <summary>
        /// - Represents a collection of key/value pairs that are sorted by key based on the associated IComparer<T> implementation.
        /// - A SortedList object internally maintains two arrays to store the elements of the list; that is, one array for the keys 
        ///   and another array for the associated values.
        /// - A key cannot be null, but a value can be.
        /// - The elements of a SortedList object are sorted by the keys either according to a specific IComparer implementation specified 
        ///   when the SortedList is created or according to the IComparable implementation provided by the keys themselves. In either case, 
        ///   a SortedList does not allow duplicate keys.
        /// </summary>
        private void SortedList()
        {
            SortedList sortedList = new SortedList();
            try
            {
                //Insert: O(n)
                sortedList.Add("Third", "!");
                sortedList.Add("Second", "World");
                sortedList.Add("First", "Hello");

                //Search: O(log n)                
                if (!sortedList.ContainsKey("doc"))
                {
                    Console.WriteLine("Key \"doc\" is not found.");
                }

                //Enumeration: O(n)
                foreach (KeyValuePair<string, string> item in sortedList)
                    Console.WriteLine($"{item.Key,-10}:{item.Value,5}");
            }
            catch
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");
            }
        }

        /// <summary>
        /// - Represents a collection of key/value pairs that are sorted on the key.
        /// - The SortedDictionary<TKey,TValue> generic class is a binary search tree with O(log n) retrieval, where n is the number of elements in the dictionary
        /// </summary>
        private void SortedDictionary()
        {
            SortedDictionary<string, string> openWith = new SortedDictionary<string, string>();
            try
            {
                //Insert: O(log n)
                openWith.Add("txt", "notepad.exe");
                openWith.Add("bmp", "paint.exe");

                //Search by key: O(log n)
                if (!openWith.ContainsKey("doc"))
                {
                    Console.WriteLine("Key \"doc\" is not found.");
                }

                //Search by value: O(n)
                if (!openWith.ContainsValue("paint.exe"))
                {
                    Console.WriteLine("value \"paint.exe\" is not found.");
                }

                //Delete: O(log n)
                openWith.Remove("txt");

                //Enumeration: O(n)
                foreach (KeyValuePair<string, string> item in openWith)
                    Console.WriteLine($"{item.Key,-10}:{item.Value,5}");
            }
            catch
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");
            }
        }

        /// <summary>
        /// - The HashSet<T> class provides high-performance set operations. 
        /// - A set is a collection that contains no duplicate elements, and whose elements are in no particular order.
        /// </summary>
        private void HashSet()
        {
            IEnumerable<int> enumerableRange = Enumerable.Range(1, 9999999);
            Console.WriteLine("-- IEnumerable --");
            MockDataUtility.Watch.Start();
            var checkContainInEnumerable = enumerableRange.Contains(9999999);
            MockDataUtility.Watch.Stop();
            Console.WriteLine($"hashSet.Contains(8888888): {checkContainInEnumerable} | {MockDataUtility.EllapsedTime(MockDataUtility.Watch.ElapsedMilliseconds)}s");

            MockDataUtility.Watch.Reset();

            Console.WriteLine("-- HashSet --");
            //Add: O(1)
            HashSet<int> hashSet = new HashSet<int>(enumerableRange);
            MockDataUtility.Watch.Start();
            //Search: O(1)
            var checkContainInHash = hashSet.Contains(9999999);
            MockDataUtility.Watch.Stop();
            Console.WriteLine($"hashSet.Contains(8888888): {checkContainInHash} | {MockDataUtility.EllapsedTime(MockDataUtility.Watch.ElapsedMilliseconds)}s");
            //Enumeration: O(n*Log(n))
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
                Console.Write($"{array[i],-3}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// - An iterator can be used to step through collections such as lists and arrays.
        /// - An iterator can be a method or a get accessor. An iterator uses a yield return statement to return each element of the 
        ///   collection one at a time.
        /// - You call an iterator by using a foreach statement.Each iteration of the foreach loop calls the iterator. When a yield return statement is reached 
        ///   in the iterator, an expression is returned, and the current location in code is retained. Execution is restarted from that location the next time  
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