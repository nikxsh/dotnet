using ObjectOriented.Patterns;

namespace ObjectOriented
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creational Patterns
            new FactoryPattern();
            new AbstractFactoryPattern();
            new BuilderPattern();
            new SingletonPattern();
            //Structural Patterns
            new AdapterPattern();
            new CompositePattern();
            new DecoratorPattern();
            new FacadePattern();
            //Behavioral Pattern
            new ObserverPattern();
            new CommandPattern();
        }
    }
}
