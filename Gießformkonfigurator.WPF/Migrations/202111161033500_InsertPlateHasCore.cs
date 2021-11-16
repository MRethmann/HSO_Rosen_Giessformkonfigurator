namespace Giessformkonfigurator.WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertPlateHasCore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cupform", "HasCore", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cupform", "HasGuideBolt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cupform", "HasGuideBolt", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cupform", "HasCore");
        }
    }
}
