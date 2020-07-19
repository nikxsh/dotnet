using System;
using System.Collections.Generic;

namespace ObjectOriented.Patterns
{
    /// <summary>
    /// - Creational Patterns
    /// - Builder pattern builds a complex object by using a step by step approach. Builder interface defines the steps to build the final object. 
    /// - This builder is independent from the objects creation process. A class that is known as Director, controls the object creation process.
    ///   Moreover, builder pattern describes a way to separate an object from its construction.The same construction method can create different 
    ///   representation of the object.
    /// </summary>
    public class BuilderPattern
	{
		public BuilderPattern()
		{
			VehicleBuilder _builder;

			var shop = new Shop();

			_builder = new ScooterBuilder();
			shop.Construct(_builder);
			_builder.Vehicle.show();

			_builder = new MotorCycleBuilder();
			shop.Construct(_builder);
			_builder.Vehicle.show();

			_builder = new CarBuilder();
			shop.Construct(_builder);
			_builder.Vehicle.show();

		}

		/// <summary>
		/// The 'Director' class
		/// </summary>
		class Shop
		{
			public void Construct(VehicleBuilder vehicleBuilder)
			{
				vehicleBuilder.BuildFrame();
				vehicleBuilder.BuildEngine();
				vehicleBuilder.BuildWheels();
				vehicleBuilder.BuildDoors();
			}
		}

		/// <summary>
		/// The 'Builder' abstract class
		/// </summary>
		abstract class VehicleBuilder
		{
			protected Vehicle vehicle;

			// Gets vehicle instance
			public Vehicle Vehicle
			{
				get { return vehicle; }
			}

			// Abstract build methods
			public abstract void BuildFrame();
			public abstract void BuildEngine();
			public abstract void BuildWheels();
			public abstract void BuildDoors();
		}

		/// <summary>
		/// The 'ConcreteBuilder1' class
		/// </summary>
		class MotorCycleBuilder : VehicleBuilder
		{
			public MotorCycleBuilder()
			{
				vehicle = new Vehicle("Motor Cycle");
			}

			public override void BuildFrame()
			{
				vehicle["frame"] = "MotorCycle Frame";
			}

			public override void BuildEngine()
			{
				vehicle["engine"] = "500 cc";
			}

			public override void BuildWheels()
			{
				vehicle["wheels"] = "2";
			}

			public override void BuildDoors()
			{
				vehicle["doors"] = "0";
			}
		}

		/// <summary>
		/// The 'ConcreteBuilder2' class
		/// </summary>
		class CarBuilder : VehicleBuilder
		{
			public CarBuilder()
			{
				vehicle = new Vehicle("Car");
			}

			public override void BuildFrame()
			{
				vehicle["frame"] = "Car Frame";
			}

			public override void BuildEngine()
			{
				vehicle["engine"] = "2500 cc";
			}

			public override void BuildWheels()
			{
				vehicle["wheels"] = "4";
			}

			public override void BuildDoors()
			{
				vehicle["doors"] = "4";
			}
		}

		/// <summary>
		/// The 'ConcreteBuilder3' class
		/// </summary>
		class ScooterBuilder : VehicleBuilder
		{
			public ScooterBuilder()
			{
				vehicle = new Vehicle("Scooter");
			}

			public override void BuildFrame()
			{
				vehicle["frame"] = "Scooter Frame";
			}

			public override void BuildEngine()
			{
				vehicle["engine"] = "50 cc";
			}

			public override void BuildWheels()
			{
				vehicle["wheels"] = "2";
			}

			public override void BuildDoors()
			{
				vehicle["doors"] = "0";
			}
		}


		/// <summary>
		///  The 'Product' class
		/// </summary>
		class Vehicle
		{
			private string _VehicleType;
			private Dictionary<string, string> _parts = new Dictionary<string, string>();

			public Vehicle(string vehicalType)
			{
				_VehicleType = vehicalType;
			}

			public string this[string key]
			{
				get { return _parts[key]; }
				set { _parts[key] = value; }
			}

			public void show()
			{
				Console.WriteLine("\n---------------------------");
				Console.WriteLine("Vehicle Type: {0}", _VehicleType);
				Console.WriteLine(" Frame : {0}", _parts["frame"]);
				Console.WriteLine(" Engine : {0}", _parts["engine"]);
				Console.WriteLine(" #Wheels: {0}", _parts["wheels"]);
				Console.WriteLine(" #Doors : {0}", _parts["doors"]);
			}
		}
	}
}
