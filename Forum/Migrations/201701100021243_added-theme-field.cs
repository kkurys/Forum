namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedthemefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Theme", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Theme");
        }
    }
}
