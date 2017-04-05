using EFDataStorage.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EFDataStorage
{
    class ForumContext : DbContext
    {
        public ForumContext() : base("ForumContextConnection")
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Reply { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
