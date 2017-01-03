namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ihavenoideawhatimdoing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostFiles", "PrivateMessage_ID", "dbo.PrivateMessages");
            DropIndex("dbo.PostFiles", new[] { "PrivateMessage_ID" });
            DropColumn("dbo.MessageFiles", "PrivateMessageID");
            RenameColumn(table: "dbo.MessageFiles", name: "PrivateMessage_ID", newName: "PrivateMessageID");
            AddColumn("dbo.PrivateThreads", "Seen", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages", "ID", cascadeDelete: true);
            DropColumn("dbo.PostFiles", "PrivateMessage_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostFiles", "PrivateMessage_ID", c => c.Int());
            DropForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages");
            DropColumn("dbo.PrivateThreads", "Seen");
            RenameColumn(table: "dbo.MessageFiles", name: "PrivateMessageID", newName: "PrivateMessage_ID");
            AddColumn("dbo.MessageFiles", "PrivateMessageID", c => c.Int(nullable: false));
            CreateIndex("dbo.PostFiles", "PrivateMessage_ID");
            AddForeignKey("dbo.PostFiles", "PrivateMessage_ID", "dbo.PrivateMessages", "ID");
        }
    }
}
