using System;

namespace PlayGround
{
	class DesignProblems
	{
		public void Play()
		{

		}

		private void DesignProblem()
		{
			//let say you have tank that shoot and drive and plane that fly and shoot, after that you 
			//have to add new class Truck that can only drive. implement design

			Plane plane = new Plane();
			plane.Fly();
			plane.Shoot(new Tank());

			Tank tank = new Tank();
			tank.Drive();
			tank.Shoot(new Plane());

			Truck truck = new Truck();
			truck.Drive();
		}
	}

	#region Design problem Classes
	public class GPS
	{
		public int Longitude { get; set; }
		public int Lattitude { get; set; }
	}

	public abstract class Machine
	{
		public GPS Location { get; set; }
		public double FuelCapacity { get; set; }
		public abstract void StartEngine();
	}

	public interface ICombat
	{
		void Shoot(Machine machine);
	}

	public class Plane : Machine, ICombat
	{
		public void Fly()
		{

		}

		public void Shoot(Machine machine)
		{
			throw new NotImplementedException();
		}

		public override void StartEngine()
		{
			throw new NotImplementedException();
		}
	}

	public class Tank : Machine, ICombat
	{
		public void Drive()
		{

		}

		public void Shoot(Machine machine)
		{
			throw new NotImplementedException();
		}

		public override void StartEngine()
		{
			throw new NotImplementedException();
		}
	}

	public class Truck : Machine
	{
		public void Drive()
		{

		}

		public override void StartEngine()
		{
			throw new NotImplementedException();
		}
	}
	#endregion

}