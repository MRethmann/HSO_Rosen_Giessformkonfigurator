namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToDatatypes_V2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCup", "CupType", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.ProductCup", "BaseCup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCup", "BaseCup", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.ProductCup", "CupType");
        }
    }
}
