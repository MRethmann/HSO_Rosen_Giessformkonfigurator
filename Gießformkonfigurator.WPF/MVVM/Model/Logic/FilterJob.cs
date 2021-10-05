//-----------------------------------------------------------------------
// <copyright file="FilterJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using log4net;
    using System;
    using System.Collections.Generic;

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

        public ToleranceSettings toleranceSettings { get; set; }

        private static readonly ILog log = LogManager.GetLogger(typeof(FilterJob));

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

            toleranceSettings = new ToleranceSettings();

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

                if (productDisc.FactorPU == null)
                {

                }

                productDisc.OuterDiameter = Math.Round(productDisc.OuterDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.InnerDiameter = Math.Round(productDisc.InnerDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.Height = Math.Round(productDisc.Height * productDisc.FactorPU.GetValueOrDefault(1m), 2);
                productDisc.HcDiameter = productDisc?.HcDiameter != null ? Math.Round((Decimal)productDisc?.HcDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2) : 0.0m;
                productDisc.HcHoleDiameter = productDisc?.HcHoleDiameter != null ? Math.Round((Decimal)productDisc?.HcHoleDiameter * productDisc.FactorPU.GetValueOrDefault(1m), 2) : 0.0m;

                log.Info("ProductDisc information with shrink --> OD: " + productDisc.OuterDiameter + ", ID: " + productDisc.InnerDiameter + ", Height: " + productDisc.Height);

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
                    foreach (var baseplate in db.Baseplates)
                    {
                        if (this.productDisc?.OuterDiameter < baseplate.OuterDiameter)
                        {
                            this.listBaseplates.Add(baseplate);
                            log.Info($"Added baseplate: {baseplate}");
                        }
                        else
                            log.Info($"Removed baseplate: {baseplate} by {productDisc?.OuterDiameter - baseplate.OuterDiameter}");
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.productDisc?.InnerDiameter >= ring.OuterDiameter - toleranceSettings.product_InnerDiameter_MIN // Tolerance Rings MIN --> smaller Product
                            || this.productDisc?.OuterDiameter <= ring.InnerDiameter + toleranceSettings.product_OuterDiameter_MIN) //Tolerance Ring Rings MIN --> smaller Product
                        {
                            this.listRings.Add(ring);
                            log.Info($"Added ring: {ring}");
                        }
                        else
                            log.Info($"Removed baseplate: {ring} by {productDisc?.InnerDiameter - ring.OuterDiameter} / {this.productDisc?.OuterDiameter - ring.InnerDiameter}");
                    }

                    // no filter for insertplates
                    foreach (var insertPlate in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(insertPlate);

                        //No need since insertplates can not be prefiltered
                        //log.Info($"Added insertplate: {insertPlate}");
                    }

                    foreach (var core in db.Cores)
                    {
                        if (this.productDisc?.InnerDiameter >= core.OuterDiameter - toleranceSettings.product_InnerDiameter_MIN) // Tolerance Core OuterDiameter MIN
                        {
                            this.listCores.Add(core);
                            log.Info($"Added core: {core}");
                        }
                        else
                            log.Info($"Removed core: {core} by {productDisc?.InnerDiameter - core.OuterDiameter}");
                    }

                    foreach (var bolt in db.Bolts)
                    {
                        if (bolt.OuterDiameter <= this.productDisc?.HcHoleDiameter + toleranceSettings.bolt_Diameter)
                        {
                            this.listBolts.Add(bolt);
                            log.Info($"Added bolt: {bolt}");
                        }
                        else
                            log.Info($"Removed bolt: {bolt} by {bolt.OuterDiameter - this.productDisc?.HcHoleDiameter}");
                    }

                    foreach (var cupform in db.Cupforms)
                    {
                        if (cupform.InnerDiameter <= this.productCup?.InnerDiameter
                            && cupform?.CupType == this.productCup?.BaseCup)
                        {
                            this.listCupforms.Add(cupform);
                            log.Info($"Added cupform: {cupform}");
                        }
                        else
                            log.Info($"Removed cupform: {cupform} by {cupform?.InnerDiameter - this.productCup?.InnerDiameter} / {cupform?.CupType} != {this.productCup?.BaseCup}");
                    }

                    foreach (var singleMoldDisc in db.SingleMoldDiscs)
                    {
                        if (productDisc.OuterDiameter <= singleMoldDisc.OuterDiameter + toleranceSettings.product_OuterDiameter_MIN // Tolerance singleMold OuterDiameter MIN
                            && productDisc.InnerDiameter >= singleMoldDisc.InnerDiameter - toleranceSettings.product_InnerDiameter_MIN
                            && productDisc.Height <= singleMoldDisc.Height) // Tolerance singleMold InnerDiameter MIN
                        {
                            this.listSingleMoldDiscs.Add(singleMoldDisc);
                            log.Info($"Added singleMoldDisc: {singleMoldDisc}");
                        }
                        else
                            log.Info($"Removed singleMoldDisc: {singleMoldDisc} by {productDisc.OuterDiameter - singleMoldDisc.OuterDiameter + 3} / {productDisc.OuterDiameter - singleMoldDisc.OuterDiameter - 1} / {productDisc.InnerDiameter - singleMoldDisc.InnerDiameter + 3} / {productDisc.InnerDiameter - singleMoldDisc.InnerDiameter - 1}");
                    }

                    // TODO: GGF. Abfrage für Cups hinzufügen.
                    foreach (var coreSingleMold in db.CoreSingleMolds)
                    {
                        if (productDisc?.InnerDiameter >= coreSingleMold.OuterDiameter - toleranceSettings.product_InnerDiameter_MIN) // Tolerance Core Outer Diameter MIN
                        {
                            this.listCoresSingleMold.Add(coreSingleMold);
                            log.Info($"Added coreSingleMold: {coreSingleMold}");
                        }
                        else
                            log.Info($"Removed coreSingleMold: {coreSingleMold} by {coreSingleMold.OuterDiameter - productDisc?.InnerDiameter}");
                    }
                }
            }
        }
    }
}
