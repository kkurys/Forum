namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dzialaj : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostFiles", "PrivateMessage_ID", "dbo.PrivateMessages");
            DropForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages");
            DropForeignKey("dbo.PrivateThreads", "RecipientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateThreads", "SenderID", "dbo.AspNetUsers");
            DropIndex("dbo.MessageFiles", new[] { "PrivateMessageID" });
            DropIndex("dbo.PostFiles", new[] { "PrivateMessage_ID" });
            DropIndex("dbo.PrivateThreads", new[] { "SenderID" });
            DropIndex("dbo.PrivateThreads", new[] { "RecipientID" });
            //DropColumn("dbo.PostFiles", "PrivateMessage_ID");
            DropTable("dbo.HtmlMarkers");
            DropTable("dbo.MessageFiles");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.PrivateThreads");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PrivateThreads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderID = c.String(maxLength: 128),
                        RecipientID = c.String(maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrivateThreadID = c.Int(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MessageFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                        PrivateMessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HtmlMarkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PostFiles", "PrivateMessage_ID", c => c.Int());
            CreateIndex("dbo.PrivateThreads", "RecipientID");
            CreateIndex("dbo.PrivateThreads", "SenderID");
            CreateIndex("dbo.PostFiles", "PrivateMessage_ID");
            CreateIndex("dbo.MessageFiles", "PrivateMessageID");
            AddForeignKey("dbo.PrivateThreads", "SenderID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateThreads", "RecipientID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostFiles", "PrivateMessage_ID", "dbo.PrivateMessages", "ID");
        }
    }
}
