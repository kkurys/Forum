namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnlyLoggedForumfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "OnlyLogged", c => c.Boolean(nullable: false));
            DropColumn("dbo.Topics", "OnlyLogged");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "OnlyLogged", c => c.Boolean(nullable: false));
            DropColumn("dbo.Fora", "OnlyLogged");
        }
    }
}
