namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outerkonusbaseplatemaybenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Baseplate", "OuterKonusMax", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "OuterKonusMin", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "OuterKonusAngle", c => c.Decimal(precision: 5, scale: 2));
            AlterColumn("dbo.Baseplate", "KonusHeight", c => c.Decimal(precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Baseplate", "KonusHeight", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "OuterKonusAngle", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Baseplate", "OuterKonusMin", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "OuterKonusMax", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
