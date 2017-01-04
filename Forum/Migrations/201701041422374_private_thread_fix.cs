namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class private_thread_fix : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PrivateMessages", "PrivateThreadID");
            AddForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "PrivateThreadID", "dbo.PrivateThreads");
            DropIndex("dbo.PrivateMessages", new[] { "PrivateThreadID" });
        }
    }
}
