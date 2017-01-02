namespace Forum.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class postsperpagefix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "PostsPerPage_ID", newName: "PostsPerPageID");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_PostsPerPage_ID", newName: "IX_PostsPerPageID");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_PostsPerPageID", newName: "IX_PostsPerPage_ID");
            RenameColumn(table: "dbo.AspNetUsers", name: "PostsPerPageID", newName: "PostsPerPage_ID");
        }
    }
}
