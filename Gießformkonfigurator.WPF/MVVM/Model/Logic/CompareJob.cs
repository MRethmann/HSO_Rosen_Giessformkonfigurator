//-----------------------------------------------------------------------
// <copyright file="CompareJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System.Collections.Generic;
    using System.Linq;

    class CompareJob
    {
        public ProductDisc productDisc { get; set; }

        public ProductCup productCup { get; set; }
        public List<ModularMold> modularMolds { get; set; }
        public List<SingleMoldDisc> singleMoldDiscs { get; set; }
        public List<SingleMoldCup> singleMoldCups { get; set; }
        public List<Bolt> bolts { get; set; }
        public List<CompareObject> compareJobOutput { get; set; } = new List<CompareObject>();

        public ToleranceSettings toleranceSettings { get; set; } = new ToleranceSettings();

        public CompareJob(Product product, CombinationJob combinationJob)
        {
            if (product is ProductCup)
            {
                this.productCup = (ProductCup) product;
            }   
            else if (product is ProductDisc)
            {
                this.productDisc = (ProductDisc) product;
            }

            modularMolds = new List<ModularMold>(combinationJob.modularMoldsOutput);

            singleMoldDiscs = new List<SingleMoldDisc>(combinationJob.singleMoldDiscOutput);

            singleMoldCups = new List<SingleMoldCup>(combinationJob.singleMoldCupOutput);

            bolts = new List<Bolt>(combinationJob.listBolts);

            // Contains the final Output of Compareobjects with ranking parameters and bolts
            compareJobOutput = new List<CompareObject>();

            if (productCup != null)
            {
                this.CompareCupProduct();
            }
            else if (productDisc != null)
            {
                this.CompareDiscProduct();
            }
        }

        public void CompareDiscProduct()
        {
            CompareRuleSet compareRuleSet = new CompareRuleSet();

            // Contains all CompareObjects without any bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // First Step: Compare ModularMolds with Product. Add ranking Information
            foreach (var modularMold in this.modularMolds)
            {
                if (compareRuleSet.Compare(this.productDisc, modularMold))
                {
                    var compareObject = new CompareObject((ProductDisc) this.productDisc, (ModularMold) modularMold);
                    if (modularMold.ListCoreRings.Count > 0)
                    {
                        compareObject.differenceInnerDiameter = modularMold.ListCoreRings.Min(p => p.Item3);
                        compareObject.differenceInnerDiameter = compareObject.differenceInnerDiameter > 0 ? compareObject.differenceInnerDiameter : 0;
                    }
                    else
                    {
                        compareObject.differenceInnerDiameter = (productDisc.InnerDiameter - modularMold.core.OuterDiameter) > 0 ? productDisc.InnerDiameter - modularMold.core.OuterDiameter : 0;
                    }

                    if (modularMold.ListOuterRings.Count > 0)
                    {
                        compareObject.differenceOuterDiameter = modularMold.ListOuterRings.Min(p => p.Item3);
                        compareObject.differenceOuterDiameter = compareObject.differenceOuterDiameter > 0 ? compareObject.differenceOuterDiameter : 0;
                    }
                    else
                    {
                        compareObject.differenceOuterDiameter = (modularMold.guideRing.InnerDiameter - productDisc.OuterDiameter) > 0 ? modularMold.guideRing.InnerDiameter - productDisc.OuterDiameter : 0;
                    }

                    compareObjectsTemp01.Add(compareObject);
                }
            }

            // Second Step: Compare SingleMolds with Product.
            foreach (var singleMoldDisc in this.singleMoldDiscs)
            {
                if (singleMoldDisc.coreSingleMold != null)
                {
                    if (compareRuleSet.Compare(this.productDisc, singleMoldDisc) 
                        && singleMoldDisc.coreSingleMold?.OuterDiameter <= productDisc.InnerDiameter)
                    {
                        var compareObject = new CompareObject((ProductDisc)this.productDisc, (SingleMoldDisc)singleMoldDisc);
                        compareObject.differenceInnerDiameter = (productDisc.InnerDiameter - singleMoldDisc.coreSingleMold.OuterDiameter) > 0 ? productDisc.InnerDiameter - singleMoldDisc.coreSingleMold.OuterDiameter : 0;
                        compareObject.differenceOuterDiameter = (singleMoldDisc.OuterDiameter - productDisc.OuterDiameter) > 0 ? singleMoldDisc.OuterDiameter - productDisc.OuterDiameter : 0;
                        compareJobOutput.Add(compareObject);
                    }
                }
                else
                {
                    if (compareRuleSet.Compare(this.productDisc, singleMoldDisc))
                    {
                        var compareObject = new CompareObject((ProductDisc)this.productDisc, (SingleMoldDisc)singleMoldDisc);
                        compareObject.differenceInnerDiameter = (productDisc.InnerDiameter - singleMoldDisc.InnerDiameter) > 0 ? productDisc.InnerDiameter - singleMoldDisc.InnerDiameter : 0;
                        compareObject.differenceOuterDiameter = (singleMoldDisc.OuterDiameter - productDisc.OuterDiameter) > 0 ? singleMoldDisc.OuterDiameter - productDisc.OuterDiameter : 0;
                        compareJobOutput.Add(compareObject);
                    }
                }
            }

            // Third Step: Compare ModularMolds with HoleCircle
            foreach (var compareObject in compareObjectsTemp01)
            {
                // Grundplatten
                if (this.productDisc.HcDiameter > 0)
                {
                    if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[1] = true;
                    }
                    else if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).baseplate.Hc2Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[2] = true;
                    }
                    else if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).baseplate.Hc3Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[3] = true;
                    }
                }


                // Einlegeplatten
                if (this.productDisc.HcDiameter > 0)
                {
                    if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Holes)
                    {
                        compareObject.boltCirclesInsertPlate[1] = true;
                    }
                    else if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Holes)
                    {
                        compareObject.boltCirclesInsertPlate[2] = true;
                    }
                    else if (this.productDisc.HcDiameter <= ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter + toleranceSettings?.hc_Diameter
                            && this.productDisc.HcDiameter >= ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter - toleranceSettings?.hc_Diameter
                            && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Holes)
                    {
                        compareObject.boltCirclesInsertPlate[3] = true;
                    }
                }
            }

            // Fourth Step: Compare ModularMolds with Bolts
            foreach (var compareObject in compareObjectsTemp01)
            {
                foreach (var bolt in this.bolts)
                {
                    // Baseplate
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.boltCirclesBaseplate[i] == true)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.HcHoleDiameter + toleranceSettings.bolt_Diameter
                                && bolt.OuterDiameter >= this.productDisc.HcHoleDiameter - toleranceSettings.bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).baseplate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, decimal?>(bolt, this.productDisc.HcHoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }

                    // Insertplate
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.boltCirclesInsertPlate[i] == true)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.HcHoleDiameter + toleranceSettings.bolt_Diameter
                                && bolt.OuterDiameter >= this.productDisc.HcHoleDiameter - toleranceSettings.bolt_Diameter
                                && bolt.Thread == ((ModularMold)compareObject.Mold).insertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).insertPlate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, decimal?>(bolt, this.productDisc.HcHoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }
                }
                this.compareJobOutput.Add(compareObject);
            }
        }

        public void CompareCupProduct()
        {
            CompareRuleSet compareRuleSet = new CompareRuleSet();

            // Contains all CompareObjects without any bolts
            List<CompareObject> compareObjectsTemp01 = new List<CompareObject>();

            // First Step: Compare ModularMolds with Product. Add ranking Information
            foreach (var modularMold in this.modularMolds)
            {
                if (compareRuleSet.Compare(this.productCup, modularMold))
                {
                    var compareObject = new CompareObject((ProductCup) this.productCup, (ModularMold) modularMold);
                    compareObject.differenceInnerDiameter = productCup.InnerDiameter - modularMold.core.OuterDiameter;
                    compareObjectsTemp01.Add(compareObject);
                }
            }
        }
    }
}
