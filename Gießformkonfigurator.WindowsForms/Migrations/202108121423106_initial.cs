namespace Gießformkonfigurator.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoltCircleType",
                c => new
                    {
                        TypeDescription = c.String(nullable: false, maxLength: 10, unicode: false),
                        Diameter = c.Int(nullable: false),
                        HoleQty = c.Int(nullable: false),
                        Angle = c.Decimal(nullable: false, precision: 10, scale: 2),
                        HolepairAngle = c.Decimal(nullable: false, precision: 10, scale: 2),
                        HoleDiameter = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.TypeDescription);
            
            CreateTable(
                "dbo.Bolzen",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(maxLength: 100, unicode: false),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Gießhoehe_Max = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Mit_Gewinde = c.Boolean(nullable: false),
                        Gewinde = c.Decimal(precision: 10, scale: 2),
                        Mit_Steckbolzen = c.Boolean(nullable: false),
                        Hoehe_Fuehrung = c.Decimal(precision: 10, scale: 2),
                        Außendurchmesser_Fuehrung = c.Decimal(precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.Cupform",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(maxLength: 255, unicode: false),
                        Cup_Typ = c.String(maxLength: 20, unicode: false),
                        Innendurchmesser = c.Decimal(precision: 10, scale: 2),
                        Toleranz_Innendurchmesser = c.String(maxLength: 10),
                        LK = c.Decimal(precision: 10, scale: 2),
                        Mit_Fuehrungsstift = c.Boolean(nullable: false),
                        Mit_Innengewinde = c.Boolean(nullable: false),
                        Mit_Konusfuehrung = c.Boolean(nullable: false),
                        Mit_Lochfuehrung = c.Boolean(nullable: false),
                        Mit_Innenkern = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.Einlegeplatte",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(maxLength: 100, unicode: false),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Toleranz_Außendurchmesser = c.String(maxLength: 5, unicode: false),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Konus_Außen_Max = c.Decimal(precision: 10, scale: 2),
                        Konus_Außen_Min = c.Decimal(precision: 10, scale: 2),
                        Konus_Außen_Winkel = c.Decimal(precision: 10, scale: 2),
                        Konus_Hoehe = c.Decimal(precision: 10, scale: 2),
                        Mit_Konusfuehrung = c.Boolean(nullable: false),
                        Konus_Innen_Max = c.Decimal(precision: 10, scale: 2),
                        Konus_Innen_Min = c.Decimal(precision: 10, scale: 2),
                        Konus_Innen_Winkel = c.Decimal(precision: 10, scale: 2),
                        Mit_Lochfuehrung = c.Boolean(nullable: false),
                        Innendurchmesser = c.Decimal(precision: 10, scale: 2),
                        Toleranz_Innendurchmesser = c.String(maxLength: 5, unicode: false),
                        Mit_Kern = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.Grundplatte",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(maxLength: 255, unicode: false),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Konus_Außen_Max = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Konus_Außen_Min = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Konus_Außen_Winkel = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Konus_Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Mit_Konusfuehrung = c.Boolean(nullable: false),
                        Konus_Innen_Max = c.Decimal(precision: 10, scale: 2),
                        Konus_Innen_Min = c.Decimal(precision: 10, scale: 2),
                        Konus_Innen_Winkel = c.Decimal(precision: 5, scale: 2),
                        Mit_Lochfuehrung = c.Boolean(nullable: false),
                        Innendurchmesser = c.Decimal(precision: 10, scale: 2),
                        Toleranz_Innendurchmesser = c.String(maxLength: 10, unicode: false),
                        Mit_Kern = c.Boolean(nullable: false),
                        Lk1Durchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk1Bohrungen = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk1Gewinde = c.String(),
                        Lk2Durchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk2Bohrungen = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk2Gewinde = c.String(),
                        Lk3Durchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk3Bohrungen = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk3Gewinde = c.String(),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.Kern",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(nullable: false, maxLength: 100, unicode: false),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Toleranz_Außendurchmesser = c.String(maxLength: 10, unicode: false),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Gießhoehe_Max = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Mit_Konusfuehrung = c.Boolean(nullable: false),
                        Konus_Außen_Max = c.Decimal(precision: 10, scale: 2),
                        Konus_Außen_Min = c.Decimal(precision: 10, scale: 2),
                        Konus_Außen_Winkel = c.Decimal(precision: 5, scale: 2),
                        Konus_Hoehe = c.Decimal(precision: 10, scale: 2),
                        Mit_Fuehrungsstift = c.Boolean(nullable: false),
                        Hoehe_Fuehrung = c.Decimal(precision: 10, scale: 2),
                        Durchmesser_Fuehrung = c.Decimal(precision: 10, scale: 2),
                        Toleranz_Durchmesser_Fuehrung = c.Decimal(precision: 10, scale: 2),
                        Mit_Lochfuehrung = c.Boolean(nullable: false),
                        Durchmesser_Adapter = c.Decimal(precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.ProduktCup",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        GrundCup = c.String(name: "Grund-Cup", nullable: false, maxLength: 100, unicode: false),
                        Innendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        LK = c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.ProduktDisc",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Innendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Lk1Durchmesser = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk1Bohrungen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk1Gewinde = c.String(),
                        Lk2Durchmesser = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk2Bohrungen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk2Gewinde = c.String(),
                        Lk3Durchmesser = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk3Bohrungen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lk3Gewinde = c.String(),
                        LK = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.SAPNr);
            
            CreateTable(
                "dbo.Ring",
                c => new
                    {
                        SAPNr = c.Int(name: "SAP-Nr.", nullable: false),
                        Bezeichnung_RoCon = c.String(nullable: false, maxLength: 255, unicode: false),
                        Außendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Toleranz_Außendurchmesser = c.Decimal(precision: 10, scale: 2),
                        Innendurchmesser = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Toleranz_Innendurchmesser = c.String(maxLength: 10, unicode: false),
                        Hoehe = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Gießhoehe_Max = c.Decimal(nullable: false, precision: 10, scale: 2),
                        mit_Konusfuehrung = c.Boolean(nullable: false),
                        Konus_Max = c.Decimal(precision: 10, scale: 2),
                        Konus_Min = c.Decimal(precision: 10, scale: 2),
                        Konus_Winkel = c.Decimal(precision: 10, scale: 2),
                        Konus_Hoehe = c.Decimal(precision: 10, scale: 2),
                        ohne_Konusfuehrung = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SAPNr);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ring");
            DropTable("dbo.ProduktDisc");
            DropTable("dbo.ProduktCup");
            DropTable("dbo.Kern");
            DropTable("dbo.Grundplatte");
            DropTable("dbo.Einlegeplatte");
            DropTable("dbo.Cupform");
            DropTable("dbo.Bolzen");
            DropTable("dbo.BoltCircleType");
        }
    }
}
