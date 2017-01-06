namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class privatemsgfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThreadID" });
            AddColumn("dbo.PrivateMessages", "AuthorID", c => c.Int(nullable: false));
            AddColumn("dbo.PrivateMessages", "Author_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PrivateMessages", "PrivateThread_ID", c => c.Int());
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.String());
            CreateIndex("dbo.PrivateMessages", "Author_Id");
            CreateIndex("dbo.PrivateMessages", "PrivateThread_ID");
            AddForeignKey("dbo.PrivateMessages", "Author_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateMessages", "PrivateThread_ID", "dbo.PrivateThreads", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateThread_ID", "dbo.PrivateThreads");
            DropForeignKey("dbo.PrivateMessages", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThread_ID" });
            DropIndex("dbo.PrivateMessages", new[] { "Author_Id" });
            AlterColumn("dbo.PrivateMessages", "PrivateThreadID", c => c.Int(nullable: false));
            DropColumn("dbo.PrivateMessages", "PrivateThread_ID");
            DropColumn("dbo.PrivateMessages", "Author_Id");
            DropColumn("dbo.PrivateMessages", "AuthorID");
            CreateIndex("dbo.PrivateMessages", "PrivateThreadID");
            AddForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads", "ID", cascadeDelete: true);
        }
    }
}
