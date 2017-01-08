namespace Forum.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addedforummoderators : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumModerators",
                c => new
                {
                    ForumID = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.ForumID, t.UserId })
                .ForeignKey("dbo.Fora", t => t.ForumID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ForumID)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ForumModerators", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumModerators", "ForumID", "dbo.Fora");
            DropIndex("dbo.ForumModerators", new[] { "UserId" });
            DropIndex("dbo.ForumModerators", new[] { "ForumID" });
            DropTable("dbo.ForumModerators");
        }
    }
}
