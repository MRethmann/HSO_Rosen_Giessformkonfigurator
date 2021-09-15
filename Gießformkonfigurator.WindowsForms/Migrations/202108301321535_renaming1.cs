namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renaming1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bolzen", newName: "Bolt");
            RenameTable(name: "dbo.Einlegeplatte", newName: "Insertplate");
            RenameTable(name: "dbo.Grundplatte", newName: "Baseplate");
            RenameTable(name: "dbo.Kern", newName: "Core");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Core", newName: "Kern");
            RenameTable(name: "dbo.Baseplate", newName: "Grundplatte");
            RenameTable(name: "dbo.Insertplate", newName: "Einlegeplatte");
            RenameTable(name: "dbo.Bolt", newName: "Bolzen");
        }
    }
}
