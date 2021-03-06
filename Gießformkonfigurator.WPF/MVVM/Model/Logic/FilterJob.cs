//-----------------------------------------------------------------------
// <copyright file="FilterJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
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
            this.ListBaseplates = new List<Baseplate>();
            this.ListRings = new List<Ring>();
            this.ListInsertPlates = new List<InsertPlate>();
            this.ListCores = new List<Core>();
            this.ListBolts = new List<Bolt>();
            this.ListSingleMoldDiscs = new List<SingleMoldDisc>();
            this.ListCoresSingleMold = new List<CoreSingleMold>();

            this.GetFilteredMultiMoldDiscComponents();
            this.GetFilteredSingleMoldDiscs();

            Log.Info("Anzahl Grundplatten: " + this.ListBaseplates.Count.ToString());
            Log.Info("Anzahl Ringe: " + this.ListRings.Count.ToString());
            Log.Info("Anzahl InsertPlates: " + this.ListInsertPlates.Count.ToString());
            Log.Info("Anzahl Cores: " + this.ListCores.Count.ToString());
            Log.Info("Anzahl Bolts: " + this.ListBolts.Count.ToString());
            Log.Info("Anzahl SingleMolds: " + this.ListSingleMoldDiscs.Count.ToString());
            Log.Info("Anzahl CoreSingleMold: " + this.ListCoresSingleMold.Count.ToString());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterJob"/> class.
        /// </summary>
        /// <param name="productCup">Expects product Cup.</param>
        public FilterJob(ProductCup productCup)
        {
            this.ProductCup = productCup;
            this.ListRings = new List<Ring>();
            this.ListInsertPlates = new List<InsertPlate>();
            this.ListCores = new List<Core>();
            this.ListBolts = new List<Bolt>();
            this.ListSingleMoldCups = new List<SingleMoldCup>();
            this.ListCoresSingleMold = new List<CoreSingleMold>();
            this.ListCupforms = new List<Cupform>();

            this.GetFilteredMultiMoldCupComponents();
            this.GetFilteredSingleMoldCups();

            Log.Info("Anzahl Cupforms: " + this.ListCupforms.Count.ToString());
        }

        public List<Baseplate> ListBaseplates { get; set; }

        public List<Ring> ListRings { get; set; }

        public List<InsertPlate> ListInsertPlates { get; set; }

        public List<Core> ListCores { get; set; }

        public List<Bolt> ListBolts { get; set; }

        public List<SingleMoldDisc> ListSingleMoldDiscs { get; set; }

        public List<SingleMoldCup> ListSingleMoldCups { get; set; }

        public List<CoreSingleMold> ListCoresSingleMold { get; set; }

        public List<Cupform> ListCupforms { get; set; }

        private ProductDisc ProductDisc { get; set; }

        private ProductCup ProductCup { get; set; }

        private ToleranceSettings ToleranceSettings { get; set; } = new ToleranceSettings();

        /// <summary>
        /// Connects to the database context and gets filtered list of all multi mold components.
        /// </summary>
        private void GetFilteredMultiMoldDiscComponents()
        {
            if (this.ProductDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var baseplate in db.Baseplates)
                    {
                        if (this.ProductDisc.ModularMoldDimensions.OuterDiameter < baseplate.OuterDiameter)
                        {
                            this.ListBaseplates.Add(baseplate);
                            Log.Info($"Added baseplate: {baseplate}");
                        }
                        else
                        {
                            Log.Info($"Removed baseplate: {baseplate} by {this.ProductDisc.ModularMoldDimensions.OuterDiameter - baseplate.OuterDiameter}");
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.ProductDisc.ModularMoldDimensions.InnerDiameter >= ring.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                            || this.ProductDisc.ModularMoldDimensions.OuterDiameter <= ring.InnerDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN)
                        {
                            this.ListRings.Add(ring);
                            Log.Info($"Added ring: {ring}");
                        }
                        else
                        {
                            Log.Info($"Removed ring: {ring} by {this.ProductDisc.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter} / {this.ProductDisc.ModularMoldDimensions.OuterDiameter - ring.InnerDiameter}");
                        }
                    }

                    // no filter for insertplates
                    foreach (var insertPlate in db.InsertPlates)
                    {
                        this.ListInsertPlates.Add(insertPlate);
                    }

                    foreach (var core in db.Cores)
                    {
                        if (this.ProductDisc.ModularMoldDimensions.InnerDiameter >= core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                        {
                            this.ListCores.Add(core);
                            Log.Info($"Added core: {core}");
                        }
                        else
                        {
                            Log.Info($"Removed core: {core} by {this.ProductDisc.ModularMoldDimensions.InnerDiameter - core.OuterDiameter}");
                        }
                    }

                    foreach (var bolt in db.Bolts)
                    {
                        if (bolt.OuterDiameter <= this.ProductDisc.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter)
                        {
                            this.ListBolts.Add(bolt);
                            Log.Info($"Added bolt: {bolt}");
                        }
                        else
                        {
                            Log.Info($"Removed bolt: {bolt} by {bolt.OuterDiameter - this.ProductDisc.ModularMoldDimensions.HcHoleDiameter}");
                        }
                    }
                }
            }
        }

        private void GetFilteredMultiMoldCupComponents()
        {
            using (var db = new GießformDBContext())
            {
                foreach (var cupform in db.Cupforms)
                {
                    if (this.ProductCup.ModularMoldDimensions.InnerDiameter >= cupform.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                        && cupform.CupType.Equals(this.ProductCup.CupType)
                        && cupform.Size == this.ProductCup.Size)
                    {
                        this.ListCupforms.Add(cupform);
                        Log.Info($"Added cupform: {cupform}");
                    }
                    else
                    {
                        Log.Info($"Removed cupform: {cupform} by {cupform?.InnerDiameter - this.ProductCup?.InnerDiameter} / {cupform?.CupType} != {this.ProductCup?.CupType}");
                    }
                }

                foreach (var ring in db.Rings)
                {
                    if (this.ProductCup.ModularMoldDimensions.InnerDiameter >= ring.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                    {
                        this.ListRings.Add(ring);
                        Log.Info($"Added ring: {ring}");
                    }
                    else
                    {
                        Log.Info($"Removed ring: {ring}");
                    }
                }

                // no filter for insertplates
                foreach (var insertPlate in db.InsertPlates)
                {
                    this.ListInsertPlates.Add(insertPlate);
                }

                foreach (var core in db.Cores)
                {
                    if (this.ProductCup.ModularMoldDimensions.InnerDiameter >= core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                    {
                        this.ListCores.Add(core);
                        Log.Info($"Added core: {core}");
                    }
                    else
                    {
                        Log.Info($"Removed core: {core} by {this.ProductCup.ModularMoldDimensions.InnerDiameter - core.OuterDiameter}");
                    }
                }

                foreach (var bolt in db.Bolts)
                {
                    this.ListBolts.Add(bolt);

                    // Filter für Bolzen wird voraussichtlich nicht benötigt.
                    //
                    /*if (bolt.OuterDiameter <= this.ProductCup.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter)
                    {
                        this.ListBolts.Add(bolt);
                        Log.Info($"Added bolt: {bolt}");
                    }
                    else
                    {
                        Log.Info($"Removed bolt: {bolt} by {bolt.OuterDiameter - this.ProductCup.ModularMoldDimensions.HcHoleDiameter}");
                    }*/
                }
            }
        }

        /// <summary>
        /// Connects to the database context and gets filtered list of all singleMolds and singleMoldComponents.
        /// </summary>
        private void GetFilteredSingleMoldDiscs()
        {
            using (var db = new GießformDBContext())
            {
                foreach (var singleMoldDisc in db.SingleMoldDiscs)
                {
                    if (this.ProductDisc.SingleMoldDimensions.OuterDiameter <= singleMoldDisc.OuterDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                        && this.ProductDisc.SingleMoldDimensions.InnerDiameter >= singleMoldDisc.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                        && this.ProductDisc.SingleMoldDimensions.Height <= singleMoldDisc.Height + this.ToleranceSettings.Product_Height_MIN)
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
                    if (this.ProductDisc?.SingleMoldDimensions.InnerDiameter >= coreSingleMold.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
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

        private void GetFilteredSingleMoldCups()
        {
            using (var db = new GießformDBContext())
            {
                foreach (var singleMoldCup in db.SingleMoldCups)
                {
                    if (this.ProductCup.CupType.Equals(singleMoldCup.CupType)
                        && this.ProductCup.Size == singleMoldCup.Size
                        && singleMoldCup.CoreSingleMold == null && this.ProductCup.SingleMoldDimensions.InnerDiameter >= singleMoldCup.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                    {
                        this.ListSingleMoldCups.Add(singleMoldCup);
                        Log.Info($"Added singleMoldDisc: {singleMoldCup}");
                    }
                    else
                    {
                        Log.Info($"Removed singleMoldDisc: {singleMoldCup}");
                    }
                }

                foreach (var coreSingleMold in db.CoreSingleMolds)
                {
                    if (this.ProductCup.SingleMoldDimensions.InnerDiameter >= coreSingleMold.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                    {
                        this.ListCoresSingleMold.Add(coreSingleMold);
                        Log.Info($"Added coreSingleMold: {coreSingleMold}");
                    }
                    else
                    {
                        Log.Info($"Removed coreSingleMold: {coreSingleMold}");
                    }
                }
            }
        }
    }
}
