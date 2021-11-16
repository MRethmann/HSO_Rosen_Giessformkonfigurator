namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baseplate", "BTC1", c => c.String(unicode: false));
            AddColumn("dbo.Baseplate", "BTC1Thread", c => c.String(unicode: false));
            AddColumn("dbo.Baseplate", "BTC2", c => c.String(unicode: false));
            AddColumn("dbo.Baseplate", "BTC2Thread", c => c.String(unicode: false));
            AddColumn("dbo.Baseplate", "BTC3", c => c.String(unicode: false));
            AddColumn("dbo.Baseplate", "BTC3Thread", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC1", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC1Thread", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC2", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC2Thread", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC3", c => c.String(unicode: false));
            AddColumn("dbo.Insertplate", "BTC3Thread", c => c.String(unicode: false));
            DropColumn("dbo.Insertplate", "HasHoleguide");
            DropColumn("dbo.Insertplate", "InnerDiameter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Insertplate", "InnerDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.Insertplate", "HasHoleguide", c => c.Boolean(nullable: false));
            DropColumn("dbo.Insertplate", "BTC3Thread");
            DropColumn("dbo.Insertplate", "BTC3");
            DropColumn("dbo.Insertplate", "BTC2Thread");
            DropColumn("dbo.Insertplate", "BTC2");
            DropColumn("dbo.Insertplate", "BTC1Thread");
            DropColumn("dbo.Insertplate", "BTC1");
            DropColumn("dbo.Baseplate", "BTC3Thread");
            DropColumn("dbo.Baseplate", "BTC3");
            DropColumn("dbo.Baseplate", "BTC2Thread");
            DropColumn("dbo.Baseplate", "BTC2");
            DropColumn("dbo.Baseplate", "BTC1Thread");
            DropColumn("dbo.Baseplate", "BTC1");
        }
    }
}
