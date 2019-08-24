using System;

namespace CSharp
{
	public class Tricky
	{
		public void Play()
		{
			//Problem1();
			//Problem2();
		}

		public string Problem1()
		{
			try
			{
				return "a";
			}
			catch (Exception)
			{
				return "b";
			}
			finally
			{
				//Compile time error: controll can not leave the body of Finally clause
				//return "c";
			}
		}

		void Problem2()
		{
			//Design problem

			//let say you have tank that shoot and drive and plane that fly and shoot, after that you have to add new class Truck that can only drive. implement design

			Plane plane = new Plane();
			plane.Fly();
			plane.Shoot(new Tank());

			Tank tank = new Tank();
			tank.Drive();
			tank.Shoot(new Plane());

			Truck truck = new Truck();
			//truck.Shoot(new Plane()); //Truck cant shoot - this is design problem
			truck.Drive();
		}
	}	

	public class Coordinates
	{
		public int Longitude { get; set; }
		public int Lattitude { get; set; }
	}

	public abstract class Machine
	{
		public Coordinates position { get; set; }

		public void Shoot(Machine target)
		{
			//Shoot the target
		}
	}

	public class Plane : Machine
	{
		public void Fly()
		{

		}
	}

	interface IDrive
	{
		void Drive();
	}

	public class Tank : Machine, IDrive
	{
		public void Drive()
		{

		}
	}

	public class Truck : IDrive
	{
		public void Drive()
		{

		}
	}
}
