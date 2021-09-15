namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toleranzringaufvarchargeaendert : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ring", "Toleranz_Außendurchmesser", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ring", "Toleranz_Außendurchmesser", c => c.String());
        }
    }
}
