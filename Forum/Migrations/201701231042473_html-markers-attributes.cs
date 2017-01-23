namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class htmlmarkersattributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HtmlMarkers", "Attribute", c => c.String());
            AddColumn("dbo.HtmlMarkers", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HtmlMarkers", "Value");
            DropColumn("dbo.HtmlMarkers", "Attribute");
        }
    }
}
