namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fillHeighoncoremaybenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Core", "FillHeightMax", c => c.Decimal(precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Core", "FillHeightMax", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
