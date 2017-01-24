namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restart : DbMigration
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
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Language = c.Int(nullable: false),
                        Theme = c.Int(nullable: false),
                        PostsPerPageID = c.Int(),
                        SessionTime = c.Time(nullable: false, precision: 7),
                        AvatarFilename = c.String(),
                        Rank = c.String(),
                        PostsCount = c.Int(nullable: false),
                        OwnRank = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostsPerPages", t => t.PostsPerPageID)
                .Index(t => t.PostsPerPageID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        TopicCount = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        LastPostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fora", t => t.ForumID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ForumID)
                .Index(t => t.UserID);
            
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
                "dbo.PostFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostsPerPages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrivateThreadID = c.Int(nullable: false),
                        AuthorID = c.String(maxLength: 128),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID)
                .ForeignKey("dbo.PrivateThreads", t => t.PrivateThreadID, cascadeDelete: true)
                .Index(t => t.PrivateThreadID)
                .Index(t => t.AuthorID);
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ForbiddenWord = c.String(),
                        IsForbidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HtmlMarkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Attribute = c.String(),
                        Value = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ForumUsers",
                c => new
                    {
                        Forum_ID = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Forum_ID, t.User_Id })
                .ForeignKey("dbo.Fora", t => t.Forum_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Forum_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Announcements", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateThreads", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateThreads", "RecipientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads");
            DropForeignKey("dbo.MessageFiles", "PrivateMessageID", "dbo.PrivateMessages");
            DropForeignKey("dbo.PrivateMessages", "AuthorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PostsPerPageID", "dbo.PostsPerPages");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Topics", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.PostFiles", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Topics", "ForumID", "dbo.Fora");
            DropForeignKey("dbo.ForumUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumUsers", "Forum_ID", "dbo.Fora");
            DropForeignKey("dbo.Fora", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ForumUsers", new[] { "User_Id" });
            DropIndex("dbo.ForumUsers", new[] { "Forum_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PrivateThreads", new[] { "RecipientID" });
            DropIndex("dbo.PrivateThreads", new[] { "SenderID" });
            DropIndex("dbo.MessageFiles", new[] { "PrivateMessageID" });
            DropIndex("dbo.PrivateMessages", new[] { "AuthorID" });
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThreadID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.PostFiles", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "TopicID" });
            DropIndex("dbo.Topics", new[] { "UserID" });
            DropIndex("dbo.Topics", new[] { "ForumID" });
            DropIndex("dbo.Fora", new[] { "CategoryID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PostsPerPageID" });
            DropIndex("dbo.Announcements", new[] { "UserID" });
            DropTable("dbo.ForumUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.HtmlMarkers");
            DropTable("dbo.Dictionaries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PrivateThreads");
            DropTable("dbo.MessageFiles");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.PostsPerPages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.PostFiles");
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
            DropTable("dbo.Categories");
            DropTable("dbo.Fora");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Announcements");
        }
    }
}
