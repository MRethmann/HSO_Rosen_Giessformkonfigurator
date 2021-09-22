namespace Gießformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCoreSingleMoldsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoreSingleMold",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                        OuterDiameter = c.Decimal(nullable: false, precision: 10, scale: 2),
                        InnerDiameter = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 10, scale: 2),
                        FactorPU = c.Decimal(precision: 10, scale: 5),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CoreSingleMold");
        }
    }
}
