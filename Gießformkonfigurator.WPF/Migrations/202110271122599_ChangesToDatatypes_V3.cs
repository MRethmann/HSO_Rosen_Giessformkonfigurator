namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToDatatypes_V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SingleMoldCup", "CupType", c => c.String(unicode: false));
            AddColumn("dbo.SingleMoldCup", "BTC", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.SingleMoldDisc", "BTC", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.SingleMoldCup", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.SingleMoldDisc", "Description", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.SingleMoldCup", "OuterDiameter");
            DropColumn("dbo.SingleMoldCup", "Height");
            DropColumn("dbo.SingleMoldCup", "HcDiameter");
            DropColumn("dbo.SingleMoldCup", "BoltDiameter");
            DropColumn("dbo.SingleMoldCup", "HcHoles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SingleMoldCup", "HcHoles", c => c.Int());
            AddColumn("dbo.SingleMoldCup", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "Height", c => c.Decimal(nullable: false, precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "OuterDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "Description", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.SingleMoldCup", "Description", c => c.String(maxLength: 255, unicode: false));
            DropColumn("dbo.SingleMoldDisc", "BTC");
            DropColumn("dbo.SingleMoldCup", "BTC");
            DropColumn("dbo.SingleMoldCup", "CupType");
        }
    }
}
