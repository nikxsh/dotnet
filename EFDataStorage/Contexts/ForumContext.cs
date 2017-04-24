using EFDataStorage.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EFDataStorage
{
    class ForumContext : DbContext
    {
        public ForumContext() : base("DBContextConnection")
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Reply { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
