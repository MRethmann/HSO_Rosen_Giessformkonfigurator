//-----------------------------------------------------------------------
// <copyright file="FilterJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class FilterJob
    {
        public List<Baseplate> listBaseplates { get; set; } = new List<Baseplate>();
        public List<Ring> listRings { get; set; } = new List<Ring>();
        public List<InsertPlate> listInsertPlates { get; set; } = new List<InsertPlate>();
        public List<Core> listCores { get; set; } = new List<Core>();
        public List<Bolt> listBolts { get; set; } = new List<Bolt>();

        public List<Cupform> listCupforms { get; set; } = new List<Cupform>();
        public ProductDisc productDisc { get; set; }
        public ProductCup productCup { get; set; }

        public FilterJob(Product product)
        {
            if (product.GetType() == typeof(ProductDisc))
            {
                this.productDisc = (ProductDisc) product;
            } 
            else if (product.GetType() == typeof(ProductCup))
            {
                this.productCup = (ProductCup) product;
            }
            this.ArraysTestData();
            //this.FilterDiscDatabase();
        }

        public void ArraysTestData()
        {
            /*this.listBaseplates.Add(new Baseplate() { Description = "Grundplatte 12", OuterDiameter = 375.00m, Height = 20.00m, OuterKonusMax = 347.89m, OuterKonusMin = 342.00m, OuterKonusAngle = 15.00m, KonusHeight = 11.00m, InnerDiameter = 15.00m, HasHoleguide = true, Hc1Holes = 8, Hc1Diameter = 10, Hc1Thread = "M5", Hc2Holes = 8, Hc2Diameter = 20, Hc2Thread = "M6", Hc3Holes = 8, Hc3Diameter = 30, Hc3Thread = "M7" });
            //this.listBaseplates.Add(new Baseplate() { Bezeichnung_RoCon = "Grundplatte 22", Außendurchmesser = 700.00m, Hoehe = 20.00m, Konus_Außen_Max = 645.58m, Konus_Außen_Min = 639.31m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11.00m, Innendurchmesser = 225.00m, Konus_Innen_Max = 265.31m, Konus_Innen_Min = 259.42m, Konus_Innen_Winkel = 15.00m, Mit_Konusfuehrung = true });
            //this.listInsertPlates.Add(new InsertPlate() { Bezeichnung_RoCon = "Einsatz fuer Grundplatte 22in-24in", Außendurchmesser = 265.00m, Hoehe = 20.00m, Konus_Außen_Max = 265.00m, Konus_Außen_Min = 259.11m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11.00m, Innendurchmesser = 30.00m, Mit_Lochfuehrung = true });
            this.listRings.Add(new Ring() { Description = "Form ring", OuterDiameter = 375.00m, Height = 31.6m, InnerKonusMax = 345.43m, InnerKonusMin = 342.21m, InnerKonusAngle = 15.00m, KonusHeight = 6.00m, InnerDiameter = 315.3m, FillHeightMax = 25.00m, HasKonus = true });
            //this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Formring Dichtscheibe d 324", Außendurchmesser = 375.00m, Hoehe = 21.6m, Konus_Max = 345.52m, Konus_Min = 342.3m, Konus_Winkel = 15.00m, Konus_Hoehe = 6.00m, Innendurchmesser = 330.6m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = true });
            //this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring01", Außendurchmesser = 330.3m, Hoehe = 21.6m, Innendurchmesser = 310.7m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
            //this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring02", Außendurchmesser = 310.5m, Hoehe = 21.6m, Innendurchmesser = 250.0m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
            this.listCores.Add(new Core() { Description = "Einsatz fuer Innendurchmesser d 40", OuterDiameter = 41.4m, Height = 40.6m, KonusHeight = 15.00m, GuideDiameter = 15.00m, GuideHeight = 20.00m, FillHeightMax = 25.6m, HasGuideBolt = true });
            //this.listCores.Add(new Core() { Bezeichnung_RoCon = "Einsatz fuer Innendurchmesser d219", Außendurchmesser = 224.4m, Hoehe = 42.00m, Konus_Außen_Max = 210.00m, Konus_Außen_Min = 206.78m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 6.00m, Gießhoehe_Max = 36.00m, Mit_Konusfuehrung = true });
            //this.listCores.Add(new Core() { Bezeichnung_RoCon = "Einsatz fuer Innendurchmesser d=82", Außendurchmesser = 84.1m, Hoehe = 40.6m, Konus_Hoehe = 15.00m, Durchmesser_Fuehrung = 15.00m, Gießhoehe_Max = 25.6m, Hoehe_Fuehrung = 20.00m, Mit_Fuehrungsstift = true });
            this.listBolts.Add(new Bolt() { ID = 25000, Description = "Testbolt_01", OuterDiameter = 1, FillHeightMax = 35.00m ,Thread = "M5", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25001, Description = "Testbolt_02", OuterDiameter = 2, FillHeightMax = 35.00m, Thread = "M5", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25002, Description = "Testbolt_03", OuterDiameter = 3, FillHeightMax = 35.00m, Thread = "M5", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25003, Description = "Testbolt_04", OuterDiameter = 1, FillHeightMax = 35.00m, Thread = "M6", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25004, Description = "Testbolt_05", OuterDiameter = 2, FillHeightMax = 35.00m, Thread = "M6", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25005, Description = "Testbolt_06", OuterDiameter = 3, FillHeightMax = 35.00m, Thread = "M6", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25006, Description = "Testbolt_07", OuterDiameter = 1, FillHeightMax = 35.00m, Thread = "M7", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25007, Description = "Testbolt_08", OuterDiameter = 2, FillHeightMax = 35.00m, Thread = "M7", HasThread = true });
            this.listBolts.Add(new Bolt() { ID = 25008, Description = "Testbolt_09", OuterDiameter = 3, FillHeightMax = 35.00m, Thread = "M7", HasThread = true });*/

            this.listBaseplates.Add(new Baseplate()
            {
                ID = 001,
                Description = "Baseplate_Test01",
                OuterDiameter = 700m,
                Height = 20,
                OuterKonusMax = 645.58m,
                OuterKonusMin = 639.31m,
                OuterKonusAngle = 15.0m,
                KonusHeight = 11m,
                HasKonus = true,
                InnerKonusMax = 265.31m,
                InnerKonusMin = 259.42m,
                InnerKonusAngle = 15m,
                HasHoleguide = false,
                InnerDiameter = 225m,
                ToleranceInnerDiameter = null,
                HasCore = false,
                Hc1Diameter = 337.59m,
                Hc1Holes = 12,
                Hc1Thread = "M10",
                Hc2Diameter = 286.44m,
                Hc2Holes = 12,
                Hc2Thread = "M10",
                Hc3Diameter = null,
                Hc3Holes = null,
                Hc3Thread = null
            });

            /*this.listInsertPlates.Add(new InsertPlate()
            {
                ID = 002,
                Description = "InsertPlate_Test02",
                OuterDiameter = 265m,
                ToleranceOuterDiameter = null,
                Height = 20,
                OuterKonusMax = 265m,
                OuterKonusMin = 259.11m,
                OuterKonusAngle = 15,
                KonusHeight = 11,
                HasKonus = false,
                InnerKonusMax = null,
                InnerKonusMin = null,
                InnerKonusAngle = null,
                HasHoleguide = true,
                InnerDiameter = 30,
                ToleranceInnerDiameter = "H7",
                HasCore = false,
                Hc1Diameter = null,
                Hc1Holes = null,
                Hc1Thread = null,
                Hc2Diameter = null,
                Hc2Holes = null,
                Hc2Thread = null,
                Hc3Diameter = null,
                Hc3Holes = null,
                Hc3Thread = null
            });*/

            this.listCores.Add(new Core()
            {
                ID = 004,
                Description = "Core_Test01",
                OuterDiameter = 240m,
                ToleranceOuterDiameter = null,
                Height = 42,
                FillHeightMax = 36,
                HasKonus = true,
                OuterKonusMax = 263.96m,
                OuterKonusMin = 259.35m,
                OuterKonusAngle = 15,
                KonusHeight = 6,
                HasGuideBolt = false,
                GuideHeight = null,
                GuideDiameter = null,
                ToleranceGuideDiameter = null,
                HasHoleguide = false,
                AdapterDiameter = null,
            });

            this.listBolts.Add(new Bolt()
            {
                ID = 005,
                Description = "Bolt_Test05",
                Height = 55,
                OuterDiameter = 10,
                FillHeightMax = 40,
                HasThread = true,
                Thread = "M10",
                HasGuideBolt = false,
                GuideHeight = 15,
                GuideOuterDiameter = 10,
            });

            this.listRings.Add(new Ring()
            {
                ID = 003,
                Description = "GuideRing_Test01",
                OuterDiameter = 700m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 630m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = true,
                InnerKonusMax = 643.71m,
                InnerKonusMin = 639.94m,
                InnerKonusAngle = 15.0m,
                KonusHeight = 10m
            });

            this.listRings.Add(new Ring() 
            {
                ID = 006,
                Description = "OuterRing_Test01",
                OuterDiameter = 629m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 620m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = false
            });

            this.listRings.Add(new Ring()
            {
                ID = 007,
                Description = "OuterRing_Test02",
                OuterDiameter = 619m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 600m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = false
            });

            this.listRings.Add(new Ring()
            {
                ID = 008,
                Description = "CoreRing_Test01",
                OuterDiameter = 250m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 241m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = false
            });

            this.listRings.Add(new Ring()
            {
                ID = 009,
                Description = "CoreRing_Test02",
                OuterDiameter = 260m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 251m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = false
            });

            this.listRings.Add(new Ring()
            {
                ID = 010,
                Description = "CoreRing_Test03_SollteNichtGenommenWerden",
                OuterDiameter = 280m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 261m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = false
            });


            //this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring01", Außendurchmesser = 330.3m, Hoehe = 21.6m, Innendurchmesser = 310.7m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
            //this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring02", Außendurchmesser = 310.5m, Hoehe = 21.6m, Innendurchmesser = 250.0m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
        }

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her und speichert die Komponenten in einer lokalen Objektliste. Die Komponenten werden über die Produktparameter vorgefiltert.
        /// </summary>
        public void FilterDiscDatabase()
        {
            if (this.productDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var grundplatte in db.Baseplates)
                    {
                        if (this.productDisc.OuterDiameter < grundplatte.OuterDiameter)
                        {
                            this.listBaseplates.Add(grundplatte);
                            Console.WriteLine("Grundplatte " + grundplatte + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Grundplatte " + grundplatte + " removed.");
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.productDisc.InnerDiameter > ring.OuterDiameter || this.productDisc.OuterDiameter < ring.InnerDiameter)
                        {
                            this.listRings.Add(ring);
                            Console.WriteLine("Ring " + ring + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Ring " + ring + " removed.");
                        }
                    }

                    // no filter for insertplates
                    foreach (var einlegeplatte in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(einlegeplatte);
                        Console.WriteLine("Einlegeplatte " + einlegeplatte + " added to the filter.");
                    }

                    foreach (var innenkern in db.Cores)
                    {
                        if (this.productDisc.InnerDiameter > innenkern.OuterDiameter)
                        {
                            this.listCores.Add(innenkern);
                            Console.WriteLine("Innenkern " + innenkern + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Innenkern " + innenkern + " removed.");
                        }
                    }

                    foreach (var bolzen in db.Bolts)
                    {
                        // TODO: Abgleich hinzufügen. Produkt besitzt aktuell nur das Attribut Lochkreis, welches keine Vergleichseigenschaft besitzt. Durchmesser der Löcher benötigt.
                        if (bolzen.OuterDiameter <= this.productDisc.HcHoleDiameter)
                        {
                            this.listBolts.Add(bolzen);
                            Console.WriteLine("Bolzen " + bolzen + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Bolzen " + bolzen + " removed.");
                        }
                    }

                    foreach (var cupform in db.Cupforms)
                    {
                        // TODO: Abgleich hinzufügen. Produkt besitzt aktuell nur das Attribut Lochkreis, welches keine Vergleichseigenschaft besitzt. Durchmesser der Löcher benötigt.
                        if (cupform.InnerDiameter <= this.productCup.InnerDiameter
                            && cupform.CupType == this.productCup.BaseCup)
                        {
                            this.listCupforms.Add(cupform);
                            Console.WriteLine("Cupform " + cupform + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Cupform " + cupform + " removed.");
                        }
                    }
                }
            }
            else
            {
                // TODO: Prüfen ob dieser Teil relevant ist. Soll das Szenario abfangen, dass kein Produkt vorhanden ist und man den Kombinationsalgorithmus trotzdem ausführen möchte.
                using (var db = new GießformDBContext())
                {
                    foreach (var grundplatte in db.Baseplates)
                    {
                        this.listBaseplates.Add(grundplatte);
                    }

                    foreach (var ring in db.Rings)
                    {
                        this.listRings.Add(ring);
                    }

                    foreach (var einlegeplatte in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(einlegeplatte);
                    }

                    foreach (var innenkern in db.Cores)
                    {
                        this.listCores.Add(innenkern);
                    }

                    foreach (var bolzen in db.Bolts)
                    {
                        this.listBolts.Add(bolzen);
                    }

                    foreach (var cupform in db.Cupforms)
                    {
                        this.listCupforms.Add(cupform);
                    }
                }
            }
        }
    }
}
