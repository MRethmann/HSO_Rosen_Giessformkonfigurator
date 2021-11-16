namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KonusHeightChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insertplate", "OuterKonusHeight", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "InnerKonusHeight", c => c.Decimal(precision: 10, scale: 2));
            DropColumn("dbo.Insertplate", "KonusHeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Insertplate", "KonusHeight", c => c.Decimal(precision: 10, scale: 2));
            DropColumn("dbo.Insertplate", "InnerKonusHeight");
            DropColumn("dbo.Insertplate", "OuterKonusHeight");
        }
    }
}
