namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhtmltable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HtmlMarkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HtmlMarkers");
        }
    }
}
