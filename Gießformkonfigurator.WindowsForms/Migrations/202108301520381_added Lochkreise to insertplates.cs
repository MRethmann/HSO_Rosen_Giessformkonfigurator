namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLochkreisetoinsertplates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insertplate", "Lk1Durchmesser", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk1Bohrungen", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk1Gewinde", c => c.String());
            AddColumn("dbo.Insertplate", "Lk2Durchmesser", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk2Bohrungen", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk2Gewinde", c => c.String());
            AddColumn("dbo.Insertplate", "Lk3Durchmesser", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk3Bohrungen", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "Lk3Gewinde", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Insertplate", "Lk3Gewinde");
            DropColumn("dbo.Insertplate", "Lk3Bohrungen");
            DropColumn("dbo.Insertplate", "Lk3Durchmesser");
            DropColumn("dbo.Insertplate", "Lk2Gewinde");
            DropColumn("dbo.Insertplate", "Lk2Bohrungen");
            DropColumn("dbo.Insertplate", "Lk2Durchmesser");
            DropColumn("dbo.Insertplate", "Lk1Gewinde");
            DropColumn("dbo.Insertplate", "Lk1Bohrungen");
            DropColumn("dbo.Insertplate", "Lk1Durchmesser");
        }
    }
}
