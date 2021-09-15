namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallerfixestodatatypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Core", "Toleranz_Durchmesser_Fuehrung", c => c.String(unicode: false));
            AlterColumn("dbo.Insertplate", "Konus_Außen_Winkel", c => c.Decimal(precision: 5, scale: 2));
            AlterColumn("dbo.Insertplate", "Konus_Innen_Winkel", c => c.Decimal(precision: 5, scale: 2));
            AlterColumn("dbo.Ring", "Konus_Winkel", c => c.Decimal(precision: 5, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ring", "Konus_Winkel", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Insertplate", "Konus_Innen_Winkel", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Insertplate", "Konus_Außen_Winkel", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Core", "Toleranz_Durchmesser_Fuehrung", c => c.Decimal(precision: 10, scale: 2));
        }
    }
}
