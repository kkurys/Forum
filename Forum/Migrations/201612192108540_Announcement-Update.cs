namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnouncementUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcements", "Content");
        }
    }
}
