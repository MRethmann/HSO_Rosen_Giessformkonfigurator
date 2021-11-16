namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHcAttributesToSingleMoldCup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SingleMoldCup", "HcDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "BoltDiameter", c => c.Decimal(precision: 10, scale: 4));
            AddColumn("dbo.SingleMoldCup", "HcHoles", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SingleMoldCup", "HcHoles");
            DropColumn("dbo.SingleMoldCup", "BoltDiameter");
            DropColumn("dbo.SingleMoldCup", "HcDiameter");
        }
    }
}
