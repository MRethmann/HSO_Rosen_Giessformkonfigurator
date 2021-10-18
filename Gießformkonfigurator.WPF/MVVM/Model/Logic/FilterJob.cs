//-----------------------------------------------------------------------
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

            this.GetFilteredMultiMoldDiscComponents();

            this.GetFilteredSingleMoldDiscs();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterJob"/> class.
        /// </summary>
        /// <param name="productCup">Expects product Cup.</param>
        public FilterJob(ProductCup productCup)
        {
            this.ProductCup = productCup;

            this.GetFilteredMultiMoldCupComponents();

            this.GetFilteredSingleMoldCups();
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
        /// Connects to the database context and gets filtered list of all multi mold components.
        /// </summary>
        public void GetFilteredMultiMoldDiscComponents()
        {
            if (this.ProductDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var baseplate in db.Baseplates)
                    {
                        if (this.ProductDisc?.ModularMoldDimensions.OuterDiameter < baseplate.OuterDiameter)
                        {
                            this.ListBaseplates.Add(baseplate);
                            Log.Info($"Added baseplate: {baseplate}");
                        }
                        else
                        {
                            Log.Info($"Removed baseplate: {baseplate} by {this.ProductDisc?.ModularMoldDimensions.OuterDiameter - baseplate.OuterDiameter}");
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if (this.ProductDisc?.ModularMoldDimensions.InnerDiameter >= ring.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                            || this.ProductDisc?.ModularMoldDimensions.OuterDiameter <= ring.InnerDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN)
                        {
                            this.ListRings.Add(ring);
                            Log.Info($"Added ring: {ring}");
                        }
                        else
                        {
                            Log.Info($"Removed baseplate: {ring} by {this.ProductDisc?.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter} / {this.ProductDisc?.ModularMoldDimensions.OuterDiameter - ring.InnerDiameter}");
                        }
                    }

                    // no filter for insertplates
                    foreach (var insertPlate in db.InsertPlates)
                    {
                        this.ListInsertPlates.Add(insertPlate);
                    }

                    foreach (var core in db.Cores)
                    {
                        if (this.ProductDisc?.ModularMoldDimensions.InnerDiameter >= core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                        {
                            this.ListCores.Add(core);
                            Log.Info($"Added core: {core}");
                        }
                        else
                        {
                            Log.Info($"Removed core: {core} by {this.ProductDisc?.ModularMoldDimensions.InnerDiameter - core.OuterDiameter}");
                        }
                    }

                    foreach (var bolt in db.Bolts)
                    {
                        if (bolt.OuterDiameter <= this.ProductDisc?.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter)
                        {
                            this.ListBolts.Add(bolt);
                            Log.Info($"Added bolt: {bolt}");
                        }
                        else
                        {
                            Log.Info($"Removed bolt: {bolt} by {bolt.OuterDiameter - this.ProductDisc?.ModularMoldDimensions.HcHoleDiameter}");
                        }
                    }
                }
            }
        }

        public void GetFilteredMultiMoldCupComponents()
        {
            using (var db = new GießformDBContext())
            {
                foreach (var cupform in db.Cupforms)
                {
                    if (cupform.InnerDiameter <= this.ProductCup?.ModularMoldDimensions?.InnerDiameter
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
            }
        }

        /// <summary>
        /// Connects to the database context and gets filtered list of all singleMolds and singleMoldComponents.
        /// </summary>
        public void GetFilteredSingleMoldDiscs()
        {
            using (var db = new GießformDBContext())
            {
                foreach (var singleMoldDisc in db.SingleMoldDiscs)
                {
                    if (this.ProductDisc.SingleMoldDimensions.OuterDiameter <= singleMoldDisc.OuterDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                        && this.ProductDisc.SingleMoldDimensions.InnerDiameter >= singleMoldDisc.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                        && this.ProductDisc.SingleMoldDimensions.Height <= singleMoldDisc.Height)
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

        public void GetFilteredSingleMoldCups()
        {
            //throw new NotImplementedException();
        }
    }
}
