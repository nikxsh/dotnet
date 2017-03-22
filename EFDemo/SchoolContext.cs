using System.Data.Entity;

namespace DotNetDemos.EFDemo
{
    class SchoolContext : DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
