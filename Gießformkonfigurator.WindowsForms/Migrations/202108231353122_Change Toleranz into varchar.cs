namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToleranzintovarchar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ring", "Toleranz_Außendurchmesser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ring", "Toleranz_Außendurchmesser", c => c.Decimal(precision: 10, scale: 2));
        }
    }
}
