namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userrank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rank", c => c.String());
            AddColumn("dbo.AspNetUsers", "PostsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PostsCount");
            DropColumn("dbo.AspNetUsers", "Rank");
        }
    }
}
