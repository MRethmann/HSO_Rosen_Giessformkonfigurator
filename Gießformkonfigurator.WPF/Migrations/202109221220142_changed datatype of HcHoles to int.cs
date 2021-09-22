namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddatatypeofHcHolestoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SingleMoldCup", "HcHoles", c => c.Int());
            AlterColumn("dbo.SingleMoldDisc", "HcHoles", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SingleMoldDisc", "HcHoles", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.SingleMoldCup", "HcHoles", c => c.Decimal(precision: 10, scale: 2));
        }
    }
}
