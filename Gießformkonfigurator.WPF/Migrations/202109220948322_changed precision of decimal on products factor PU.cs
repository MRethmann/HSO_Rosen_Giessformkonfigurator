namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedprecisionofdecimalonproductsfactorPU : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCup", "FactorPU", c => c.Decimal(precision: 10, scale: 5));
            AlterColumn("dbo.ProductDisc", "FactorPU", c => c.Decimal(precision: 10, scale: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductDisc", "FactorPU", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.ProductCup", "FactorPU", c => c.Decimal(precision: 10, scale: 2));
        }
    }
}
