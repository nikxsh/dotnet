using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WineryStore.Persistence.Datastore
{
	public class WineryContext : DbContext
	{
		public DbSet<Winery> Wineries { get; set; }
		public DbSet<Wine> Wines { get; set; }

		public WineryContext(DbContextOptions<WineryContext> dbContextOptions) : base(dbContextOptions)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			//optionsBuilder.UseSqlServer(@"Server=(local);Database=Test;Integrated Security=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Winery>()
				.ToTable("Wineries");

			modelBuilder.Entity<Winery>()
				.HasKey(w => w.Id);

			modelBuilder.Entity<Wine>()
				.ToTable("Wines");

			modelBuilder.Entity<Wine>()
				.HasKey(w => w.Id);

			modelBuilder.Entity<Wine>()
				.HasOne(wine => wine.WineryRelation)
				.WithMany(winery => winery.Wines)
				.HasForeignKey(w => w.WineryId);

			modelBuilder.Entity<Wine>()
				.Property(x => x.Color)
				.HasConversion<int>();

			//Seed Mock Data
			modelBuilder.Entity<Winery>().HasData(
				MockWineryData.Wineries
				.Select(x => new Winery
				{
					Id = x.Id,
					Name = x.Name,
					Region = x.Region,
					Country = x.Country
				}).ToArray());

			modelBuilder.Entity<Wine>().HasData(
				MockWineryData.Wines
				.Select(x => new Wine
				{
					Id = x.Id,
					Name = x.Name,
					Color = (WineColor)x.Color,
					Vintage = x.Vintage,
					Score = x.Score,
					Price = x.Price,
					IssueDate = x.IssueDate,
					Rank = x.Rank,
					Note = x.Note,
					WineryId = x.WineryId
				}).ToArray());

			//https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/index
			//1. Install-Package Microsoft.EntityFrameworkCore.Design -Version 2.1.1
			//2. dotnet ef migrations add InitialWineryCreate -s ..\WineryStore.API  (-c contextName)
			//3. dotnet ef database update -s ..\WineryStore.API
			//4. dotnet ef migrations remove -s ..\WineryStore.API

			base.OnModelCreating(modelBuilder);
		}

		public class Winery
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Region { get; set; }
			public string Country { get; set; }

			//one-to-many relationship
			public List<Wine> Wines { get; set; }
		}

		public class Wine
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public WineColor Color { get; set; }
			public string Vintage { get; set; }
			public int Score { get; set; }
			public int Price { get; set; }
			public string IssueDate { get; set; }
			public int Rank { get; set; }
			public string Note { get; set; }

			//It is recommended to have a foreign key property defined in the dependent entity class, it is not required. 
			//If no foreign key property is found, a shadow foreign key property will be introduced with the name 
			// <navigation property name><principal key property name>
			public Guid WineryId { get; set; }
			//[ForeignKey("WineryForeignKey")]
			public Winery WineryRelation { get; set; }
		}

		public enum WineColor
		{
			Red,
			White,
			Rose,
			Champagne,
			Dessert,
			Sparkling
		}
	}
}