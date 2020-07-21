using ObjectOriented.BehavioralPatterns;
using ObjectOriented.CreationalPatterns;
using ObjectOriented.StructuralPatterns;

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
            //Behavioral Patterns
            new ObserverPattern();
            new CommandPattern();

            new Oops();
        }
    }
}
