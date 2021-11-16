namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KonusAttributesOnCupform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cupform", "InnerKonusHeight", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Cupform", "InnerKonusMax", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Cupform", "InnerKonusMin", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Cupform", "InnerKonusAngle", c => c.Decimal(precision: 5, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cupform", "InnerKonusAngle", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Cupform", "InnerKonusMin", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Cupform", "InnerKonusMax", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Cupform", "InnerKonusHeight");
        }
    }
}
