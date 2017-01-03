namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdropedtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                        PrivateMessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PrivateMessages", t => t.PrivateMessageID, cascadeDelete: true)
                .Index(t => t.PrivateMessageID);
            
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
                "dbo.PrivateThreads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderID = c.String(maxLength: 128),
                        RecipientID = c.String(maxLength: 128),
                        Title = c.String(),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientID)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.RecipientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateThreads", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateThreads", "RecipientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages");
            DropIndex("dbo.PrivateThreads", new[] { "RecipientID" });
            DropIndex("dbo.PrivateThreads", new[] { "SenderID" });
            DropIndex("dbo.MessageFiles", new[] { "PrivateMessageID" });
            DropTable("dbo.PrivateThreads");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.MessageFiles");
        }
    }
}
