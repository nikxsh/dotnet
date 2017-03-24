using System.Data.Entity;

namespace EntityFrameworkDemo
{
    class ForumContext : DbContext
    {
        public ForumContext() : base("ForumContextConnection")
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Reply { get; set; }
    }
}
