namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedprecisiononcoreMoldSingle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CoreSingleMold", "OuterDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 4));
            AlterColumn("dbo.CoreSingleMold", "InnerDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CoreSingleMold", "InnerDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.CoreSingleMold", "OuterDiameter", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
