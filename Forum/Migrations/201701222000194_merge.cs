namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.ForumUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumUsers", "Forum_ID", "dbo.Fora");
            DropIndex("dbo.ForumUsers", new[] { "User_Id" });
            DropIndex("dbo.ForumUsers", new[] { "Forum_ID" });
            DropTable("dbo.ForumUsers");
        }
    }
}
