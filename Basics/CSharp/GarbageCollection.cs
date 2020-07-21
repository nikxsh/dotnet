using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CSharp
{
    /// <summary>
    /// - Memory 
    ///   1. When any process gets triggered, separate virtual space is assigned to that process, from a physical memory which is the same and used 
    ///      by every process of a system, any program deals with virtual space not with physical memory, GC also deals with the same virtual memory 
    ///      to allocate and de-allocate memory. Basically, there are free-blocks that exist in virtual memory (also known as holes), when any object 
    ///      request for memory allocation manager searches for free-block and assigns memory to the said object.
    ///   2. Virtual memory has three blocks:
    ///      Free(empty space)
    ///      Reserved(already allocated)
    ///      Committed(This block is give-out to physical memory and not available for space allocation)
    ///
    /// - Garbage Collection
    ///   1. When you create any object in C#, CLR (common language runtime) allocates memory for the object from heap.
    ///   2. This process is repeated for each newly created object, but there is a limitation to everything, Memory is not un-limited and we 
    ///      need to clean some used space in order to make room for new objects, Here, the concept of garbage collection is introduced, 
    ///   3. Garbage collector manages allocation and reclaiming of memory. GC (Garbage collector) makes a trip to the heap and collects all objects 
    ///      that are no longer used by the application and then makes them free from memory.
    ///   https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/
    ///
    /// - How GC Works?
    ///   1. GC works on managed heap, which is nothing but a block of memory to store objects, when garbage collection process is put in motion, it 
    ///      checks for dead objects and the objects which are no longer used, then it compacts the space of live object and tries to free more memory.
    ///   2. Basically, heap is managed by different 'Generations', it stores and handles long-lived and short-lived objects, see the below generations 
    ///      of Heap:
    ///      a. Generation 0: This generation holds short-lived objects, e.g., Temporary objects. GC initiates garbage collection process frequently in 
    ///         this generation.
    ///      b. Generation 1: This generation is the buffer between short-lived and long-lived objects.
    ///      c. This generation holds long-lived objects like a static and global variable, that needs to be persisted for a certain amount of time. 
    ///         Objects which are not collected in generation Zero, are then moved to generation 1, such objects are known as survivors, similarly objects 
    ///         which are not collected in generation One, are then moved to generation 2 and from there onwards objects remain in the same generation.
    ///
    /// - How GC decide who's gonna die?
    ///   1. When virtual memory is running out of space.
    ///   2. When allocated memory is suppressed acceptable threshold (when GC found if the survival rate (living objects) is high, then it increases the 
    ///      threshold allocation).
    ///   3. When we call GC.Collect() method explicitly, as GC runs continuously, we actually do not need to call this method.
    ///
    /// - Managed and unmanaged objects/resources
    ///   1. Managed objects: Created, managed and under scope of CLR, pure .NET code managed by runtime, Anything that lies within .NET scope and under 
    ///     .NET framework classes such as string, int, bool variables are referred to as managed code.
    ///   2. UnManaged objects: Created outside the control of .NET libraries and are not managed by CLR, example of such unmanaged code is COM objects, 
    ///      file streams, connection objects, network related instances, Interop objects,registries, pointers etc. (Basically, third party libraries that 
    ///      are referred in .NET code.)
    /// </summary>
    class GarbageCollection
    {
        public void Play()
        {
            CustomIDisposable();
            //CustomFinalize();
        }

        private void CustomIDisposable()
        {
            string message = "Valar morghulis!";
            Console.WriteLine($"---  IDisposable Implementation (Base)  ---");
            //using statement ensures object dispose, in short, it gives a comfort way of use of IDisposable objects. 
            //When an Object goes out of scope, Dispose method will get called automatically, basically using block does 
            // the same thing as 'TRY...FINALLY' block.
            using (var resource = new CustomResource(message))
            {
                Console.WriteLine($"{message} > {resource.Reverse(message.Length)}");
            }

            Console.WriteLine();

            message = "valar dohaeris!";
            Console.WriteLine($"---  IDisposable Implementation (Derived)  ---");
            using (var resource = new DerivedResource(message))
            {
                Console.WriteLine($"{message} > {resource.Reverse(message.Length)}");
            }
        }

        private void CustomFinalize()
        {
            var finalizeX = new FinalizeX();
            finalizeX.ShowDuration();
            finalizeX = null;
            GC.Collect();
        }
    }

    /// <summary>
    /// - The primary use of this interface is to release unmanaged resources. 
    ///   The garbage collector automatically releases the memory allocated to a managed object when that object is no longer used. 
    /// - However,it is not possible to predict when garbage collection will occur. 
    /// - Furthermore, the garbage collector has no knowledge of unmanaged resources such as window handles, or open files and streams.3
    /// 
    /// Note: It is a breaking change to add the IDisposable interface to an existing class. Because pre-existing consumers of your type cannot call Dispose, 
    /// you cannot be certain that unmanaged resources held by your type will be released.
    /// </summary>
    class CustomResource : IDisposable
    {
        // Pointer to an external unmanaged resource.
        private IntPtr handle;
        // Other managed resource this class uses.
        SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        // Track whether Dispose has been called.
        private bool disposed = false;

        public CustomResource(string message)
        {
            handle = Marshal.StringToHGlobalAnsi(message);
            Console.WriteLine($"Before Dispose {handle.ToString()}");
        }

        public string Reverse(int length)
        {
            string reverseString = string.Empty;
            unsafe
            {
                IntPtr dptr = Marshal.AllocHGlobal(length + 1);

                byte* src = (byte*)handle.ToPointer();
                byte* dst = (byte*)dptr.ToPointer();

                if (length > 0)
                {
                    // set the source pointer to the end of the string to do a reverse copy.
                    src += length - 1;

                    while (length-- > 0)
                    {
                        *dst++ = *src--;
                    }
                    *dst = 0;
                }
                reverseString = Marshal.PtrToStringAnsi(dptr);
                Marshal.FreeHGlobal(dptr);
            }
            return reverseString;
        }

        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue
            // and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged 
        // resources can be disposed.
        // If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not 
        // reference other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    safeHandle.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
                Marshal.FreeHGlobal(handle);
                handle = IntPtr.Zero;

                Console.WriteLine($"After Dispose {handle.ToString()}");

                // Note disposing has been done.
                disposed = true;

            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~CustomResource()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }
    }


    /// <summary>
    /// - A class derived from a class that implements the IDisposable interface shouldn't implement IDisposable, because the base class implementation of 
    ///   IDisposable.Dispose is inherited by its derived classes. Instead, to implement the dispose pattern for a derived class
    /// </summary>
    class DerivedResource : CustomResource
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Other managed resource this class uses.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public DerivedResource(string message) : base(message)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
            }

            disposed = true;
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// - The Finalize method is used to perform cleanup operations on unmanaged resources held by the current object before the object is destroyed.
    /// - The method is protected and therefore is accessible only through this class or through a derived class.
    /// - The Object class provides no implementation for the Finalize method, and the garbage collector does not mark types derived from Object for 
    ///   finalization unless they override the Finalize method.
    /// - If a type does override the Finalize method, the garbage collector adds an entry for each instance of the type to an internal structure called 
    ///   the finalization queue. 
    /// - The finalization queue contains entries for all the objects in the managed heap whose finalization code must run before the garbage collector 
    ///   can reclaim their memory.
    /// - The garbage collector then calls the Finalize method automatically under the following conditions:
    ///    - After the garbage collector has discovered that an object is inaccessible, unless the object has been exempted from finalization by a 
    ///      call to the GC.SuppressFinalize method.
    ///
    ///https://docs.microsoft.com/en-us/dotnet/api/system.object.finalize?view=netframework-4.8
    /// </summary>
    class FinalizeX
    {
        Stopwatch sw;

        public FinalizeX()
        {
            sw = Stopwatch.StartNew();
            Console.WriteLine("Instantiated object");
        }

        public void ShowDuration()
        {
            Console.WriteLine($"The instance of {this} has been in existence for {sw.Elapsed}");
        }

        ~FinalizeX()
        {
            sw.Stop();
            Console.WriteLine($"The instance of {this} has been in existence for {sw.Elapsed}");
        }
    }

    //SafeHandles
    //Represents a wrapper class for operating system handles. This class must be inherited.
    //The SafeHandle class provides critical finalization of handle resources, preventing handles from being reclaimed prematurely by garbage 
    //collection and from being recycled by Windows to reference unintended unmanaged objects.		
}