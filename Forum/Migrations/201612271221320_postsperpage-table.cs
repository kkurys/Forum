namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postsperpagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostsPerPages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "PostsPerPage_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PostsPerPage_ID");
            AddForeignKey("dbo.AspNetUsers", "PostsPerPage_ID", "dbo.PostsPerPages", "ID");
            DropColumn("dbo.AspNetUsers", "PostsPerPage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PostsPerPage", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "PostsPerPage_ID", "dbo.PostsPerPages");
            DropIndex("dbo.AspNetUsers", new[] { "PostsPerPage_ID" });
            DropColumn("dbo.AspNetUsers", "PostsPerPage_ID");
            DropTable("dbo.PostsPerPages");
        }
    }
}
