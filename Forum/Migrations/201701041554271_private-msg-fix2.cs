namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class privatemsgfix2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PrivateMessages", new[] { "Author_Id" });
            DropColumn("dbo.PrivateMessages", "AuthorID");
            RenameColumn(table: "dbo.PrivateMessages", name: "Author_Id", newName: "AuthorID");
            AlterColumn("dbo.PrivateMessages", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.PrivateMessages", "AuthorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PrivateMessages", new[] { "AuthorID" });
            AlterColumn("dbo.PrivateMessages", "AuthorID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PrivateMessages", name: "AuthorID", newName: "Author_Id");
            AddColumn("dbo.PrivateMessages", "AuthorID", c => c.Int(nullable: false));
            CreateIndex("dbo.PrivateMessages", "Author_Id");
        }
    }
}
