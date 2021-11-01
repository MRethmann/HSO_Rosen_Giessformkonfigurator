namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBTCToCupform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cupform", "BTC1", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC1Thread", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC2", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC2Thread", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC3", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Cupform", "BTC3Thread", c => c.String(maxLength: 10, unicode: false));
            DropColumn("dbo.Cupform", "BTC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cupform", "BTC", c => c.String(maxLength: 10, unicode: false));
            DropColumn("dbo.Cupform", "BTC3Thread");
            DropColumn("dbo.Cupform", "BTC3");
            DropColumn("dbo.Cupform", "BTC2Thread");
            DropColumn("dbo.Cupform", "BTC2");
            DropColumn("dbo.Cupform", "BTC1Thread");
            DropColumn("dbo.Cupform", "BTC1");
        }
    }
}
