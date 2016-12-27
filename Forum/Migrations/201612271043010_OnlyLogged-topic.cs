namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnlyLoggedtopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "OnlyLogged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "OnlyLogged");
        }
    }
}
