namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ownrankfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OwnRank", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OwnRank");
        }
    }
}
