namespace Gießformkonfigurator.UnitTests
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;

    class KombinationStressTest
    {
       /* public static void fillDatabase()
        {
            int Anzahl_Eintraege = 2;
            using (var db = new GießformDBContext())
            {
                for (int i = 1; i < Anzahl_Eintraege; i++)
                {
                    Baseplate gp1 = new Baseplate() { SAP_Nr_ = 10 + i, Bezeichnung_RoCon = "GPTest" + 10 + i, Konus_Innen_Max = 265.31m, Konus_Innen_Min = 259.42m, Konus_Innen_Winkel = 15.00m, Konus_Außen_Max = 347.89m, Konus_Außen_Min = 342.00m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11 };
                    InsertPlate ep1 = new InsertPlate() { SAP_Nr_ = 10 + i, Bezeichnung_RoCon = "EPTest" + 10 + i, Konus_Außen_Max = 265.00m, Konus_Außen_Min = 259.11m, Konus_Außen_Winkel = 15.00m, Konus_Innen_Max = 265.31m, Konus_Innen_Min = 259.42m, Konus_Innen_Winkel = 15.00m, Mit_Konusfuehrung = true };
                    Ring fr1 = new Ring() { SAP_Nr_ = 10 + i, Bezeichnung_RoCon = "FRTest" + 10 + i, Konus_Max = 345.43m, Konus_Min = 342.21m, Konus_Winkel = 15.00m, Konus_Hoehe = 6 };
                    Core k1 = new Core() { SAP_Nr_ = 10 + i, Bezeichnung_RoCon = "KTest" + 10 + i, Konus_Außen_Max = 265.00m, Konus_Außen_Min = 259.20m, Konus_Außen_Winkel = 15.00m, Mit_Konusfuehrung = true };

                    Console.ReadLine();

                    db.Baseplates.Add(gp1);
                    db.InsertPlates.Add(ep1);
                    db.Rings.Add(fr1);
                    db.Cores.Add(k1);
                    db.SaveChanges();
                }
            }
        }*/
    }
}
