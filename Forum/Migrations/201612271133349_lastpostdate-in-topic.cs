namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastpostdateintopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "LastPostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "LastPostDate");
        }
    }
}
