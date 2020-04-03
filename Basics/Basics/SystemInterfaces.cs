using Basics.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Basics
{
	public class SystemInterfaces
	{
		public void Play()
		{
			//IComparer();
			//IComparerGeneric();
			//IEqualityComparer();
			//IEqualityComparerGeneric();
			//IComparable();
			//IComparableGeneric();
			//IEquatable();
			//IEnumerator();
			ICollection();
		}


		public void IComparer()
		{
			try
			{
				var employees = Utility.GetEmployeeMockArray()
				.Select(x => new EmployeeIComparable
				{
					Name = x.Name,
					Salary = x.Salary,
					Rank = x.Rank
				}).ToArray();

				Console.WriteLine("Sort as EmployeeAsComparable (By Name)");
				Array.Sort(employees);
				Utility.PrintployeeMockArray(employees);

				Array.Sort(employees, new EmployeeIComparer());

				Console.WriteLine("Sort with EmployeeComparer (By Rank)");
				Utility.PrintployeeMockArray(employees);
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

		public void IComparerGeneric()
		{
			try
			{
				var employees = Utility.GetEmployeeMockArray().ToList();

				Console.WriteLine("Sort without GenericEmployeeComparer (By Name)");
				employees.Sort();
				Utility.PrintployeeMockArray(employees);

				//Sorts the elements in the entire System.Collections.Generic.List`1 using the specified 
				employees.Sort(new GenericEmployeeIComparer());

				Console.WriteLine("Sort with GenericEmployeeComparer (By Rank)");
				Utility.PrintployeeMockArray(employees);
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
		class GenericEmployeeIComparer : IComparer<Employee>
		{
			public int Compare(Employee x, Employee y)
			{
				return x.Rank.CompareTo(y.Rank);
			}
		}

		public void IEqualityComparer()
		{
			try
			{
				var equalityComparer = new EmployeeIEqualityComparer();
				var hashtable = new Hashtable(equalityComparer); //Represents a collection of key/value pairs that are organized based on the hash code of the key.

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

		public void IEqualityComparerGeneric()
		{
			try
			{
				var equalityComparer = new GenericEmployeeIEqualityComparer();
				var dictEmployee = new Dictionary<Employee, int>(equalityComparer);

				var emp1 = new Employee { Name = "Jon Snow" };
				dictEmployee.Add(emp1, 107);

				var emp2 = new Employee { Name = "Nikhilesh" };
				dictEmployee.Add(emp2, 108);

				var emp3 = new Employee { Name = "jon snow" };
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

		public void IComparable()
		{
			try
			{
				//Defines a generalized type-specific comparison method that a value type or class implements to order or sort its instances.
				var employees = Utility.GetEmployeeMockArray().ToList();

				var employeeAsComparable = Utility.GetEmployeeMockArray()
				.Select(x => new EmployeeIComparable
				{
					Name = x.Name,
					Salary = x.Salary,
					Rank = x.Rank
				}).ToArray();

				Console.WriteLine("Sort as EmployeeAsComparable (By Name)");
				Array.Sort(employeeAsComparable);
				Utility.PrintployeeMockArray(employeeAsComparable);
				//The instance's IComparable implementation is called automatically by methods such as Array.Sort and ArrayList.Sort.

			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// - Defines a generalized type-specific comparison method that a value type or class implements to order or sort its instances.
		/// </summary>
		public class EmployeeIComparable : Employee, IComparable
		{
			public int CompareTo(object obj)
			{
				if (obj == null) return 1;

				Employee otherEmployee = obj as Employee;
				return Name.CompareTo(otherEmployee.Name);
			}
		}

		public void IComparableGeneric()
		{
			try
			{
				var genericEmployeeAsComparable = Utility.GetEmployeeMockArray()
				.Select(x => new GenericEmployeeIComparable
				{
					Name = x.Name,
					Salary = x.Salary,
					Rank = x.Rank
				}).ToList();

				Console.WriteLine("Sort as EmployeeAsComparable (By Salary)");
				genericEmployeeAsComparable.Sort();
				Utility.PrintployeeMockArray(genericEmployeeAsComparable);

				var employee1 = genericEmployeeAsComparable[0];
				var employee2 = genericEmployeeAsComparable[1];
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

		public void IEquatable()
		{
			try
			{
				var employees = new List<EmployeeIEquatable>();

				var emp1 = new EmployeeIEquatable { Name = "Jon Snow", Rank = 1};
				employees.Add(emp1);

				var emp2 = new EmployeeIEquatable { Name = "Nikhilesh", Rank = 2};
				employees.Add(emp2);

				var emp3 = new EmployeeIEquatable { Name = "Jon Snow", Rank = 2};
				if (employees.Contains(emp3))
					employees.Add(emp3);
				else
					Console.WriteLine($"{emp3.Rank}:{emp3.Name} already exists");
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
		/// - It should be implemented for any object that might be stored in a 
		///   generic collection.
		/// - If your type implements IComparable<T>, you almost always also implement IEquatable<T>.
		/// </summary>
		class EmployeeIEquatable : Employee, IEquatable<Employee>
		{
			public bool Equals(Employee other)
			{
				return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		public void IEnumerator()
		{
			try
			{
				var employees = Utility.GetEmployeeMockArray().ToArray();
				Utility.PrintployeeMockArray(employees);

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

		public void ICollection()
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
				Utility.PrintployeeMockArray(employeeCollection.AsEnumerable());
				Console.WriteLine($"Remove Nikhilesh (Based on Name)");
				employeeCollection.Remove(new EmployeeIEquatable { Name = "Nikhilesh" });
				Utility.PrintployeeMockArray(employeeCollection.AsEnumerable());

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
	}
}