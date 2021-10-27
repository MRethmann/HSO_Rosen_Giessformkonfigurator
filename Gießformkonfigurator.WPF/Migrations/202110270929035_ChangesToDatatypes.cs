namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToDatatypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCup", "Size", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Cupform", "Size", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.ProductCup", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.ProductCup", "BTC", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.ProductDisc", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.ProductDisc", "BTC", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductDisc", "BTC", c => c.String(maxLength: 128, fixedLength: true, unicode: false));
            AlterColumn("dbo.ProductDisc", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.ProductCup", "BTC", c => c.String(maxLength: 128, fixedLength: true, unicode: false));
            AlterColumn("dbo.ProductCup", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Cupform", "Size", c => c.String(maxLength: 10, unicode: false));
            DropColumn("dbo.ProductCup", "Size");
        }
    }
}
