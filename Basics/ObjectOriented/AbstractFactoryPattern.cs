using System;

namespace ObjectOriented.Patterns
{
    /// <summary>
    /// - Creational Patterns
    /// - Creates an instance of several families of classes
    /// - Provide an interface for creating families of related or dependent objects without specifying their concrete.
    /// - Abstract Factory patterns acts a super-factory which creates other factories. This pattern is also called as Factory of factories.
    /// - In Abstract Factory pattern an interface is responsible for creating a set of related objects, or dependent objects without specifying 
    ///   their concrete classes.
    /// </summary>
    class AbstractFactoryPattern
    {

        public AbstractFactoryPattern()
        {
            var asia = new AsianFactory();
            var world = new AnimalWorld(asia);
            world.RunFoodChain();

            Console.WriteLine("");

            var africa = new AfricanFactory();
            world = new AnimalWorld(africa);
            world.RunFoodChain();
        }

        /// <summary>
        /// AbstractProductA
        /// </summary>
        interface IHerbivore
        {
            void Eat();
        }

        /// <summary>
        /// AbstractProductB
        /// </summary>
        interface ICarnivore
        {
            void Eat(IHerbivore food);
        }

        /// <summary>
        /// AbstractFactory
        /// </summary>
        interface IContinentFactory
        {
            IHerbivore CreateHerbivore();
            ICarnivore CreateCarnivore();
        }

        /// <summary>
        /// ConcreteFactory1
        /// </summary>
        class AsianFactory : IContinentFactory
        {
            public ICarnivore CreateCarnivore()
            {
                return new Wolf();
            }

            public IHerbivore CreateHerbivore()
            {
                return new Bison();
            }
        }

        /// <summary>
        /// ConcreteFactory2
        /// </summary>
        class AfricanFactory : IContinentFactory
        {
            public ICarnivore CreateCarnivore()
            {
                return new Lion();
            }

            public IHerbivore CreateHerbivore()
            {
                return new WildBeast();
            }
        }

        /// <summary>
        /// AbstractProductA's ProductA1
        /// </summary>
        class WildBeast : IHerbivore
        {
            public void Eat()
            {
                Console.WriteLine(this.GetType().Name + " eats vegan");
            }
        }

        /// <summary>
        /// AbstractProductB's ProductB1
        /// </summary>
        class Lion : ICarnivore
        {
            public void Eat(IHerbivore food)
            {
                Console.WriteLine(this.GetType().Name + " eats " + food.GetType().Name);
            }
        }

        /// <summary>
        /// AbstractProductA's ProductA2
        /// </summary>
        class Bison : IHerbivore
        {
            public void Eat()
            {
                Console.WriteLine(this.GetType().Name + " eats vegan");
            }
        }

        /// <summary>
        /// AbstractProductB's ProductB2
        /// </summary>
        class Wolf : ICarnivore
        {
            public void Eat(IHerbivore food)
            {
                Console.WriteLine(this.GetType().Name + " eats " + food.GetType().Name);
            }

        }

        /// <summary>
        /// Client
        /// </summary>
        class AnimalWorld
        {
            private IHerbivore _herbivore;
            private ICarnivore _carnivore;

            public AnimalWorld(IContinentFactory factory)
            {
                _herbivore = factory.CreateHerbivore();
                _carnivore = factory.CreateCarnivore();
            }

            public void RunFoodChain()
            {
                _herbivore.Eat();
                _carnivore.Eat(_herbivore);
            }
        }
    }
}
