namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductchangesAddedSingleMoldTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCup", "Description", c => c.String(unicode: false));
            AddColumn("dbo.ProductCup", "FactorPU", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductCup", "BTC", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
            AddColumn("dbo.ProductDisc", "Description", c => c.String(unicode: false));
            AddColumn("dbo.ProductDisc", "FactorPU", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductDisc", "BTC", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
            AlterColumn("dbo.ProductCup", "BaseCup", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.ProductCup", "HoleCircle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCup", "HoleCircle", c => c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false));
            AlterColumn("dbo.ProductCup", "BaseCup", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.ProductDisc", "BTC");
            DropColumn("dbo.ProductDisc", "FactorPU");
            DropColumn("dbo.ProductDisc", "Description");
            DropColumn("dbo.ProductCup", "BTC");
            DropColumn("dbo.ProductCup", "FactorPU");
            DropColumn("dbo.ProductCup", "Description");
        }
    }
}
