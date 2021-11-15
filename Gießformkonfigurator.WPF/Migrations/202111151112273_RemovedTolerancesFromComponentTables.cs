namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTolerancesFromComponentTables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Baseplate", "ToleranceInnerDiameter");
            DropColumn("dbo.Core", "ToleranceOuterDiameter");
            DropColumn("dbo.Core", "ToleranceGuideDiameter");
            DropColumn("dbo.Cupform", "ToleranceInnerDiameter");
            DropColumn("dbo.Insertplate", "ToleranceOuterDiameter");
            DropColumn("dbo.Insertplate", "ToleranceInnerDiameter");
            DropColumn("dbo.Ring", "ToleranceOuterDiameter");
            DropColumn("dbo.Ring", "ToleranceInnerDiameter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ring", "ToleranceInnerDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Ring", "ToleranceOuterDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Insertplate", "ToleranceInnerDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Insertplate", "ToleranceOuterDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "ToleranceInnerDiameter", c => c.String(maxLength: 10));
            AddColumn("dbo.Core", "ToleranceGuideDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Core", "ToleranceOuterDiameter", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Baseplate", "ToleranceInnerDiameter", c => c.String(maxLength: 10, unicode: false));
        }
    }
}
