namespace Domain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistoryTableFixFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoryLogUsers", "HistoryLogId", "dbo.HistoryLogs");
            DropForeignKey("dbo.HistoryLogUsers", "UserId", "dbo.Users");
            DropIndex("dbo.HistoryLogUsers", new[] { "HistoryLogId" });
            DropIndex("dbo.HistoryLogUsers", new[] { "UserId" });
            AddColumn("dbo.HistoryLogs", "SourceUser", c => c.Int(nullable: false));
            AddColumn("dbo.HistoryLogs", "DestinationUser", c => c.Int(nullable: false));
            DropTable("dbo.HistoryLogUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HistoryLogUsers",
                c => new
                    {
                        HistoryLogId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryLogId, t.UserId });
            
            DropColumn("dbo.HistoryLogs", "DestinationUser");
            DropColumn("dbo.HistoryLogs", "SourceUser");
            CreateIndex("dbo.HistoryLogUsers", "UserId");
            CreateIndex("dbo.HistoryLogUsers", "HistoryLogId");
            AddForeignKey("dbo.HistoryLogUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HistoryLogUsers", "HistoryLogId", "dbo.HistoryLogs", "Id", cascadeDelete: true);
        }
    }
}
