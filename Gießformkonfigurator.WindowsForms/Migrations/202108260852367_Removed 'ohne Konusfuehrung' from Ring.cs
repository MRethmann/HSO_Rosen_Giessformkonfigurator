namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedohneKonusfuehrungfromRing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ring", "ohne_Konusfuehrung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ring", "ohne_Konusfuehrung", c => c.Boolean(nullable: false));
        }
    }
}
