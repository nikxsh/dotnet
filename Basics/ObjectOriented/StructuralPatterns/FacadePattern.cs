using System;
using System.Collections.Generic;

namespace ObjectOriented.StructuralPatterns
{
    /// <summary>
    /// - Structural Patterns
    /// - A single class that represents an entire subsystem
    /// - Provide a unified interface to a set of interfaces in a subsystem. Façade defines a higher-level interface that makes the subsystem easier to use.
    /// - Facade pattern hides the complexities of the system and provides an interface to the client using which the client can access the system.
    /// - The facade design pattern is particularly used when a system is very complex or difficult to understand because system has a large number of 
    ///   interdependent classes or its source code is unavailable.
    /// </summary>
    class FacadePattern
    {
        public FacadePattern()
        {
            List<Customer> customerList = new List<Customer>()
                {
                     new Customer() { CustomerID = 111, CustomerName = "Shaktimaan", CustomerSaving = 0, CustomerCredit = 2500, CustomerLoan = 0 },
                     new Customer() { CustomerID = 112, CustomerName = "Captain Vyom", CustomerSaving = 1000, CustomerCredit = -500, CustomerLoan = 0 },
                     new Customer() { CustomerID = 113, CustomerName = "Robot", CustomerSaving = 1200, CustomerCredit = 1000, CustomerLoan = 120 }
                };

            Mortgage mortgage = new Mortgage();

            foreach (Customer cust in customerList)
            {
                if (mortgage.IsEligible(cust))
                    Console.WriteLine("Customer {0}/{1} is Eligible", cust.CustomerID, cust.CustomerName);
                else
                    Console.WriteLine("Customer {0}/{1} is not Eligible", cust.CustomerID, cust.CustomerName);
            }
        }

        /// <summary>
        /// Customer class
        /// </summary>
        class Customer
        {
            private string _CustomerName;
            private int _CustomerID;
            private double _CustomerCredit;
            private double _CustomerLoan;
            private double _CustomerSaving;

            public Customer()
            {
            }

            public int CustomerID
            {
                get { return _CustomerID; }
                set { _CustomerID = value; }
            }

            public string CustomerName
            {
                get { return _CustomerName; }
                set { _CustomerName = value; }
            }

            public double CustomerLoan
            {
                get { return _CustomerLoan; }
                set { _CustomerLoan = value; }
            }

            public double CustomerCredit
            {
                get { return _CustomerCredit; }
                set { _CustomerCredit = value; }
            }

            public double CustomerSaving
            {
                get { return _CustomerSaving; }
                set { _CustomerSaving = value; }
            }
        }

        /// <summary>
        /// The 'Subsystem ClassA' class
        /// </summary>
        class Bank
        {
            public bool HasEnoughSaving(Customer cust)
            {
                Console.WriteLine("Customer {0} has saving of {1}", cust.CustomerName, cust.CustomerSaving);
                if (cust.CustomerSaving > 500)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// The 'Subsystem ClassB' class
        /// </summary>
        class Credit
        {
            public bool HasGoodCredit(Customer cust)
            {
                Console.WriteLine("Customer {0} has Credit of {1}", cust.CustomerName, cust.CustomerCredit);
                if (cust.CustomerCredit > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// The 'Subsystem ClassC' class
        /// </summary>
        class Loan
        {
            public bool HasGoodLoan(Customer cust)
            {
                Console.WriteLine("Customer {0} has Loan of {1}", cust.CustomerName, cust.CustomerLoan);
                if (cust.CustomerLoan == 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// The 'Facade' class
        /// This pattern involves a single wrapper class which contains a set of members which are required by client. 
        /// These members access the system on behalf of the facade client and hide the implementation details.
        /// </summary>
        class Mortgage
        {
            private Bank _bank = new Bank();
            private Credit _credit = new Credit();
            private Loan _loan = new Loan();
            private bool eligible = false;

            public Mortgage()
            {
            }

            public bool IsEligible(Customer cust)
            {
                eligible = _bank.HasEnoughSaving(cust);
                eligible = _credit.HasGoodCredit(cust) && eligible;
                eligible = _loan.HasGoodLoan(cust) && eligible;

                return eligible;
            }
        }
    }
}
