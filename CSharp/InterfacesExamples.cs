using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetDemos.CSharpExamples
{
    class InterfacesExamples
    {
        public void DoAction()
        {
            var choice = -1;
            Console.WriteLine("--------Inbuilt Interface Examples --------");
            Console.WriteLine(" 1. IEqualityComparer<T>");
            Console.WriteLine(" 2. IComparable");
            Console.WriteLine(" 3. IComparable<T>");
            Console.WriteLine(" 4. IComparer ");
            Console.WriteLine(" 5. IComparer<T> ");
            Console.WriteLine(" 10. Exit");
            Console.WriteLine("---------------------------------");

            var sortObject = new Sort();
            do
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var equalityComprare = new EmployeeEqualityComparer();

                        var dictEmployee = new Dictionary<Employee, int>(equalityComprare);
                        try
                        {
                            var emp1 = new Employee { Name = "Nik" };
                            dictEmployee.Add(emp1, 107);

                            var emp2 = new Employee { Name = "Chelsi" };
                            dictEmployee.Add(emp2, 108);

                            var emp3 = new Employee { Name = "Nik" };
                            dictEmployee.Add(emp1, 109);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("{0}", ex.Message);
                        }
                        break;

                    case 2:
                        try
                        {
                            Random ran = new Random();
                            var arrayList = new ArrayList();

                            for (int i = 0; i < 10; i++)
                                arrayList.Add(new EmployeeAsIComparable { Name = string.Format("Name {0}", i + 1), Rank = ran.Next(30) });

                            arrayList.Sort();

                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("{0}", ex.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            Random ran = new Random();
                            var arrayList = new List<EmployeeAsGenericIComparable>();

                            for (int i = 0; i < 10; i++)
                                arrayList.Add(new EmployeeAsGenericIComparable { Name = string.Format("Name {0}", i + 1), Rank = ran.Next(30) });

                            arrayList.Sort();

                            var lastEmployee = new EmployeeAsGenericIComparable { Name = string.Format("Name {0}", 1), Rank = ran.Next(30) };

                            if (!arrayList.Contains(lastEmployee))
                                arrayList.Add(lastEmployee);

                            var sharedInheritedEquals = EmployeeAsGenericIComparable.Equals(arrayList.Where(x => x.Name == "Name 1").FirstOrDefault(), lastEmployee);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("{0}", ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            Random ran = new Random();
                            var arrayList = new ArrayList();

                            for (int i = 0; i < 5; i++)
                                arrayList.Add(new Employee { Name = string.Format("Name {0}", i + 1), Rank = ran.Next(30) });

                            foreach (Employee item in arrayList)
                            {
                                Console.WriteLine("\t[{0}]:\t{1}", item.Name, item.Rank);
                            }

                            IComparer comparer1 = new EmployeeAsIComparer();
                            arrayList.Sort(comparer1);

                            Console.WriteLine("");

                            foreach (Employee item in arrayList)
                            {
                                Console.WriteLine("\t[{0}]:\t{1}", item.Name, item.Rank);
                            }                            

                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("{0}", ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            Random ran = new Random();
                            var arrayList = new List<Employee>();

                            for (int i = 0; i < 5; i++)
                                arrayList.Add(new Employee { Name = string.Format("Name {0}", i + 1), Rank = ran.Next(30) });

                            foreach (Employee item in arrayList)
                            {
                                Console.WriteLine("\t[{0}]:\t{1}", item.Name, item.Rank);
                            }

                            arrayList.Sort(new EmployeeAsGenericIComparer());
                            Console.WriteLine("");

                            foreach (Employee item in arrayList)
                            {
                                Console.WriteLine("\t[{0}]:\t{1}", item.Name, item.Rank);
                            }

                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("{0}", ex.Message);
                        }
                        break;
                }
            } while (choice != 10);
        }

        //This interface is used in conjunction with the Array.Sort and Array.BinarySearch methods. 
        //It provides a way to customize the sort order of a collection. See the Compare method for notes on parameters and return value.
        //The default implementation of this interface is the Comparer class. 
        //For the generic version of this interface, see System.Collections.Generic.IComparer<T>.
        internal class EmployeeAsGenericIComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return ((new CaseInsensitiveComparer()).Compare(y.Name, x.Name));
            }
        }

        internal class EmployeeAsIComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var obj1 = x as Employee;
                var obj2 = y as Employee;
                return ((new CaseInsensitiveComparer()).Compare(obj2.Rank, obj1.Rank));
            }
        }

        //Defines a generalized comparison method that a value type or class implements to create a type-specific comparison method for ordering or sorting its instances.
        //Namespace:   System
        //Assembly:  mscorlib(in mscorlib.dll)
        internal class EmployeeAsGenericIComparable : Employee, IComparable<EmployeeAsGenericIComparable>, IEquatable<EmployeeAsGenericIComparable>
        {
            #region IComparable<T>
            //CompareTo provides a strongly typed comparison method for ordering members of a generic collection object. 
            //Because of this, it is usually not called directly from developer code. 
            //Instead, it is called automatically by methods such as List<T>.Sort() and Add.
            public int CompareTo(EmployeeAsGenericIComparable other)
            {
                if (other == null) return 1;

                EmployeeAsGenericIComparable empObject = other as EmployeeAsGenericIComparable;

                if (empObject != null)
                    return this.Rank.CompareTo(empObject.Rank);
                else
                    throw new ArgumentException("Object is not a Student");
            }
            //If you implement IComparable<T>, you should overload the op_GreaterThan, op_GreaterThanOrEqual, op_LessThan, and op_LessThanOrEqual operators 
            //to return values that are consistent with CompareTo. 

            // Define the is greater than operator.
            public static bool operator >(EmployeeAsGenericIComparable operand1, EmployeeAsGenericIComparable operand2)
            {
                return operand1.CompareTo(operand2) == 1;
            }

            // Define the is less than operator.
            public static bool operator <(EmployeeAsGenericIComparable operand1, EmployeeAsGenericIComparable operand2)
            {
                return operand1.CompareTo(operand2) == -1;
            }

            // Define the is greater than or equal to operator.
            public static bool operator >=(EmployeeAsGenericIComparable operand1, EmployeeAsGenericIComparable operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            // Define the is less than or equal to operator.
            public static bool operator <=(EmployeeAsGenericIComparable operand1, EmployeeAsGenericIComparable operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }
            #endregion

            #region IEquatable<T>
            //If you implement IEquatable<T>, you should also override the base class implementations of Object.Equals(Object) 
            //and GetHashCode so that their behavior is consistent with that of the IEquatable<T>.Equals method. 
            //If you do override Object.Equals(Object), your overridden implementation is also called in calls to the static Equals(System.Object, System.Object) 
            //method on your class. In addition, you should overload the op_Equality and op_Inequality operators. 
            //This ensures that all tests for equality return consistent results.
            public bool Equals(EmployeeAsGenericIComparable other)
            {
                if (other == null)
                    return true;
                else if (this.Name.Equals(other.Name))
                    return true;
                else
                    return false;
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as EmployeeAsGenericIComparable);
            }

            public override int GetHashCode()
            {
                return this.Name.GetHashCode();
            }

            public static bool operator ==(EmployeeAsGenericIComparable person1, EmployeeAsGenericIComparable person2)
            {
                if (((object)person1) == null || ((object)person2) == null)
                    return Object.Equals(person1, person2);

                return person1.Equals(person2);
            }

            public static bool operator !=(EmployeeAsGenericIComparable person1, EmployeeAsGenericIComparable person2)
            {
                if (((object)person1) == null || ((object)person2) == null)
                    return !Object.Equals(person1, person2);

                return !(person1.Equals(person2));
            }
            #endregion

        }


        //Defines a generalized type-specific comparison method that a value type or class implements to order or sort its instances.
        //This interface is implemented by types whose values can be ordered or sorted.It requires that implementing types define a single method, 
        //CompareTo(Object), that indicates whether the position of the current instance in the sort order is before, after, or the same as a second 
        //object of the same type.The instance's IComparable implementation is called automatically by methods such as Array.Sort and ArrayList.Sort.
        //The implementation of the CompareTo(Object) method must return an Int32 that has one of three values, as shown in the following table.
        //
        //Less than zero > The current instance precedes the object specified by the CompareTo method in the sort order.
        //Zero > This current instance occurs in the same position in the sort order as the object specified by the CompareTo method.
        //Greater than zero > This current instance follows the object specified by the CompareTo method in the sort order.
        internal class EmployeeAsIComparable : Employee, IComparable
        {
            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                EmployeeAsIComparable empObject = obj as EmployeeAsIComparable;

                if (empObject != null)
                    return this.Rank.CompareTo(empObject.Rank);
                else
                    throw new ArgumentException("Object is not a Student");
            }
        }


        //IEqualityComparer<T>
        //Defines methods to support the comparison of objects for equality.
        //Namespace:   System.Collections.Generic
        //Assembly: mscorlib(in mscorlib.dll)
        //This interface allows the implementation of customized equality comparison for collections.
        internal class EmployeeEqualityComparer : IEqualityComparer<Employee>
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

        internal class EmployeeEqualityComparerByObject : IEqualityComparer<Employee>
        {
            public string Name { get; set; }

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

            public int GetHashCode(Employee obj)
            {
                var hash = obj.Name.GetHashCode();
                return hash;
            }
        }

        internal class Employee
        {
            public string Name { get; set; }

            public int Rank { get; set; }
        }
    }
}
