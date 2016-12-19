namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        TopicCount = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
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
                "dbo.PostFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                        PostID = c.Int(nullable: false),
                        PrivateMessage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.PrivateMessages", t => t.PrivateMessage_ID)
                .Index(t => t.PostID)
                .Index(t => t.PrivateMessage_ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TopicID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.TopicID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ForumID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        IsGlued = c.Boolean(nullable: false),
                        PostCount = c.Int(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fora", t => t.ForumID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ForumID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PrivateThreads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderID = c.String(maxLength: 128),
                        RecipientID = c.String(maxLength: 128),
                        Title = c.String(),
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
            DropForeignKey("dbo.PostFiles", "PrivateMessage_ID", "dbo.PrivateMessages");
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Topics", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Topics", "ForumID", "dbo.Fora");
            DropForeignKey("dbo.PostFiles", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Fora", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Announcements", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateThreads", new[] { "RecipientID" });
            DropIndex("dbo.PrivateThreads", new[] { "SenderID" });
            DropIndex("dbo.Topics", new[] { "UserID" });
            DropIndex("dbo.Topics", new[] { "ForumID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "TopicID" });
            DropIndex("dbo.PostFiles", new[] { "PrivateMessage_ID" });
            DropIndex("dbo.PostFiles", new[] { "PostID" });
            DropIndex("dbo.MessageFiles", new[] { "PrivateMessageID" });
            DropIndex("dbo.Fora", new[] { "CategoryID" });
            DropIndex("dbo.Announcements", new[] { "UserID" });
            DropTable("dbo.PrivateThreads");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
            DropTable("dbo.PostFiles");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.MessageFiles");
            DropTable("dbo.Fora");
            DropTable("dbo.Categories");
            DropTable("dbo.Announcements");
        }
    }
}
