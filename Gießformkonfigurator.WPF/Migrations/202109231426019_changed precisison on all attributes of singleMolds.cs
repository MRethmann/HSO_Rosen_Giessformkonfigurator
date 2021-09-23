namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedprecisisononallattributesofsingleMolds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SingleMoldCup", "OuterDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldCup", "InnerDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldCup", "Height", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldCup", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldCup", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "OuterDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "InnerDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "Height", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
            AlterColumn("dbo.SingleMoldDisc", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SingleMoldDisc", "BoltDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldDisc", "HcDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldDisc", "Height", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldDisc", "InnerDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldDisc", "OuterDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "BoltDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "HcDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "Height", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "InnerDiameter", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "OuterDiameter", c => c.Decimal(precision: 10, scale: 2));
        }
    }
}
