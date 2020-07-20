using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Models;

namespace CSharp
{
    public class SystemInterfaces
    {
        public void Play()
        {
            //IComparerExample();
            IComparerGenericExample();
            //IComparableExample();
            //IComparableGenericExample();

            //IEqualityComparerExample();
            //IEqualityComparerGenericExample();
            //IEquatableExample();

            //IEnumeratorExample();
            //ICollectionExample();
        }


        public void IComparerExample()
        {
            try
            {
                var employees = MockDataUtility
                    .GetEmployeeMockArray()
                    .Select(x => new Employee
                    {
                        Name = x.Name,
                        Salary = x.Salary,
                        Rank = x.Rank
                    })
                    .ToArray();

                //Console.WriteLine("Sort as EmployeeAsComparable (By Name)");
                //Array.Sort(employees);
                //Utility.PrintployeeMockArray(employees);

                Array.Sort(employees, new EmployeeIComparer());

                Console.WriteLine("Sort with EmployeeComparer (By Rank)");
                MockDataUtility.PrintployeeMockArray(employees);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Exposes a method that compares two objects.
        /// - This interface is used in conjunction with the Array.Sort and Array.BinarySearch methods. It provides a way to 
        ///   customize the sort order of a collection.
        /// </summary>
        public class EmployeeIComparer : IComparer
        {
            /// It Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            int IComparer.Compare(object x, object y)
            {
                var obj1 = x as Employee;
                var obj2 = y as Employee;
                return ((new CaseInsensitiveComparer()).Compare(obj1.Rank, obj2.Rank));
            }
        }

        public void IComparerGenericExample()
        {
            try
            {
                var number = new List<int>() { 4, 7, 9, 10, 11 };
                var employees = new List<Shoe>()
                {
                    new Shoe { Size = 8, Color = 1},
                    new Shoe { Size = 7, Color = 2},
                    new Shoe { Size = 6, Color = 3},
                };

                number.Sort(); //This will work as T is value type
                employees.Sort(); //ArgumentException: At least one object must implement IComparable

                employees.Sort(new ColorShoeComparer());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - This interface is used in conjunction with the Array.Sort and Array.BinarySearch methods.
        /// - It provides a way to customize the sort order of a collection. See the Compare method for notes on parameters and return value
        /// - The default implementation of this interface is the Comparer class.
        /// - For the generic version of this interface, see System.Collections.Generic.IComparer<T>.
        /// - This interface is used with the List<T>.Sort and List<T>.BinarySearch methods. It provides a way to customize the sort order of a collection.
        /// - The default implementation of this interface is the Comparer<T> class. The StringComparer class implements this interface for type String.
        /// </summary>
        class Shoe
        {
            public int Size { get; set; }
            public int Color { get; set; }
        }

        class ColorShoeComparer : IComparer<Shoe>
        {
            public int Size { get; set; }
            public int Color { get; set; }

            public int Compare(Shoe x, Shoe y)
            {
                return x.Color.CompareTo(y.Color);
            }
        }

        public void IComparableExample()
        {
            try
            {
                ArrayList temperatures = new ArrayList();
                Random rnd = new Random();

                for (int ctr = 1; ctr <= 10; ctr++)
                {
                    int degrees = rnd.Next(0, 100);
                    Temperature temp = new Temperature
                    {
                        Celsius = degrees
                    };
                    temperatures.Add(temp);
                }

                temperatures.Sort();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Defines a generalized type-specific comparison method that a value type or class implements to order or sort its instances.
        /// - This interface is implemented by types whose values can be ordered or sorted
        /// - It requires that implementing types define a single method, CompareTo(Object), that indicates whether the position of the current 
        ///	  instance in the sort order is before, after, or the same as a second object of the same type
        ///	- The instance's IComparable implementation is called automatically by methods such as Array.Sort and ArrayList.Sort.
        ///	- All numeric types (such as Int32 and Double) implement IComparable, as do String, Char, and DateTime. Custom types should also provide 
        ///	  their own implementation of IComparable to enable object instances to be ordered or sorted.
        /// </summary>
        public class Temperature : IComparable
        {
            public double Fahrenheit { get; set; }
            public double Celsius { get; set; }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                Temperature otherTemperature = obj as Temperature;
                return Celsius.CompareTo(otherTemperature.Celsius);
            }
        }

        public void IComparableGenericExample()
        {
            try
            {
                var genericEmployees = MockDataUtility
                    .GetEmployeeMockArray()
                    .Select(x => new GenericEmployeeIComparable
                    {
                        Name = x.Name,
                        Salary = x.Salary,
                        Rank = x.Rank
                    })
                    .ToList();

                Console.WriteLine("Sort as EmployeeAsComparable (By Salary)");
                genericEmployees.Sort();
                MockDataUtility.PrintployeeMockArray(genericEmployees);

                var employee1 = genericEmployees[0];
                var employee2 = genericEmployees[1];
                Console.WriteLine($"{employee1.Rank}:{employee1.Name} > {employee2.Rank}:{employee2.Name} is { employee1 > employee2}");
                Console.WriteLine($"{employee1.Rank}:{employee1.Name} < {employee2.Rank}:{employee2.Name} is { employee1 < employee2}");
                Console.WriteLine($"{employee1.Rank}:{employee1.Name} >= {employee2.Rank}:{employee2.Name} is { employee1 >= employee2}");
                Console.WriteLine($"{employee1.Rank}:{employee1.Name} <= {employee2.Rank}:{employee2.Name} is { employee1 <= employee2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Defines a generalized comparison method that a value type or class implements to create a type-specific comparison method for ordering or sorting its instances.
        /// - Typically, the method is not called directly from developer code. Instead, it is called automatically by methods such as List<T>.Sort() and Add.
        /// </summary>
        class GenericEmployeeIComparable : Employee, IComparable<Employee>
        {
            public int CompareTo(Employee other)
            {
                if (other != null)
                    return Salary.CompareTo(other.Salary);
                else
                    throw new ArgumentException("Object is not a Employee");
            }

            // Define the is greater than operator.
            public static bool operator >(GenericEmployeeIComparable operand1, GenericEmployeeIComparable operand2)
            {
                return operand1.Rank.CompareTo(operand2.Rank) == 1;
            }

            // Define the is less than operator.
            public static bool operator <(GenericEmployeeIComparable operand1, GenericEmployeeIComparable operand2)
            {
                return operand1.Rank.CompareTo(operand2.Rank) == -1;
            }

            // Define the is greater than or equal to operator.
            public static bool operator >=(GenericEmployeeIComparable operand1, GenericEmployeeIComparable operand2)
            {
                return operand1.Rank.CompareTo(operand2.Rank) >= 0;
            }

            // Define the is less than or equal to operator.
            public static bool operator <=(GenericEmployeeIComparable operand1, GenericEmployeeIComparable operand2)
            {
                return operand1.Rank.CompareTo(operand2.Rank) <= 0;
            }
        }

        // ---------------------------------------------- IComparer<T> vs IComparable<T> vs Comparer  ---------------------------------------------------
        // What they are :
        // - IComparer<T> is implemented on a type that is capable of comparing two different objects. 
        // - Whereas, IComparable<T> is implemented on types that are able to compare themselves with other instances of the same type
        //
        // What they are for :
        // - Use IComparer<T> is useful for sorting collections as the IComparer<T> stands outside of the comparison. The role of IComparer is to 
        //   provide additional comparison mechanisms. For example, you may want to provide ordering of your class on several fields or properties, 
        //   ascending and descending order on the same field, or both.
        // - Use IComparable<T> for times when I need to know how another instance relates to this instance. The role of IComparable is to provide a 
        //   method of comparing two objects of a particular type. This is necessary if you want to provide any ordering capability for your object.
        // ----------------------------------------------------------------------------------------------------------------------------------------------


        public void IEqualityComparerExample()
        {
            try
            {
                var equalityComparer = new EmployeeIEqualityComparer();
                var hashtable = new Hashtable(equalityComparer);

                var emp1 = new Employee { Name = "Jon Snow", Rank = 1 };
                hashtable.Add(emp1, 107);

                var emp2 = new Employee { Name = "Nikhilesh", Rank = 2 };
                hashtable.Add(emp2, 108);

                var emp3 = new Employee { Name = "Mad King", Rank = 1 };
                hashtable.Add(emp1, 109);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Defines methods to support the comparison of objects for equality.
        /// - In the .NET Framework, constructors of the Hashtable, NameValueCollection, and OrderedDictionary collection types accept this interface.
        /// </summary>
        class EmployeeIEqualityComparer : IEqualityComparer
        {
            public new bool Equals(object x, object y)
            {
                var objX = x as Employee;
                var objY = y as Employee;
                return objX.Rank == objY.Rank;
            }

            //Returns a hash code for the specified object.
            public int GetHashCode(object obj)
            {
                var temp = obj as Employee;
                return temp.Rank.GetHashCode();
            }
        }

        public void IEqualityComparerGenericExample()
        {
            try
            {
                var equalityComparer = new GenericEmployeeIEqualityComparer();
                var dictEmployee = new Dictionary<Employee, int>(equalityComparer);

                var emp1 = new Employee { Name = "Jon Snow" };
                //call GetHashCode
                dictEmployee.Add(emp1, 107);

                var emp2 = new Employee { Name = "Nikhilesh" };
                //call GetHashCode
                dictEmployee.Add(emp2, 108);

                var emp3 = new Employee { Name = "jon snow" };
                //call GetHashCode
                //(Hash same as previous) call Equals
                dictEmployee.Add(emp1, 109);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Defines methods to support the comparison of objects for equality.
        /// - In the .NET Framework, constructors of the Dictionary<TKey,TValue> generic collection type accept this interface.
        /// - This interface supports only equality comparisons. Customization of comparisons for sorting and ordering is provided by the IComparer<T> 
        ///   generic interface.
        /// - We recommend that you derive from the EqualityComparer<T> class instead of implementing the IEqualityComparer<T> interface, because the EqualityComparer<T> 
        ///   class tests for equality using the IEquatable<T>.Equals method instead of the Object.Equals method. 
        /// - This is consistent with the Contains, IndexOf, LastIndexOf,and Remove methods of the Dictionary<TKey,TValue> class and other generic collections.
        /// </summary>
        class GenericEmployeeIEqualityComparer : IEqualityComparer<Employee>
        {
            //Object.Equals
            public bool Equals(Employee x, Employee y)
            {
                if (x == null && y == null)
                    return true;
                else if (x == null || y == null)
                    return false;
                else if (x.Name == y.Name)
                    return true;
                else
                    return false;
            }

            //If it returns same hash then it will goes to equals method
            public int GetHashCode(Employee obj)
            {
                var hash = obj.Name.GetHashCode();
                return hash;
            }
        }

        public void IEquatableExample()
        {
            try
            {
                var employees = new List<Animal>();

                var emp1 = new Animal { Name = "Jon Snow", Kind = "Superhero" };
                employees.Add(emp1);

                var emp2 = new Animal { Name = "Nikhilesh", Kind = "Human" };
                employees.Add(emp2);

                var emp3 = new Animal { Name = "Jon Snow", Kind = "Human" };

                if (employees.Contains(emp3))
                    employees.Add(emp3);
                else
                    Console.WriteLine($"{emp3.Kind}:{emp3.Name} already exists");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// - Defines a generalized method that a value type or class implements to create a type-specific method for determining equality of instances.
        /// - The IEquatable<T> interface is used by generic collection objects such as Dictionary<TKey,TValue>, List<T>, and LinkedList<T> when testing 
        ///   for equality in such methods as Contains, IndexOf, LastIndexOf, and Remove. 
        /// - It should be implemented for any object that might be stored in a generic collection.
        /// - If your type implements IComparable<T>, you almost always also implement IEquatable<T>.
        /// - The main reason is performance. When generics were introduced in .NET 2.0 they were able to add a bunch of neat classes such as List<T>, 
        ///   Dictionary<K,V>, HashSet<T>, etc. These structures make heavy use of GetHashCode and Equals. But for value types this required boxing. 
        ///   IEquatable<T> lets a structure implement a strongly typed Equals method so no boxing is required. Thus much better performance when using 
        ///   value types with generic collections.
        /// </summary>
        class Animal : IEquatable<Animal>
        {
            public string Kind { get; set; }
            public string Name { get; set; }

            public bool Equals(Animal other)
            {
                return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// - Provides a base class for implementations of the IEqualityComparer<T> generic interface.
        /// - Derive from this class to provide a custom implementation of the IEqualityComparer<T> generic interface for use with 
        ///   collection classes such as the Dictionary<TKey,TValue> generic class, or with methods such as List<T>.Sort.
        /// - The Default property checks whether type T implements the System.IEquatable<T> generic interface and, if so, returns an EqualityComparer<T> 
        ///   that invokes the implementation of the IEquatable<T>.Equals method. Otherwise, it returns an EqualityComparer<T>, as provided by T.
        /// - Is is recommend that you derive from the EqualityComparer<T> class instead of implementing the IEqualityComparer<T> interface, because the EqualityComparer<T> 
        ///   class tests for equality using the IEquatable<T>.Equals method instead of the Object.Equals method. This is consistent with the Contains, IndexOf, LastIndexOf,
        ///   and Remove methods of the Dictionary<TKey,TValue> class and other generic collections.
        /// </summary>
        class EmployeeEqualityComparer : EqualityComparer<EmployeeIEquatable>
        {
            public override bool Equals(EmployeeIEquatable x, EmployeeIEquatable y)
            {
                return x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            public override int GetHashCode(EmployeeIEquatable obj)
            {
                return base.GetHashCode();
            }
        }

        // ---------------------------------------------------  IEquatable<T> vs EqualityComparer<T>  ---------------------------------------------------
        // What they are :
        // - In short, if you implement IEquatable<T> on class T, the Equals method on an object of type T tells you if the object itself 
        //   (the one being tested for equality) is equal to another instance of the same type T.
        //   Whereas, IEqualityComparer<T> is for testing the equality of any two instances of T, typically outside the scope of the instances of T.
        //
        // What they are for :
        // - From the definition it should be clear that hence IEquatable<T>(defined in the class T itself) should be the de facto standard to represent 
        //   uniqueness of its objects/instances.
        // - HashSet<T>, Dictionary<T, U>(considering GetHashCode is overridden as well), Contains on List<T> etc make use of this. Implementing 
        //   IEqualityComparer<T> on T doesn't help the above mentioned general cases. Subsequently, there is little value for implementing IEquatable<T> 
        //   on any other class other than T.
        // - IEqualityComparer<T> can be useful when you require a custom validation of equality, but not as a general rule.
        // -----------------------------------------------------------------------------------------------------------------------------------------------



        public void IEnumeratorExample()
        {
            try
            {
                var employees = MockDataUtility.GetEmployeeMockArray().ToArray();
                MockDataUtility.PrintployeeMockArray(employees);

                Console.WriteLine();
                Console.WriteLine($"Using Employee as IEnumerable");
                var employessAsIEnumerable = new EmployeeIEnumerable(employees);
                foreach (Employee item in employessAsIEnumerable)
                    Console.WriteLine($"{item.Rank,3}:{item.Name,-20}:{item.Salary,3:C}");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Exposes an enumerator, which supports a simple iteration over a non-generic collection.
        /// </summary>
        class EmployeeIEnumerable : Employee, IEnumerable
        {
            private readonly Employee[] employees;

            public EmployeeIEnumerable(Employee[] employees)
            {
                this.employees = new Employee[employees.Length];
                for (int i = 0; i < employees.Length; i++)
                {
                    this.employees[i] = employees[i];
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new EmployeeIEnumerator(employees);
            }
        }

        /// <summary>
        /// - When you implement IEnumerable, you must also implement IEnumerator.
        /// </summary>
        class EmployeeIEnumerator : IEnumerator
        {
            public Employee[] employees;
            int position = -1;

            public EmployeeIEnumerator(Employee[] employees)
            {
                this.employees = employees;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Employee Current
            {
                get
                {
                    try
                    {
                        return employees[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                position++;
                return (position < employees.Length);
            }

            public void Reset()
            {
                position = -1;
            }
        }

        public void ICollectionExample()
        {
            try
            {
                var employeeCollection = new EmployeeICollection
                {
                    new EmployeeIEquatable { Name = "Jon Snow", Rank = 1, Salary = 50000  },
                    new EmployeeIEquatable { Name = "Nikhilesh", Rank = 2, Salary = 5000  },
                    new EmployeeIEquatable { Name = "Mad King", Rank = 3, Salary = 55000  },
                    new EmployeeIEquatable { Name = "Nikhilesh", Rank = 3, Salary = 65000  } //Same name will not be added
				};
                MockDataUtility.PrintployeeMockArray(employeeCollection.AsEnumerable());
                Console.WriteLine($"Remove Nikhilesh (Based on Name)");
                employeeCollection.Remove(new EmployeeIEquatable { Name = "Nikhilesh" });
                MockDataUtility.PrintployeeMockArray(employeeCollection.AsEnumerable());

                Console.WriteLine($"employeeCollection.Contains(new EmployeeIEquatable {{ Name = \"Nikhilesh\" }}) > " +
                    $"{employeeCollection.Contains(new EmployeeIEquatable { Name = "Nikhilesh" })}");
                Console.WriteLine($"employeeCollection.Contains(new EmployeeIEquatable {{ Name = \"Mad King\" }}) > " +
                    $"{employeeCollection.Contains(new EmployeeIEquatable { Name = "Mad King" })}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// - Supports a simple iteration over a non-generic collection.
        /// - IEnumerator is the base interface for all non-generic enumerators. 
        /// - Its generic equivalent is the System.Collections.Generic.IEnumerator<T> interface.
        /// </summary>
        class GenericEmployeeIEnumerator : EmployeeIEquatable, IEnumerator<EmployeeIEquatable>
        {
            private int currentIndex;
            private EmployeeICollection collection;
            public EmployeeIEquatable Current { get; private set; }

            public GenericEmployeeIEnumerator(EmployeeICollection collection)
            {
                this.collection = collection;
                currentIndex = -1;
                Current = default(EmployeeIEquatable);
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (++currentIndex >= collection.Count)
                    return false;
                else
                    Current = collection[currentIndex];
                return true;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }

        /// <summary>
        /// - Defines methods to manipulate generic collections.
        /// - The EmployeeICollection class implements the Contains method to use the default equality to determine whether a Employee is in the collection.
        /// - The EmployeeIEquatable class implements the IEquatable<T> interface to define the default equality as the Employee.Name being the same.
        /// - This example also implements an IEnumerator<T> interface for the EmployeeIEquatableCollection class so that the collection can be enumerated.
        /// </summary>
        class EmployeeICollection : EmployeeIEquatable, ICollection<EmployeeIEquatable>
        {
            private List<EmployeeIEquatable> innerCollection;

            public int Count
            {
                get
                {
                    return innerCollection.Count;
                }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public EmployeeICollection()
            {
                innerCollection = new List<EmployeeIEquatable>();
            }

            public IEnumerator<EmployeeIEquatable> GetEnumerator()
            {
                return new GenericEmployeeIEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new GenericEmployeeIEnumerator(this);
            }

            public EmployeeIEquatable this[int index]
            {
                get { return innerCollection[index]; }
                set { innerCollection[index] = value; }
            }

            public bool Contains(EmployeeIEquatable item)
            {
                bool found = false;

                foreach (var employee in innerCollection)
                {
                    // Equality defined by the employee class's implmentation of IEquatable<T>.
                    if (employee.Equals(item))
                        found = true;
                }
                return found;
            }

            public bool Contains(EmployeeIEquatable item, EqualityComparer<EmployeeIEquatable> equalityComparer)
            {
                bool found = false;
                foreach (var employee in innerCollection)
                {
                    if (equalityComparer.Equals(employee, item))
                        found = true;
                }
                return found;
            }

            public void Add(EmployeeIEquatable item)
            {
                if (!Contains(item))
                    innerCollection.Add(item);
                else
                    Console.WriteLine($"{item.Rank}:{item.Name} already exists!");
            }

            public void CopyTo(EmployeeIEquatable[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException("The array cannot be null.");
                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
                if (Count > array.Length - arrayIndex + 1)
                    throw new ArgumentException("The destination array has fewer elements than the collection.");

                for (int i = 0; i < innerCollection.Count; i++)
                    array[i + arrayIndex] = innerCollection[i];
            }

            public bool Remove(EmployeeIEquatable item)
            {
                bool result = false;

                // Iterate the inner collection to find the box to be removed.
                for (int i = 0; i < innerCollection.Count; i++)
                {
                    EmployeeIEquatable employee = innerCollection[i];
                    if (new EmployeeEqualityComparer().Equals(employee, item))
                    {
                        innerCollection.RemoveAt(i);
                        result = true;
                        break;
                    }
                }
                return result;
            }

            public void Clear()
            {
                innerCollection.Clear();
            }
        }
        class EmployeeIEquatable : Employee, IEquatable<Employee>
        {
            public bool Equals(Employee other)
            {
                return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}