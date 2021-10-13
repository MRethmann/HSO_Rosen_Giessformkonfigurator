//-----------------------------------------------------------------------
// <copyright file="CompareJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Compares singleMolds and multiMolds with the given product to see if they are matching.
    /// </summary>
    public class CompareJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareJob"/> class.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="combinationJob"></param>
        public CompareJob(Product product, CombinationJob combinationJob)
        {
            if (product is ProductCup)
            {
                this.ProductCup = (ProductCup)product;
            }
            else if (product is ProductDisc)
            {
                this.ProductDisc = (ProductDisc)product;
            }

            this.ModularMolds = new List<ModularMold>(combinationJob.ModularMoldsOutput);

            this.SingleMoldDiscs = new List<SingleMoldDisc>(combinationJob.SingleMoldDiscOutput);

            this.SingleMoldCups = new List<SingleMoldCup>(combinationJob.SingleMoldCupOutput);

            this.Bolts = new List<Bolt>(combinationJob.ListBolts);

            // Contains the final Output of Compareobjects with ranking parameters and bolts
            this.CompareJobOutput = new List<CompareObject>();

            if (this.ProductCup != null)
            {
                this.CompareCupProduct();
            }
            else if (this.ProductDisc != null)
            {
                this.CompareDiscProduct();
            }
        }

        public ProductDisc ProductDisc { get; set; }

        public ProductCup ProductCup { get; set; }

        public List<ModularMold> ModularMolds { get; set; }

        public List<SingleMoldDisc> SingleMoldDiscs { get; set; }

        public List<SingleMoldCup> SingleMoldCups { get; set; }

        public List<Bolt> Bolts { get; set; }

        public List<CompareObject> CompareJobOutput { get; set; } = new List<CompareObject>();

        public ToleranceSettings ToleranceSettings { get; set; } = new ToleranceSettings();

        public void CompareDiscProduct()
        {
            CompareRuleSet compareRuleSet = new CompareRuleSet();

            // Contains all CompareObjects without the bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // First Step: Compare ModularMolds with Product. Add ranking Information
            foreach (var modularMold in this.ModularMolds)
            {
                if (compareRuleSet.Compare(this.ProductDisc, modularMold))
                {
                    var compareObject = new CompareObject((ProductDisc) this.ProductDisc, (ModularMold) modularMold);
                    if (modularMold.ListCoreRings.Count > 0)
                    {
                        compareObject.DifferenceInnerDiameter = modularMold.ListCoreRings.Min(p => p.Item3);
                        compareObject.DifferenceInnerDiameter = compareObject.DifferenceInnerDiameter > 0 ? compareObject.DifferenceInnerDiameter : 0;
                    }
                    else
                    {
                        compareObject.DifferenceInnerDiameter = (this.ProductDisc.InnerDiameter - modularMold.Core.OuterDiameter) > 0 ? this.ProductDisc.InnerDiameter - modularMold.Core.OuterDiameter : 0;
                    }

                    if (modularMold.ListOuterRings.Count > 0)
                    {
                        compareObject.DifferenceOuterDiameter = modularMold.ListOuterRings.Min(p => p.Item3);
                        compareObject.DifferenceOuterDiameter = compareObject.DifferenceOuterDiameter > 0 ? compareObject.DifferenceOuterDiameter : 0;
                    }
                    else
                    {
                        compareObject.DifferenceOuterDiameter = (modularMold.GuideRing.InnerDiameter - this.ProductDisc.OuterDiameter) > 0 ? modularMold.GuideRing.InnerDiameter - this.ProductDisc.OuterDiameter : 0;
                    }

                    compareObjectsTemp01.Add(compareObject);
                }
            }

            // Second Step: Compare SingleMolds with Product. Add rating information.
            foreach (var singleMoldDisc in this.SingleMoldDiscs)
            {
                if (singleMoldDisc.CoreSingleMold != null)
                {
                    if (compareRuleSet.Compare(this.ProductDisc, singleMoldDisc) 
                        && singleMoldDisc.CoreSingleMold?.OuterDiameter <= this.ProductDisc.InnerDiameter)
                    {
                        var compareObject = new CompareObject((ProductDisc)this.ProductDisc, (SingleMoldDisc)singleMoldDisc);
                        compareObject.DifferenceInnerDiameter = (this.ProductDisc.InnerDiameter - singleMoldDisc.CoreSingleMold.OuterDiameter) > 0 ? this.ProductDisc.InnerDiameter - singleMoldDisc.CoreSingleMold.OuterDiameter : 0;
                        compareObject.DifferenceOuterDiameter = (singleMoldDisc.OuterDiameter - this.ProductDisc.OuterDiameter) > 0 ? singleMoldDisc.OuterDiameter - this.ProductDisc.OuterDiameter : 0;
                        this.CompareJobOutput.Add(compareObject);
                    }
                }
                else
                {
                    if (compareRuleSet.Compare(this.ProductDisc, singleMoldDisc))
                    {
                        var compareObject = new CompareObject((ProductDisc)this.ProductDisc, (SingleMoldDisc)singleMoldDisc);
                        compareObject.DifferenceInnerDiameter = (this.ProductDisc.InnerDiameter - singleMoldDisc.InnerDiameter) > 0 ? this.ProductDisc.InnerDiameter - singleMoldDisc.InnerDiameter : 0;
                        compareObject.DifferenceOuterDiameter = (singleMoldDisc.OuterDiameter - this.ProductDisc.OuterDiameter) > 0 ? singleMoldDisc.OuterDiameter - this.ProductDisc.OuterDiameter : 0;
                        this.CompareJobOutput.Add(compareObject);
                    }
                }
            }

            // Third Step: Compare ModularMolds with HoleCircle
            foreach (var compareObject in compareObjectsTemp01)
            {
                // Baseplates
                if (this.ProductDisc.HcDiameter > 0)
                {
                    if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc1Holes)
                    {
                        compareObject.BoltCirclesBaseplate[1] = true;
                    }
                    else if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc2Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc2Holes)
                    {
                        compareObject.BoltCirclesBaseplate[2] = true;
                    }
                    else if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).Baseplate.Hc3Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).Baseplate.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).Baseplate.Hc3Holes)
                    {
                        compareObject.BoltCirclesBaseplate[3] = true;
                    }
                }


                // InsertPlates
                if (this.ProductDisc.HcDiameter > 0)
                {
                    if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc1Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[1] = true;
                    }
                    else if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc2Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[2] = true;
                    }
                    else if (this.ProductDisc.HcDiameter <= ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Diameter + this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcDiameter >= ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Diameter - this.ToleranceSettings?.Hc_Diameter
                            && this.ProductDisc.HcHoles == ((ModularMold)compareObject.Mold).InsertPlate?.Hc3Holes)
                    {
                        compareObject.BoltCirclesInsertPlate[3] = true;
                    }
                }
            }

            // Fourth Step: Compare ModularMolds with Bolts
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
                            if (bolt.OuterDiameter <= this.ProductDisc.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter
                                && bolt.OuterDiameter >= this.ProductDisc.HcHoleDiameter - this.ToleranceSettings.Bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).Baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).Baseplate).ToString())
                            {
                                compareObject.Bolts.Add(new System.Tuple<Bolt, decimal?>(bolt, this.ProductDisc.HcHoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }

                    // Insertplates
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.BoltCirclesInsertPlate[i] == true)
                        {
                            if (bolt.OuterDiameter <= this.ProductDisc.HcHoleDiameter + this.ToleranceSettings.Bolt_Diameter
                                && bolt.OuterDiameter >= this.ProductDisc.HcHoleDiameter - this.ToleranceSettings.Bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).InsertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).InsertPlate).ToString())
                            {
                                compareObject.Bolts.Add(new System.Tuple<Bolt, decimal?>(bolt, this.ProductDisc.HcHoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }
                }
                this.CompareJobOutput.Add(compareObject);
            }
        }

        public void CompareCupProduct()
        {
            CompareRuleSet compareRuleSet = new CompareRuleSet();

            // Contains all CompareObjects without any bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // First Step: Compare ModularMolds with Product. Add rating Information.
            foreach (var modularMold in this.ModularMolds)
            {
                if (compareRuleSet.Compare(this.ProductCup, modularMold))
                {
                    var compareObject = new CompareObject((ProductCup) this.ProductCup, (ModularMold) modularMold);
                    compareObject.DifferenceInnerDiameter = this.ProductCup.InnerDiameter - modularMold.Core.OuterDiameter;
                    compareObjectsTemp01.Add(compareObject);
                }
            }
        }
    }
}
