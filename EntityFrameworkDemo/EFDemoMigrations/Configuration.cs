namespace EntityFrameworkDemo.EFDemoMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkDemo.ForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"EFDemoMigrations";
        }

        protected override void Seed(EntityFrameworkDemo.ForumContext context)
        {
            context.Users.AddOrUpdate(
                            n => n.Id,
                            new User
                            {
                                Id = Guid.NewGuid(),
                                UserName = "Nik",
                                FirstName = "Nikhilesh",
                                LastName = "Shinde",
                                Email = "shinde.nikhilesh@gmail.com",
                                Dob =  DateTime.ParseExact("14/03/1990","dd/MM/yyyy", null)
        },
                            new User
                            {
                                Id = Guid.NewGuid(),
                                UserName = "ravi",
                                FirstName = "Ravi",
                                LastName = "Singh",
                                Email = "ravi.singh@gmail.com",
                                Dob = DateTime.ParseExact("07/07/1988", "dd/MM/yyyy", null)
                            },
                            new User
                            {
                                Id = Guid.NewGuid(),
                                UserName = "nehu",
                                FirstName = "Neha",
                                LastName = "Jain",
                                Email = "neha.jain@gmail.com",
                                Dob = DateTime.ParseExact("11/08/1990", "dd/MM/yyyy", null)
                            },
                            new User
                            {
                                Id = Guid.NewGuid(),
                                UserName = "Nikki",
                                FirstName = "Nikita",
                                LastName = "khatri",
                                Email = "nikita.khatri@gmail.com",
                                Dob = DateTime.ParseExact("04/01/1990", "dd/MM/yyyy", null)
                            }
                            );
        }
    }
}
