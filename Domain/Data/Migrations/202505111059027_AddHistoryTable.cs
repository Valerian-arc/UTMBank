namespace Domain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        Destination = c.String(),
                        Amount = c.Double(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoryLogs");
        }
    }
}
