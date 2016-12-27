namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postsperpagefix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "PostsPerPage_ID1", newName: "PostsPerPageID");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_PostsPerPage_ID1", newName: "IX_PostsPerPageID");
            DropColumn("dbo.AspNetUsers", "PostsPerPage_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PostsPerPage_ID", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_PostsPerPageID", newName: "IX_PostsPerPage_ID1");
            RenameColumn(table: "dbo.AspNetUsers", name: "PostsPerPageID", newName: "PostsPerPage_ID1");
        }
    }
}
