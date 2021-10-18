namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class holePairAngleDatatypechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BoltCircleType", "HolepairAngle", c => c.Decimal(precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BoltCircleType", "HolepairAngle", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
