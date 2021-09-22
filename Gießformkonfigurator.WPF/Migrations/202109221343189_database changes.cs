namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDisc", "HcDiameter", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "HcHoles", c => c.Int());
            AddColumn("dbo.ProductDisc", "HcHoleDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "Hc1Holes", c => c.Int());
            AlterColumn("dbo.Baseplate", "Hc2Holes", c => c.Int());
            AlterColumn("dbo.Baseplate", "Hc3Holes", c => c.Int());
            AlterColumn("dbo.Insertplate", "Hc1Holes", c => c.Int());
            AlterColumn("dbo.Insertplate", "Hc2Holes", c => c.Int());
            AlterColumn("dbo.Insertplate", "Hc3Holes", c => c.Int());
            DropColumn("dbo.ProductDisc", "Hc1Diameter");
            DropColumn("dbo.ProductDisc", "Hc1Holes");
            DropColumn("dbo.ProductDisc", "Hc1HoleDiameter");
            DropColumn("dbo.ProductDisc", "Hc2Diameter");
            DropColumn("dbo.ProductDisc", "Hc2Holes");
            DropColumn("dbo.ProductDisc", "Hc2HoleDiameter");
            DropColumn("dbo.ProductDisc", "Hc3Diameter");
            DropColumn("dbo.ProductDisc", "Hc3Holes");
            DropColumn("dbo.ProductDisc", "Hc3HoleDiameter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductDisc", "Hc3HoleDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc3Holes", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc3Diameter", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc2HoleDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc2Holes", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc2Diameter", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc1HoleDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc1Holes", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductDisc", "Hc1Diameter", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Insertplate", "Hc3Holes", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Insertplate", "Hc2Holes", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Insertplate", "Hc1Holes", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "Hc3Holes", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "Hc2Holes", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.Baseplate", "Hc1Holes", c => c.Decimal(precision: 10, scale: 2));
            DropColumn("dbo.ProductDisc", "HcHoleDiameter");
            DropColumn("dbo.ProductDisc", "HcHoles");
            DropColumn("dbo.ProductDisc", "HcDiameter");
        }
    }
}
