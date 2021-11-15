//-----------------------------------------------------------------------
// <copyright file="CompareJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using log4net;

    /// <summary>
    /// Compares singleMolds and multiMolds with the given product to see if they are matching.
    /// </summary>
    public class CompareJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CompareJob));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareJob"/> class.
        /// </summary>
        /// <param name="productDisc">Expects Disc product.</param>
        /// <param name="combinationJob">Receives combinationJobOutput.</param>
        public CompareJob(ProductDisc productDisc, CombinationJob combinationJob)
        {
            this.ProductDisc = productDisc;

            this.ModularMoldDiscs = new List<ModularMold>(combinationJob.ModularMoldDiscOutput);
            this.SingleMoldDiscs = new List<SingleMoldDisc>(combinationJob.SingleMoldDiscOutput);
            this.Bolts = new List<Bolt>(combinationJob.ListBolts);

            this.CompareDiscProductModularMold();
            this.CompareDiscProductSingleMold();

            Log.Info("Anzahl compareObjects: " + this.CompareJobOutput.Count.ToString());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareJob"/> class.
        /// </summary>
        /// <param name="productCup">Expects Cup product.</param>
        /// <param name="combinationJob">Receives combinationJobOutput.</param>
        public CompareJob(ProductCup productCup, CombinationJob combinationJob)
        {
            this.ProductCup = productCup;

            this.ModularMoldCups = new List<ModularMold>(combinationJob.ModularMoldCupOutput);
            this.SingleMoldCups = new List<SingleMoldCup>(combinationJob.SingleMoldCupOutput);

            this.CompareCupProductModularMold();
            this.CompareCupProductSingleMold();
        }

        public List<CompareObject> CompareJobOutput { get; set; } = new List<CompareObject>();

        private ProductDisc ProductDisc { get; set; }

        private ProductCup ProductCup { get; set; }

        private List<ModularMold> ModularMoldDiscs { get; set; }

        private List<ModularMold> ModularMoldCups { get; set; }

        private List<SingleMoldDisc> SingleMoldDiscs { get; set; }

        private List<SingleMoldCup> SingleMoldCups { get; set; }

        private List<Bolt> Bolts { get; set; }

        private CompareRuleSet CompareRuleSet { get; set; } = new CompareRuleSet();

        private ToleranceSettings ToleranceSettings { get; set; } = new ToleranceSettings();

        /// <summary>
        /// Compares ProductDiscs with modularMolds.
        /// 1. General comparison to check if product and mold would fit. Also notes differences.
        /// 2. Comparison to determine if BTCs would fit and notes down which ones.
        /// 3. Gets fitting bolts for the used BTCs.
        /// </summary>
        private void CompareDiscProductModularMold()
        {
            // Contains all CompareObjects without the bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // Compare ModularMolds with Product. Add ranking Information
            foreach (var modularMold in this.ModularMoldDiscs)
            {
                if (this.CompareRuleSet.Compare(this.ProductDisc, modularMold))
                {
                    var compareObject = new CompareObject((ProductDisc)this.ProductDisc, (ModularMold)modularMold);
                    if (modularMold.ListCoreRings.Count > 0)
                    {
                        compareObject.DifferenceInnerDiameter = modularMold.ListCoreRings.Min(p => p.Item3);
                        // compareObject.DifferenceInnerDiameter = compareObject.DifferenceInnerDiameter > 0 ? compareObject.DifferenceInnerDiameter : 0;
                    }
                    else
                    {
                        compareObject.DifferenceInnerDiameter = this.ProductDisc.ModularMoldDimensions.InnerDiameter - modularMold.Core.OuterDiameter;
                    }

                    if (modularMold.ListOuterRings.Count > 0)
                    {
                        compareObject.DifferenceOuterDiameter = modularMold.ListOuterRings.Min(p => p.Item3);
                        // compareObject.DifferenceOuterDiameter = compareObject.DifferenceOuterDiameter > 0 ? compareObject.DifferenceOuterDiameter : 0;
                    }
                    else
                    {
                        compareObject.DifferenceOuterDiameter = modularMold.GuideRing.InnerDiameter - this.ProductDisc.ModularMoldDimensions.OuterDiameter;
                    }

                    compareObjectsTemp01.Add(compareObject);
                }
            }

            // Compare ModularMolds with HoleCircle
            foreach (var compareObject in compareObjectsTemp01)
            {
                // Baseplates
                if (this.ProductDisc.HcDiameter > 0)
                {
                    if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc1Holes)
                    {
                        compareObject.BoltCirclesBaseplate[1] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                    else if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc2Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc2Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc2Holes)
                    {
                        compareObject.BoltCirclesBaseplate[2] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                    else if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc3Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc3Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc3Holes)
                    {
                        compareObject.BoltCirclesBaseplate[3] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                }

                // InsertPlates
                if (this.ProductDisc.HcDiameter > 0)
                {
                    if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[1] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                    else if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[2] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                    else if (this.ProductDisc.ModularMoldDimensions.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.ModularMoldDimensions.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[3] = true;
                        compareObject.Mold.HasFittingBTC = true;
                    }
                }
            }

            // Compare ModularMolds with Bolts
            foreach (var compareObject in compareObjectsTemp01)
            {
                foreach (var bolt in this.Bolts)
                {
                    // Baseplates
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.BoltCirclesBaseplate[i] == true)
                        {
                            if (bolt.OuterDiameter <= this.ProductDisc.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter
                                && bolt.OuterDiameter >= this.ProductDisc.ModularMoldDimensions.HcHoleDiameter - this.ToleranceSettings.Bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).Baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).Baseplate).ToString())
                            {
                                compareObject.Bolts.Add(bolt);
                            }
                        }
                    }

                    // Insertplates
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.BoltCirclesInsertPlate[i] == true)
                        {
                            if (bolt.OuterDiameter <= this.ProductDisc.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter
                                && bolt.OuterDiameter >= this.ProductDisc.ModularMoldDimensions.HcHoleDiameter - this.ToleranceSettings.Bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).InsertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).InsertPlate).ToString())
                            {
                                compareObject.Bolts.Add(bolt);
                            }
                        }
                    }
                }
            }

            this.CompareJobOutput.AddRange(compareObjectsTemp01);
        }

        private void CompareDiscProductSingleMold()
        {
            // Compare SingleMolds with Product. Add rating information.
            foreach (var singleMoldDisc in this.SingleMoldDiscs)
            {
                if (this.CompareRuleSet.Compare(this.ProductDisc, singleMoldDisc))
                {
                    var compareObject = new CompareObject(this.ProductDisc, singleMoldDisc);
                    if (singleMoldDisc.CoreSingleMold != null)
                    {
                        compareObject.DifferenceInnerDiameter = this.ProductDisc.SingleMoldDimensions.InnerDiameter - singleMoldDisc.CoreSingleMold.OuterDiameter;
                        compareObject.DifferenceOuterDiameter = singleMoldDisc.OuterDiameter - this.ProductDisc.SingleMoldDimensions.OuterDiameter;
                    }
                    else
                    {
                        compareObject.DifferenceInnerDiameter = this.ProductDisc.SingleMoldDimensions.InnerDiameter - singleMoldDisc.InnerDiameter;
                        compareObject.DifferenceOuterDiameter = singleMoldDisc.OuterDiameter - this.ProductDisc.SingleMoldDimensions.OuterDiameter;
                    }

                    this.CompareJobOutput.Add(compareObject);
                }
            }
        }

        /// <summary>
        /// TODO: Add logic to compare BTCs from insertPlates.
        /// </summary>
        private void CompareCupProductModularMold()
        {
            CompareRuleSet compareRuleSet = new CompareRuleSet();

            // Contains all CompareObjects without any bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // Compare ModularMolds with Product. Add rating Information.
            foreach (var modularMold in this.ModularMoldCups)
            {
                if (compareRuleSet.Compare(this.ProductCup, modularMold))
                {
                    var compareObject = new CompareObject(this.ProductCup, modularMold);
                    compareObject.DifferenceInnerDiameter = modularMold.Core != null ? this.ProductCup.ModularMoldDimensions.InnerDiameter - modularMold.Core.OuterDiameter : this.ProductCup.ModularMoldDimensions.InnerDiameter - modularMold.Cupform.InnerDiameter;
                    compareObjectsTemp01.Add(compareObject);
                }
            }

            // Compare ModularMolds with HoleCircle and add fitting bolts.
            foreach (var compareObject in compareObjectsTemp01)
            {
                // Baseplates
                if (this.ProductCup.BTC != null)
                {
                    var cupformBTCList = new List<Tuple<string, string>>()
                    { new Tuple<string, string>(((ModularMold)compareObject.Mold).Cupform.BTC1, ((ModularMold)compareObject.Mold).Cupform.BTC1Thread),
                      new Tuple<string, string>(((ModularMold)compareObject.Mold).Cupform.BTC2, ((ModularMold)compareObject.Mold).Cupform.BTC2Thread),
                      new Tuple<string, string>(((ModularMold)compareObject.Mold).Cupform.BTC3, ((ModularMold)compareObject.Mold).Cupform.BTC3Thread),
                    };

                    // var insertPlateBTCList = new List<string>() { ((ModularMold)compareObject.Mold).InsertPlate.BTC}
                    string btcThread;
                    for (int i = 0; i < 3; i++)
                    {
                        if (cupformBTCList[i].Item1 != null
                            && cupformBTCList[i].Item1.Equals(this.ProductCup.BTC))
                        {
                            btcThread = cupformBTCList[i].Item2;
                            compareObject.Mold.HasFittingBTC = true;

                            foreach (var bolt in this.Bolts)
                            {
                                if (bolt.OuterDiameter <= this.ProductCup.ModularMoldDimensions.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter
                                    && bolt.OuterDiameter >= this.ProductCup.ModularMoldDimensions.HcHoleDiameter - this.ToleranceSettings.Bolt_Diameter
                                    && bolt.Thread == btcThread)
                                {
                                    compareObject.Bolts.Add(bolt);
                                }
                            }
                        }
                    }

                    // TODO: Add BTC and Bolt logic for insertplates.
                }
            }

            this.CompareJobOutput.AddRange(compareObjectsTemp01);
        }

        private void CompareCupProductSingleMold()
        {
            foreach (var singleMoldCup in this.SingleMoldCups)
            {
                if (this.CompareRuleSet.Compare(this.ProductCup, singleMoldCup))
                {
                    var compareObject = new CompareObject(this.ProductCup, singleMoldCup);
                    if (singleMoldCup.CoreSingleMold != null)
                    {
                        compareObject.DifferenceInnerDiameter = this.ProductCup.SingleMoldDimensions.InnerDiameter - singleMoldCup.CoreSingleMold.OuterDiameter;
                    }
                    else
                    {
                        compareObject.DifferenceInnerDiameter = this.ProductCup.SingleMoldDimensions.InnerDiameter - singleMoldCup.InnerDiameter;
                    }

                    this.CompareJobOutput.Add(compareObject);
                }
            }
        }
    }
}
