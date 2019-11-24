namespace CSharp.ProblemSolving
{
	public class StarWars
	{
		public void Run()
		{

		}
	}

	abstract class KingdomBase
	{
		protected readonly int FirePower = 0;
		public KingdomBase(int firepower)
		{
			FirePower = firepower;
		}
	}
	
	class Kingdom : KingdomBase
	{
		private readonly Arsenal Horses;
		private readonly Arsenal Elephants;
		private readonly Arsenal ArmouredTanks;
		private readonly Arsenal SlingGuns;

		public Kingdom(int firePower, int horses, int elephants, int tanks, int slingGuns) : base(firePower)
		{
			Horses = new Arsenal(ArsenalType.Horse, horses);
			Elephants = new Arsenal(ArsenalType.Elephant, elephants);
			ArmouredTanks = new Arsenal(ArsenalType.ArmouredTank, tanks);
			SlingGuns = new Arsenal(ArsenalType.SlingGun, slingGuns);
		}

		public void Attack(Kingdom kingdom)
		{
			//Does nothing right now
		}

		public void Defend(Kingdom enemyKingdom)
		{

		}

		private int GetSlingGuns(Arsenal enemySlingGuns)
		{
			var slingGunPower = Horses.Total * FirePower;
			var requiredSlingGuns = 0;
			if (slingGunPower > enemySlingGuns.Total)
			{
				SlingGuns.Total = SlingGuns.Total - enemySlingGuns.Total;
				requiredSlingGuns = enemySlingGuns.Total;
			}
			else if (slingGunPower < enemySlingGuns.Total)
			{

			}
			else if (slingGunPower == enemySlingGuns.Total)
			{
				SlingGuns.Total = 0;
				requiredSlingGuns = enemySlingGuns.Total;
			}
			return requiredSlingGuns;
		}
	}

	class Arsenal
	{
		public ArsenalType Type { get; set; }
		public int Total { get; set; }

		public Arsenal(ArsenalType type, int total)
		{
			Type = type;
			Total = total;
		}

		public Arsenal GetSubstitution(Arsenal arsenal)
		{
			switch (arsenal.Type)
			{
				case ArsenalType.ArmouredTank:
					return new Arsenal(ArsenalType.SlingGun, arsenal.Total * 2);

				case ArsenalType.SlingGun:
					return new Arsenal(ArsenalType.Elephant, arsenal.Total * 2);

				case ArsenalType.Elephant:
					return new Arsenal(ArsenalType.Horse, arsenal.Total * 2);

				default:
					return arsenal;
			}
		}
	}

	enum ArsenalType
	{
		SlingGun = 1,
		ArmouredTank = 2,
		Elephant = 3,
		Horse = 4
	}
}