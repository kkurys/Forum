namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddictionary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                        IsForbidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dictionaries");
        }
    }
}
