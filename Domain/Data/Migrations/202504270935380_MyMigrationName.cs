namespace Domain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyMigrationName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Amount = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropForeignKey("dbo.Cards", "User_Id", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_Id" });
            DropIndex("dbo.Cards", new[] { "UserId" });
            DropTable("dbo.Cards");
        }
    }
}
