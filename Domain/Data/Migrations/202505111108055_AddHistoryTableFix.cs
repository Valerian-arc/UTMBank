namespace Domain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistoryTableFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryLogUsers",
                c => new
                    {
                        HistoryLogId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryLogId, t.UserId })
                .ForeignKey("dbo.HistoryLogs", t => t.HistoryLogId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.HistoryLogId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoryLogUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.HistoryLogUsers", "HistoryLogId", "dbo.HistoryLogs");
            DropIndex("dbo.HistoryLogUsers", new[] { "UserId" });
            DropIndex("dbo.HistoryLogUsers", new[] { "HistoryLogId" });
            DropTable("dbo.HistoryLogUsers");
        }
    }
}
