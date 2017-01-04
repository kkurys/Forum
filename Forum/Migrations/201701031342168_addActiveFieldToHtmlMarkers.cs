namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addActiveFieldToHtmlMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HtmlMarkers", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HtmlMarkers", "Active");
        }
    }
}
