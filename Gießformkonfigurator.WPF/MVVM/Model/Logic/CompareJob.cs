//-----------------------------------------------------------------------
// <copyright file="CompareJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System.Collections.Generic;
    class CompareJob
    {
        public ProductDisc productDisc { get; set; }

        public ProductCup productCup { get; set; }
        public List<ModularMold> modularMolds { get; set; }
        public List<SingleMold> singleMolds { get; set; }
        public List<Bolt> bolts { get; set; }
        public List<CompareObject> compareJobOutput { get; set; } = new List<CompareObject>();

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

            singleMolds = new List<SingleMold>();

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
                    compareObject.differenceOuterDiameter = modularMold.guideRing.InnerDiameter - productDisc.OuterDiameter;
                    compareObject.differenceInnerDiameter = productDisc.InnerDiameter - modularMold.core.OuterDiameter;
                    //compareObject.differenceHeight = ;
                    compareObjectsTemp01.Add(compareObject);
                }
            }

            // Second Step: Compare SingleMolds with Product. Add ranking Information
            foreach (var singleMold in this.singleMolds)
            {
                // TODO
            }

            // Third Step: Compare ModularMolds with HoleCircle
            foreach (var compareObject in compareObjectsTemp01)
            {
                // Grundplatten
                if (this.productDisc.HcDiameter > 0)
                {
                    if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[1] = 1;
                    }
                    else if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).baseplate.Hc2Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[2] = 1;
                    }
                    else if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).baseplate.Hc3Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).baseplate.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[3] = 1;
                    }
                }


                // Einlegeplatten
                if (this.productDisc.HcDiameter > 0)
                {
                    if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[4] = 1;
                    }
                    else if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[5] = 1;
                    }
                    else if (this.productDisc.HcDiameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter
                        && this.productDisc.HcHoles == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[6] = 1;
                    }
                }
            }

            // Fourth Step: Compare ModularMolds with Bolts
            foreach (var compareObject in compareObjectsTemp01)
            {
                foreach (var bolt in this.bolts)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        var propGewinde = "Hc" + i + "Thread";
                        if (compareObject.boltCirclesBaseplate[i] == 1)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.HcHoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).baseplate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 1, this.productDisc.HcHoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }

                    for (int i = 4; i < 7; i++)
                    {
                        var propGewinde = "Hc" + (i - 3) + "Thread";
                        if (compareObject.boltCirclesBaseplate[i] == 1)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.HcHoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).insertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).insertPlate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 1, this.productDisc.HcHoleDiameter - bolt.OuterDiameter));
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
                    //compareObject.differenceHeight = ;
                    compareObjectsTemp01.Add(compareObject);
                }
            }
        }
    }
}
