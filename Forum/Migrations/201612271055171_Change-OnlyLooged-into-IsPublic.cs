namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOnlyLoogedintoIsPublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "IsPublic", c => c.Boolean(nullable: false));
            DropColumn("dbo.Fora", "OnlyLogged");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fora", "OnlyLogged", c => c.Boolean(nullable: false));
            DropColumn("dbo.Fora", "IsPublic");
        }
    }
}
