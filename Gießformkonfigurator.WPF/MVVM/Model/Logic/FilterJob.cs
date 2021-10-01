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
                productDisc = new ProductDisc();
                productDisc = (ProductDisc) product;
            } 
            else if (product.GetType() == typeof(ProductCup))
            {
                productCup = new ProductCup();
                productCup = (ProductCup) product;
            }

            this.AdjustProductInformation();
            this.GetFilteredDatabase();
        }

        public void AdjustProductInformation()
        {
            if (this.productDisc != null)
            {
                if (!String.IsNullOrWhiteSpace(productDisc.BTC))
                {
                    BoltCircleType boltCircleInformation = new BoltCircleType();

                    try
                    {
                        using (var db = new GießformDBContext())
                        {
                            boltCircleInformation = db.BoltCircleTypes.Find(productDisc.BTC);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    productDisc.HcDiameter = boltCircleInformation?.Diameter;
                    productDisc.HcHoleDiameter = boltCircleInformation?.HoleDiameter;
                    productDisc.HcHoles = boltCircleInformation?.HoleQty;
                }

                productDisc.OuterDiameter = Math.Round(productDisc.OuterDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.InnerDiameter = Math.Round(productDisc.InnerDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.Height = Math.Round(productDisc.Height * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                //productDisc.HcDiameter = Math.Round((Decimal)productDisc?.HcDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                //productDisc.HcHoleDiameter = Math.Round((Decimal)productDisc?.HcHoleDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);

            }
            else if (this.productCup != null)
            {
                productCup.InnerDiameter = productCup.InnerDiameter * productCup.FactorPU.GetValueOrDefault(1m);
            }
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

                    // Check if it makes sense
                    //singleMoldDisc.OuterDiameter + 0.1m >= productDisc?.OuterDiameter && singleMoldDisc.OuterDiameter - 0.1m <= productDisc?.OuterDiameter

                    foreach (var singleMoldDisc in db.SingleMoldDiscs)
                    {
                        if (singleMoldDisc.InnerDiameter + 0.1m >= productDisc?.InnerDiameter && singleMoldDisc.InnerDiameter - 2m <= productDisc?.InnerDiameter)
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
