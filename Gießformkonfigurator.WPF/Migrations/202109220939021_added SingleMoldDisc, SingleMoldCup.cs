namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSingleMoldDiscSingleMoldCup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SingleMoldCup",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Description = c.String(maxLength: 255, unicode: false),
                        OuterDiameter = c.Decimal(precision: 10, scale: 2),
                        InnerDiameter = c.Decimal(precision: 10, scale: 2),
                        Height = c.Decimal(precision: 10, scale: 2),
                        HcDiameter = c.Decimal(precision: 10, scale: 2),
                        BoltDiameter = c.Decimal(precision: 10, scale: 2),
                        HcHoles = c.Decimal(precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SingleMoldDisc",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Description = c.String(maxLength: 255, unicode: false),
                        OuterDiameter = c.Decimal(precision: 10, scale: 2),
                        InnerDiameter = c.Decimal(precision: 10, scale: 2),
                        Height = c.Decimal(precision: 10, scale: 2),
                        HcDiameter = c.Decimal(precision: 10, scale: 2),
                        BoltDiameter = c.Decimal(precision: 10, scale: 2),
                        HcHoles = c.Decimal(precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SingleMoldDisc");
            DropTable("dbo.SingleMoldCup");
        }
    }
}
