namespace EntityFrameworkDemo.EFDemoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EFDemoMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reply",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Body = c.String(),
                        Created = c.DateTime(nullable: false),
                        TopicId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topic", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reply", "TopicId", "dbo.Topic");
            DropIndex("dbo.Reply", new[] { "TopicId" });
            DropTable("dbo.Topic");
            DropTable("dbo.Reply");
        }
    }
}
