﻿//-----------------------------------------------------------------------
// <copyright file="FilterJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using log4net;

    /// <summary>
    /// Gets filtered database information to be used in the later algorithms.
    /// </summary>
    public class FilterJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FilterJob));

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterJob"/> class.
        /// </summary>
        /// <param name="productDisc">Expects product Disc.</param>
        public FilterJob(ProductDisc productDisc)
        {
            this.ProductDisc = productDisc;

            this.AdjustProductInformation(productDisc);
            this.GetFilteredDatabase();
        }

        public FilterJob(ProductCup productCup)
        {
            this.ProductCup = productCup;

            this.AdjustProductInformation(productCup);
            this.GetFilteredDatabase();
        }

        public List<Baseplate> ListBaseplates { get; set; } = new List<Baseplate>();

        public List<Ring> ListRings { get; set; } = new List<Ring>();

        public List<InsertPlate> ListInsertPlates { get; set; } = new List<InsertPlate>();

        public List<Core> ListCores { get; set; } = new List<Core>();

        public List<Bolt> ListBolts { get; set; } = new List<Bolt>();

        public List<SingleMoldDisc> ListSingleMoldDiscs { get; set; } = new List<SingleMoldDisc>();

        public List<SingleMoldCup> ListSingleMoldCups { get; set; } = new List<SingleMoldCup>();

        public List<CoreSingleMold> ListCoresSingleMold { get; set; } = new List<CoreSingleMold>();

        public List<Cupform> ListCupforms { get; set; } = new List<Cupform>();

        public ProductDisc ProductDisc { get; set; }

        public ProductCup ProductCup { get; set; }

        public ToleranceSettings ToleranceSettings { get; set; } = new ToleranceSettings();

        /// <summary>
        /// ProductDisc
        /// Transforms boltCircleType key into real dimensions via btc Table in database. Also adjusts product dimensions based on material factor.
        /// </summary>
        /// <param name="productDisc">Expects product Disc.</param>
        public void AdjustProductInformation(ProductDisc productDisc)
        {

            if (!string.IsNullOrWhiteSpace(this.ProductDisc.BTC))
            {
                BoltCircleType boltCircleInformation = new BoltCircleType();

                try
                {
                    using (var db = new GießformDBContext())
                    {
                        boltCircleInformation = db.BoltCircleTypes.Find(this.ProductDisc.BTC);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                this.ProductDisc.HcDiameter = boltCircleInformation?.Diameter;
                this.ProductDisc.HcHoleDiameter = boltCircleInformation?.HoleDiameter;
                this.ProductDisc.HcHoles = boltCircleInformation?.HoleQty;
            }
        }

        /// <summary>
        /// ProductCup.
        /// Transforms boltCircleType key into real dimensions via btc Table in database. Also adjusts product dimensions based on material factor.
        /// </summary>
        /// <param name="productCup">Expects product Cup.</param>
        public void AdjustProductInformation(ProductCup productCup)
        {
            if (!string.IsNullOrWhiteSpace(this.ProductCup.BTC))
            {
                BoltCircleType boltCircleInformation = new BoltCircleType();

                try
                {
                    using (var db = new GießformDBContext())
                    {
                        boltCircleInformation = db.BoltCircleTypes.Find(this.ProductCup.BTC);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                // TODO: Add propertys to product cup.
                // this.ProductCup.HcDiameter = boltCircleInformation?.Diameter;
                // this.ProductCup.HcHoleDiameter = boltCircleInformation?.HoleDiameter;
                // this.ProductCup.HcHoles = boltCircleInformation?.HoleQty;
            }

            Log.Info("ProductDisc information with shrink --> OD: " + this.ProductDisc.OuterDiameter + ", ID: " + this.ProductDisc.InnerDiameter + ", Height: " + this.ProductDisc.Height);
        }

        /// <summary>
        /// Connects to the database context and gets filtered list of all components and molds based on the product specifications.
        /// </summary>
        public void GetFilteredDatabase()
        {
            if (this.ProductDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var baseplate in db.Baseplates)
                    {
                        if (this.ProductDisc?.MultiMoldDimensions.OuterDiameter < baseplate.OuterDiameter)
                        {
                            this.ListBaseplates.Add(baseplate);
                            Log.Info($"Added baseplate: {baseplate}");
                        }
                        else
                        {
                            Log.Info($"Removed baseplate: {baseplate} by {this.ProductDisc?.OuterDiameter - baseplate.OuterDiameter}");
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.ProductDisc?.InnerDiameter >= ring.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                            || this.ProductDisc?.OuterDiameter <= ring.InnerDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN)
                        {
                            this.ListRings.Add(ring);
                            Log.Info($"Added ring: {ring}");
                        }
                        else
                        {
                            Log.Info($"Removed baseplate: {ring} by {this.ProductDisc?.InnerDiameter - ring.OuterDiameter} / {this.ProductDisc?.OuterDiameter - ring.InnerDiameter}");
                        }
                    }

                    // no filter for insertplates
                    foreach (var insertPlate in db.InsertPlates)
                    {
                        this.ListInsertPlates.Add(insertPlate);
                    }

                    foreach (var core in db.Cores)
                    {
                        if (this.ProductDisc?.InnerDiameter >= core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                        {
                            this.ListCores.Add(core);
                            Log.Info($"Added core: {core}");
                        }
                        else
                        {
                            Log.Info($"Removed core: {core} by {this.ProductDisc?.InnerDiameter - core.OuterDiameter}");
                        }
                    }

                    foreach (var bolt in db.Bolts)
                    {
                        if (bolt.OuterDiameter <= this.ProductDisc?.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter)
                        {
                            this.ListBolts.Add(bolt);
                            Log.Info($"Added bolt: {bolt}");
                        }
                        else
                        {
                            Log.Info($"Removed bolt: {bolt} by {bolt.OuterDiameter - this.ProductDisc?.HcHoleDiameter}");
                        }
                    }

                    foreach (var cupform in db.Cupforms)
                    {
                        if (cupform.InnerDiameter <= this.ProductCup?.InnerDiameter
                            && cupform?.CupType == this.ProductCup?.BaseCup)
                        {
                            this.ListCupforms.Add(cupform);
                            Log.Info($"Added cupform: {cupform}");
                        }
                        else
                        {
                            Log.Info($"Removed cupform: {cupform} by {cupform?.InnerDiameter - this.ProductCup?.InnerDiameter} / {cupform?.CupType} != {this.ProductCup?.BaseCup}");
                        }
                    }

                    foreach (var singleMoldDisc in db.SingleMoldDiscs)
                    {
                        if (this.ProductDisc.OuterDiameter <= singleMoldDisc.OuterDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                            && this.ProductDisc.InnerDiameter >= singleMoldDisc.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                            && this.ProductDisc.Height <= singleMoldDisc.Height)
                        {
                            this.ListSingleMoldDiscs.Add(singleMoldDisc);
                            Log.Info($"Added singleMoldDisc: {singleMoldDisc}");
                        }
                        else
                        {
                            Log.Info($"Removed singleMoldDisc: {singleMoldDisc} by {this.ProductDisc.OuterDiameter - singleMoldDisc.OuterDiameter + 3} / {this.ProductDisc.OuterDiameter - singleMoldDisc.OuterDiameter - 1} / {ProductDisc.InnerDiameter - singleMoldDisc.InnerDiameter + 3} / {ProductDisc.InnerDiameter - singleMoldDisc.InnerDiameter - 1}");
                        }
                    }

                    foreach (var coreSingleMold in db.CoreSingleMolds)
                    {
                        if (this.ProductDisc?.InnerDiameter >= coreSingleMold.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                        {
                            this.ListCoresSingleMold.Add(coreSingleMold);
                            Log.Info($"Added coreSingleMold: {coreSingleMold}");
                        }
                        else
                        {
                            Log.Info($"Removed coreSingleMold: {coreSingleMold} by {coreSingleMold.OuterDiameter - this.ProductDisc?.InnerDiameter}");
                        }
                    }
                }
            }
        }

    }
}
