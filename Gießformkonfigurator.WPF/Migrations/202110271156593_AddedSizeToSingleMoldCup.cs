namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSizeToSingleMoldCup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SingleMoldCup", "Size", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SingleMoldCup", "Size");
        }
    }
}
