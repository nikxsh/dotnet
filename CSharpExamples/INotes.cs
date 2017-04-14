namespace DotNetDemos.CSharpExamples.OOP.Throery
{
    ///Singleton VS. Static Classes

    /// A static class can be used as a convenient container for sets of methods that just operate on input parameters and do not have to get or 
    /// set any internal instance fields. It has the following characteristics:
    ///
    ///     - Contains only static members.
    ///     - Cannot be instantiated.
    ///     - Is sealed.
    ///     - Cannot contain Instance Constructors.
    ///
    /// The advantage of using a static class is that the compiler can check to make sure that no instance members are accidentally added.
    /// The compiler will guarantee that instances of this class cannot be created.
    /// 
    /// With static classes in C# there are two potential dangers if you're not careful.
    ///    - The requested resources will not be freed until the end of application life
    ///    - The values of static variables are shared within an application.Especially bad for ASP.NET applications, because these values will then be 
    ///    shared between all users of a site residing in a particular Application Domain.
    ///    
    /// Using static classes for global data. You can use static classes to store single-instance, global data. The class will usually be initialized lazily, 
    /// at the last possible moment, making startup faster.
    /// However:You lose control over the exact behavior and static constructors are slow.

    /// A "singleton" can be viewed as a shared instance of a class.  This does not necessarily preclude there being other instances.* For example,
    /// System.Text.Encoding.Unicode is a singleton, but you can create instances of Unicode that are configured differently(e.g., you can change how it 
    /// deals with errors). Even if you are only going to have one instance of the class right now, you might need the ability to have multiple instances in 
    /// the future.Going with the singleton gives you this flexibility for the future.  
    /// (Note:  You can make the constructor private or internal--not public--to tightly control the creation of instances to what you support.)
    /// 
    /// Another advantage of a singleton is that you can implement interfaces or derive from other classes (while the static class cannot).This is essential 
    /// for some scenarios.
    /// 
    /// As you can see, you can usually reduce your program so that the only public static properties are the properties that return singleton instances.I think 
    /// this is a very robust way of structuring the program.
    /// 
    /// A shared public method is okay for an operation such as an mathematical function where there is only one definition.
    /// 
    ///     - Singleton object stores in Heap but, static object stores in stack.
    ///     - We can clone the object of Singleton but, we can not clone the static class object.
    ///     - Singleton can use the Object Oriented feature of polymorphism but static class cannot.
    /// 
    /// * A matter of definition.Even if you take the definition as meaning a class that only has one instance, you still should be familiar with the 
    /// various possibilities described here.
    /// 
    /// Singletons preserve the conventional class approach, and don't require that you use the static keyword everywhere. 
    /// They may be more demanding to implement at first, but will greatly simplify the architecture of your program.
    /// And: Unlike static classes, we can use singletons as parameters to methods, or objects.
}
