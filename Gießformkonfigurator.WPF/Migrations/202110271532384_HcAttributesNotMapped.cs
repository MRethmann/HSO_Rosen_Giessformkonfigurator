namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HcAttributesNotMapped : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SingleMoldCup", "HcDiameter");
            DropColumn("dbo.SingleMoldCup", "BoltDiameter");
            DropColumn("dbo.SingleMoldCup", "HcHoles");
            DropColumn("dbo.SingleMoldDisc", "HcDiameter");
            DropColumn("dbo.SingleMoldDisc", "BoltDiameter");
            DropColumn("dbo.SingleMoldDisc", "HcHoles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SingleMoldDisc", "HcHoles", c => c.Int());
            AddColumn("dbo.SingleMoldDisc", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldDisc", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "HcHoles", c => c.Int());
            AddColumn("dbo.SingleMoldCup", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
        }
    }
}
