namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cupform", "Size", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "HasFixedBTC", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cupform", "Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Baseplate", "Hc1Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Baseplate", "Hc2Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Baseplate", "Hc3Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Bolt", "OuterDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Bolt", "Thread", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Core", "FillHeightMax", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Core", "ToleranceGuideDiameter", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Cupform", "CupType", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Insertplate", "ToleranceOuterDiameter", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Insertplate", "ToleranceInnerDiameter", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Insertplate", "Hc1Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Insertplate", "Hc2Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Insertplate", "Hc3Thread", c => c.String(maxLength: 10));
            AlterColumn("dbo.Ring", "ToleranceOuterDiameter", c => c.String(maxLength: 10, unicode: false));
            DropColumn("dbo.Cupform", "HoleCircle");
            DropColumn("dbo.Cupform", "HasCore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cupform", "HasCore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cupform", "HoleCircle", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Ring", "ToleranceOuterDiameter", c => c.String(unicode: false));
            AlterColumn("dbo.Insertplate", "Hc3Thread", c => c.String());
            AlterColumn("dbo.Insertplate", "Hc2Thread", c => c.String());
            AlterColumn("dbo.Insertplate", "Hc1Thread", c => c.String());
            AlterColumn("dbo.Insertplate", "ToleranceInnerDiameter", c => c.String(maxLength: 5, unicode: false));
            AlterColumn("dbo.Insertplate", "ToleranceOuterDiameter", c => c.String(maxLength: 5, unicode: false));
            AlterColumn("dbo.Cupform", "CupType", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.Core", "ToleranceGuideDiameter", c => c.String(unicode: false));
            AlterColumn("dbo.Core", "FillHeightMax", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Bolt", "Thread", c => c.String(unicode: false));
            AlterColumn("dbo.Bolt", "OuterDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "Hc3Thread", c => c.String());
            AlterColumn("dbo.Baseplate", "Hc2Thread", c => c.String());
            AlterColumn("dbo.Baseplate", "Hc1Thread", c => c.String());
            DropColumn("dbo.Cupform", "Thread");
            DropColumn("dbo.Cupform", "HasFixedBTC");
            DropColumn("dbo.Cupform", "BTC");
            DropColumn("dbo.Cupform", "Size");
        }
    }
}
