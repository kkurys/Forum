namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dictionarybugfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dictionaries", "ForbiddenWord", c => c.String());
            DropColumn("dbo.Dictionaries", "Word");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dictionaries", "Word", c => c.String());
            DropColumn("dbo.Dictionaries", "ForbiddenWord");
        }
    }
}
