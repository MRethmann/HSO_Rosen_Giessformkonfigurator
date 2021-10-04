namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedvererbungoncomponents : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Baseplate", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Core", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.CoreSingleMold", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Cupform", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Ring", "Description", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ring", "Description", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Cupform", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.CoreSingleMold", "Description", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Core", "Description", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Baseplate", "Description", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
