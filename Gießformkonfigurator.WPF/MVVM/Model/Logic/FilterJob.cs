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
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    class FilterJob
    {
        public List<Baseplate> listBaseplates { get; set; } = new List<Baseplate>();
        public List<Ring> listRings { get; set; } = new List<Ring>();
        public List<InsertPlate> listInsertPlates { get; set; } = new List<InsertPlate>();
        public List<Core> listCores { get; set; } = new List<Core>();
        public List<Bolt> listBolts { get; set; } = new List<Bolt>();
        public List<SingleMoldDisc> listSingleMoldDiscs { get; set; } = new List<SingleMoldDisc>();
        public List<SingleMoldCup> listSingleMoldCups { get; set; } = new List<SingleMoldCup>();
        public List<CoreSingleMold> listCoresSingleMold { get; set; } = new List<CoreSingleMold>();
        public List<Cupform> listCupforms { get; set; } = new List<Cupform>();
        public ProductDisc productDisc { get; set; }
        public ProductCup productCup { get; set; }

        public FilterJob(Product product)
        {
            if (product.GetType() == typeof(ProductDisc))
            {
                this.productDisc = (ProductDisc) product;
                productDisc.OuterDiameter = Math.Round(productDisc.OuterDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.InnerDiameter = Math.Round(productDisc.InnerDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.Height = Math.Round(productDisc.Height * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                if (productDisc.HcDiameter != null)
                {
                    productDisc.HcDiameter = Math.Round((Decimal)productDisc.HcDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                }
                
                if (productDisc.HcHoleDiameter != null)
                {
                    productDisc.HcHoleDiameter = Math.Round((Decimal)productDisc.HcHoleDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                }
            } 
            else if (product.GetType() == typeof(ProductCup))
            {
                this.productCup = (ProductCup) product;
                productCup.InnerDiameter = productCup.InnerDiameter * productCup.FactorPU.GetValueOrDefault(1m);
            }

            //this.ArraysTestData();
            this.GetFilteredDatabase();
        }

        public void ArraysTestData()
        {
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
        public void GetFilteredDatabase()
        {
            if (this.productDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var grundplatte in db.Baseplates)
                    {
                        if (this.productDisc?.OuterDiameter < grundplatte.OuterDiameter)
                        {
                            this.listBaseplates.Add(grundplatte);
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.productDisc?.InnerDiameter > ring.OuterDiameter || this.productDisc?.OuterDiameter < ring.InnerDiameter)
                        {
                            this.listRings.Add(ring);
                        }
                    }

                    // no filter for insertplates
                    foreach (var einlegeplatte in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(einlegeplatte);
                    }

                    foreach (var innenkern in db.Cores)
                    {
                        if (this.productDisc?.InnerDiameter > innenkern.OuterDiameter)
                        {
                            this.listCores.Add(innenkern);
                        }
                    }

                    foreach (var bolzen in db.Bolts)
                    {
                        // TODO: Abgleich hinzufügen. Produkt besitzt aktuell nur das Attribut Lochkreis, welches keine Vergleichseigenschaft besitzt. Durchmesser der Löcher benötigt.
                        if (bolzen.OuterDiameter <= this.productDisc?.HcHoleDiameter)
                        {
                            this.listBolts.Add(bolzen);
                        }
                    }

                    foreach (var cupform in db.Cupforms)
                    {
                        // TODO: Abgleich hinzufügen. Produkt besitzt aktuell nur das Attribut Lochkreis, welches keine Vergleichseigenschaft besitzt. Durchmesser der Löcher benötigt.
                        if (cupform.InnerDiameter <= this.productCup?.InnerDiameter
                            && cupform.CupType == this.productCup.BaseCup)
                        {
                            this.listCupforms.Add(cupform);
                        }
                    }

                    foreach (var singleMoldDisc in db.SingleMoldDiscs)
                    {
                        if (singleMoldDisc.OuterDiameter + 0.1m >= productDisc?.OuterDiameter && singleMoldDisc.OuterDiameter - 0.1m <= productDisc?.OuterDiameter
                            && singleMoldDisc.InnerDiameter + 0.1m >= productDisc?.InnerDiameter && singleMoldDisc.InnerDiameter - 0.1m <= productDisc?.InnerDiameter)
                        {
                            this.listSingleMoldDiscs.Add(singleMoldDisc);
                        }
                    }

                    // TODO: GGF. Abfrage für Cups hinzufügen.
                    foreach (var coreSingleMold in db.CoreSingleMolds)
                    {
                        if (coreSingleMold.OuterDiameter <= productDisc?.InnerDiameter)
                        {
                            this.listCoresSingleMold.Add(coreSingleMold);
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
