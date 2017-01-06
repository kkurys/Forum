namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class privatemsgfix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateThread_ID", "dbo.PrivateThreads");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThread_ID" });
            DropColumn("dbo.PrivateMessages", "PrivateThreadID");
            RenameColumn(table: "dbo.PrivateMessages", name: "PrivateThread_ID", newName: "PrivateThreadID");
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.Int(nullable: false));
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.Int(nullable: false));
            CreateIndex("dbo.PrivateMessages", "PrivateThreadID");
            AddForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThreadID" });
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.Int());
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.String());
            RenameColumn(table: "dbo.PrivateMessages", name: "PrivateThreadID", newName: "PrivateThread_ID");
            AddColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.String());
            CreateIndex("dbo.PrivateMessages", "PrivateThread_ID");
            AddForeignKey("dbo.PrivateMessages", "PrivateThread_ID", "dbo.PrivateThreads", "ID");
        }
    }
}
