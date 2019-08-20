using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNetDemos.CSharpExamples
{
	/**
	* - In the common language runtime (CLR), the garbage collector serves as an automatic memory manager. It provides 
	*   the following benefits:
	* - Enables you to develop your application without having to free memory.
	* - Allocates objects on the managed heap efficiently.
	* - Reclaims objects that are no longer being used, clears their memory, and keeps the memory available for 
	*   future allocations.Managed objects automatically get clean content to start with, so their constructors do 
	*   not have to initialize every data field.
	* - Provides memory safety by making sure that an object cannot use the content of another object.
	* 
	* Garbage collection occurs when one of the following conditions is true:
	*  1. The system has low physical memory.
	*  2. The memory that is used by allocated objects on the managed heap surpasses an acceptable threshold. This
	*     threshold is continuously adjusted as the process runs.
	*  3. The System.GC.Collect method is called.In almost all cases, you do not have to call this method, because 
	*     the garbage collector runs continuously. This method is primarily used for unique situations and testing.
	*    
	* The managed heap
	*  - After the garbage collector is initialized by the CLR, it allocates a segment of memory to store and manage
	*    objects.This memory is called the managed heap, as opposed to a native heap in the operating system.
	*  - There is a managed heap for each managed process.All threads in the process allocate memory for objects on the
	*    same heap.
	*  - To reserve memory, the garbage collector calls the Win32 VirtualAlloc function, and reserves one segment of
	*    memory at a time for managed applications. The garbage collector also reserves segments as needed, and releases
	*    segments back to the operating system (after clearing them of any objects) by calling the Win32 VirtualFree 
	*    function.
	*  - The fewer objects allocated on the heap, the less work the garbage collector has to do. When you allocate 
	*    objects, do not use rounded-up values that exceed your needs, such as allocating an array of 32 bytes when 
	*    you need only 15 bytes.
	*  - When a garbage collection is triggered, the garbage collector reclaims the memory that is occupied by dead 
	*    objects.The reclaiming process compacts live objects so that they are moved together, and the dead space is
	*    removed, thereby making the heap smaller.This ensures that objects that are allocated together stay together
	*    on the managed heap, to preserve their locality.
	*  - The intrusiveness (frequency and duration) of garbage collections is the result of the volume of allocations
	*    and the amount of survived memory on the managed heap.
	*  - The heap can be considered as the accumulation of two heaps: the large object heap and the small object heap.
	*  - The large object heap contains very large objects that are 85,000 bytes and larger.The objects on the large
	*    object heap are usually arrays.It is rare for an instance object to be extremely large.
	*    
	* Generations
	*  - The heap is organized into generations so it can handle long-lived and short-lived objects.Garbage collection
	*    primarily occurs with the reclamation of short-lived objects that typically occupy only a small part of the
	*    heap. 
	*    
	*    There are three generations of objects on the heap:
	*    
	*    Generation 0 : This is the youngest generation and contains short-lived objects. An example of a short-lived
	*    object is a temporary variable.Garbage collection occurs most frequently in this generation.
	*    Newly allocated objects form a new generation of objects and are implicitly generation 0 collections, unless 
	*    they are large objects, in which case they go on the large object heap in a generation 2 collection.
	*    Most objects are reclaimed for garbage collection in generation 0 and do not survive to the next generation.
	*    
	*    Generation 1 : This generation contains short-lived objects and serves as a buffer between short-lived objects 
	*    and long-lived objects.
	*    
	*    Generation 2 : This generation contains long-lived objects. An example of a long-lived object is an object in 
	*    a server application that contains static data that is live for the duration of the process.
	*    Garbage collections occur on specific generations as conditions warrant.Collecting a generation means 
	*    collecting objects in that generation and all its younger generations.A generation 2 garbage collection is 
	*    also known as a full garbage collection, because it reclaims all objects in all generations (that is, all 
	*    objects in the managed heap).
	*  
	* Survival and promotions
	*  -  Objects that are not reclaimed in a garbage collection are known as survivors, and are promoted to the next 
	*     generation.Objects that survive a generation 0 garbage collection are promoted to generation 1; objects that 
	*     survive a generation 1 garbage collection are promoted to generation 2; and objects that survive a generation 
	*     2 garbage collection remain in generation 2.
	*  -  When the garbage collector detects that the survival rate is high in a generation, it increases the threshold
	*     of allocations for that generation, so the next collection gets a substantial size of reclaimed memory.The CLR
	*     continually balances two priorities: not letting an application's working set get too big and not letting the 
	*     garbage collection take too much time.
	*     
	*  Workstation and server garbage collection
	*  -  Workstation garbage collection, which is available on all systems. For single-processor computers, 
	*     the default workstation garbage collection should be the fastest option. Workstation garbage collection 
	*     can be concurrent or non-concurrent. Concurrent garbage collection enables managed threads to continue 
	*     operations during a garbage collection.
	*  -  Server garbage collection, which is available on multiprocessor systems. You use the <gcServer> element to
	*     control the type of garbage collection the CLR performs. Use the System.Runtime.GCSettings.IsServerGC 
	*     property to determine if server garbage collection is enabled. Server garbage collection can be used for
	*     two-processor computers. Server garbage collection should be the fastest option for more than two 
	*     processors.Server garbage collection, which is intended for server applications that need high throughput 
	*     and scalability. Server garbage collection can be non-concurrent or background.
	*     => The following example enables server garbage collection.
	*     <configuration>  
	*       <runtime>  
	*         <gcServer enabled = "true" />
	*       </runtime>
	*     </configuration>
	*  -  Starting with the .NET Framework 4, background garbage collection replaces concurrent garbage collection
	*/
	class GarbageCollection
	{
		public void WithIDisposable()
		{
			for (int i = 0; i < 100; i++)
			{
				using (var state = new UmanagedDatabaseState())
				{
					Console.WriteLine(state.GetData());
				}

				*/ This will give exception
				//var state = new UmanagedDatabaseState();
				//Console.WriteLine(state.GetData());
			}
			Console.WriteLine("The End");
		}
	}

	class DatabaseState : IDisposable
	{
		protected SqlConnection _connection;
		private bool _disposed = false;

		public virtual string GetData()
		{
			if (_disposed)
				throw new ObjectDisposedException("Datastate");

			if (_connection == null)
			{
				_connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=SCS_Tenant_Reporting_localhost;Integrated Security=True;Trusted_Connection=True;App=testapp;Max pool size=215;Connection Timeout=2;");
				_connection.Open();
			}
			using (var _command = _connection.CreateCommand())
			{
				_command.CommandText = "Select CONCAT(FirstName,' ', LastName) from [UserManagement].[User] where Username = 'superadmin'";
				return _command.ExecuteScalar().ToString();
			}
		}

		/**
		* - Use this method to close or release unmanaged resources such as files, streams, and handles held by an 
		*   instance of the class that implements this interface. By convention, this method is used for all tasks 
		*   associated with freeing resources held by an object, or preparing an object for reuse.
		* - When implementing this method, ensure that all held resources are freed by propagating the call through 
		*   the containment hierarchy. For example, if an object A allocates an object B, and object B allocates an 
		*   object C, then A's Dispose implementation must call Dispose on B, which must in turn call Dispose on C. 
		*   An object must also call the Dispose method of its base class if the base class implements IDisposable.
		* - If an object's Dispose method is called more than once, the object must ignore all calls after the first
		*   one. The object must not throw an exception if its Dispose method is called multiple times. Instance 
		*   methods other than Dispose can throw an ObjectDisposedException when resources are already disposed. 
		* - Because the Dispose method must be called explicitly, objects that implement IDisposable must also
		*   implement a finalizer to handle freeing resources when Dispose is not called. 
		* - By default, the garbage  collector automatically calls an object's finalizer prior to reclaiming its memory. 
		*   However, once the Dispose method has been called, it is typically unnecessary for the garbage collector to call the disposed 
		*   object's finalizer. To prevent automatic finalization, Dispose implementations can call the 
		*   GC.SuppressFinalize method.
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					if (_connection != null)
					{
						Console.WriteLine(string.Format("Disposing SQLConnection: {0}", _connection.GetHashCode()));
						_connection.Dispose();
						_connection = null;
					}
					_disposed = true;
				}
			}
		}

		//select*
		//from[master].sys.sysprocesses
		//where program_name = 'testapp'
	}

	class UmanagedDatabaseState : DatabaseState
	{
		private SqlCommand _command;
		IntPtr _umanagedPointer;
		public override string GetData()
		{
			var data = base.GetData();
			if (_command != null)
			{
				_command = _connection.CreateCommand();
			}

			if (_umanagedPointer == IntPtr.Zero)
			{
				_umanagedPointer = Marshal.AllocHGlobal(100 * 1024 * 1024);
			}
			return data;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_command != null)
				{
					_command.Dispose();
					_command = null;
				}
			}

			if (_umanagedPointer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_umanagedPointer);
				_umanagedPointer = IntPtr.Zero;
			}

			base.Dispose(disposing);
		}

		~UmanagedDatabaseState()
		{
			Dispose(false);
		}
	}
	
	/**
	*- Unfortunately, managed memory is just one of many types of system resources. Resources other than managed
	*  memory still need to be released explicitly and are referred to as unmanaged resources. The GC was specifically
	*  not designed to manage such unmanaged resources, which means that the responsibility for managing unmanaged 
	*  resources lies in the hands of the developers.
	*- The CLR provides some help in releasing unmanaged resources.System.Object declares a virtual method Finalize
	*  (also called the finalizer) that is called by the GC before the object’s memory is reclaimed by the GC and 
	*  can be overridden to release unmanaged resources.
	*- The Finalize method is used to perform cleanup operations on unmanaged resources held by the current object 
	*  before the object is destroyed. The method is protected and therefore is accessible only through this class or
	*  through a derived class.
	*- The CLR provides some help in releasing unmanaged resources. System.Object declares a virtual method 
	*  Finalize (also called the finalizer) that is called by the GC before the object’s memory is reclaimed by 
	*  the GC and can be overridden to release unmanaged resources.  
	*- Although finalizers are effective in some cleanup scenarios, they have two significant drawbacks:
	*  1. The finalizer is called when the GC detects that an object is eligible for collection.This happens at some 
	*     undetermined period of time after the resource is not needed anymore.The delay between when the developer 
	*     could or would like to release the resource and the time when the resource is actually released by the 
	*     finalizer might be unacceptable in programs that acquire many scarce resources(resources that can be easily 
	*     exhausted) or in cases in which resources are costly to keep in use(e.g., large unmanaged memory buffers).
	*  2. When the CLR needs to call a finalizer, it must postpone collection of the object’s memory until the next 
	*     round of garbage collection(the finalizers run between collections). This means that the object’s memory
	*     (and all objects it refers to) will not be released for a longer period of time.
	*- Therefore, relying exclusively on finalizers might not be appropriate in many scenarios when it is important 
	*  to reclaim unmanaged resources as quickly as possible, when dealing with scarce resources, or in highly
	*  performant scenarios in which the added GC overhead of finalization is unacceptable.
	 */
	public class FinalizeClass
	{
		Stopwatch sw;

		public FinalizeClass()
		{
			sw = Stopwatch.StartNew();
			Console.WriteLine("Instantiated object");
		}

		public void ShowDuration()
		{
			Console.WriteLine("This instance of {0} has been in existence for {1}",
									this, sw.Elapsed);
		}

		~FinalizeClass()
		{
			Console.WriteLine("Finalizing object");
			sw.Stop();
			Console.WriteLine("This instance of {0} has been in existence for {1}",
									this, sw.Elapsed);
		}
	}

	// The FinalizeClass displays output like the following:
	//    Instantiated object
	//    This instance of ExampleClass has been in existence for 00:00:00.0011060
	//    Finalizing object
	//    This instance of ExampleClass has been in existence for 00:00:00.0036294
}
