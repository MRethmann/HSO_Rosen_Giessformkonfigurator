namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedHcAttributesFromDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductDisc", "BTC", c => c.String(maxLength: 10, unicode: false));
            DropColumn("dbo.ProductDisc", "HcDiameter");
            DropColumn("dbo.ProductDisc", "HcHoles");
            DropColumn("dbo.ProductDisc", "HcHoleDiameter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductDisc", "HcHoleDiameter", c => c.Decimal(precision: 10, scale: 2));
            AddColumn("dbo.ProductDisc", "HcHoles", c => c.Int());
            AddColumn("dbo.ProductDisc", "HcDiameter", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ProductDisc", "BTC", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
        }
    }
}
