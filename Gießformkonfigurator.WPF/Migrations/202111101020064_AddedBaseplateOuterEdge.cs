namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBaseplateOuterEdge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baseplate", "HasOuterEdge", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Baseplate", "HasOuterEdge");
        }
    }
}
