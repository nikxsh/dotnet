using System;
using System.Collections.Generic;

namespace ObjectOriented.Patterns
{
    /// <summary>
    /// - Behavioral Pattern
    /// - A way of notifying change to a number of classes
    /// - Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
    /// - The Observer pattern is used when the change of a state in one object must be reflected in another object without keeping the objects tightly coupled.
    /// </summary>
    class ObserverPattern
    {
        public ObserverPattern()
        {
            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Observer("Daddy yanky"));
            ibm.Attach(new Observer("Sean Paul"));
            ibm.Attach(new Observer("Nikhilesh shinde"));

            ibm.Price = 120.11;
            ibm.Price = 120.01;
            ibm.Price = 120.61;
        }

        /// <summary>
        /// The 'Observer' interface
        /// </summary>
        public interface IInvestor
        {
            void Update(Stock stock);
        }

        /// <summary>
        /// The 'ConcreteObserver' class
        /// </summary>
        public class Observer : IInvestor
        {
            private readonly string _name;

            public Stock Stock { get; set; }

            public Observer(string name)
            {
                _name = name;
            }

            public void Update(Stock stock)
            {
                Console.WriteLine("Notified {0} of {1}'s " + "change to {2:C}", _name, stock.Symbol, stock.Price);
            }
        }

        /// <summary>
        /// The 'Subject' abstract class  
        /// </summary>
        public abstract class Stock
        {
            private double _price;

            private readonly List<IInvestor> investorList = new List<IInvestor>();

            public Stock(string symbol, double price)
            {
                Symbol = symbol;
                _price = price;
            }

            public void Attach(IInvestor investor)
            {
                investorList.Add(investor);
            }

            public void Detach(IInvestor investor)
            {
                investorList.Remove(investor);
            }

            public void Notify()
            {
                foreach (IInvestor o in investorList)
                {
                    o.Update(this);
                }
            }

            public string Symbol { get; }

            public double Price
            {
                get { return _price; }
                set
                {
                    if (_price != value)
                    {
                        _price = value;
                        Notify();
                    }
                }
            }
        }


        /// <summary>
        /// The 'ConcreteSubject' class
        /// </summary>
        public class IBM : Stock
        {
            public IBM(string symbol, double price)
                 : base(symbol, price)
            { }
        }

    }
}
