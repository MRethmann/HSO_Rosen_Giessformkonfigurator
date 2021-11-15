namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMultipleAttributes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Baseplate", "HasHoleguide");
            DropColumn("dbo.Baseplate", "InnerDiameter");
            DropColumn("dbo.Baseplate", "HasCore");
            DropColumn("dbo.Core", "HasGuideBolt");
            DropColumn("dbo.Core", "GuideHeight");
            DropColumn("dbo.Core", "GuideDiameter");
            DropColumn("dbo.Cupform", "HasHoleguide");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cupform", "HasHoleguide", c => c.Boolean(nullable: false));
            AddColumn("dbo.Core", "GuideDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.Core", "GuideHeight", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.Core", "HasGuideBolt", c => c.Boolean(nullable: false));
            AddColumn("dbo.Baseplate", "HasCore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Baseplate", "InnerDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.Baseplate", "HasHoleguide", c => c.Boolean(nullable: false));
        }
    }
}
