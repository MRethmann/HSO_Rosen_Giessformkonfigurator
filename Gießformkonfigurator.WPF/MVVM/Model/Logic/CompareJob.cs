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
                if (this.productDisc.Hc1Diameter > 0)
                {
                    if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).baseplate.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[1] = 1;
                    }
                    else if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc2Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).baseplate.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[2] = 1;
                    }
                    else if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc3Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).baseplate.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[3] = 1;
                    }
                }
                
                if (this.productDisc.Hc2Diameter > 0)
                {
                    if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).baseplate.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[1] = 2;
                    }
                    else if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc2Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).baseplate.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[2] = 2;
                    }
                    else if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc3Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).baseplate.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[3] = 2;
                    }
                }
                
                if (this.productDisc.Hc3Diameter > 0)
                {
                    if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc1Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).baseplate.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[1] = 3;
                    }
                    else if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc2Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).baseplate.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[2] = 3;
                    }
                    else if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).baseplate.Hc3Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).baseplate.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[3] = 3;
                    }
                }

                // Einlegeplatten
                if (this.productDisc.Hc1Diameter > 0)
                {
                    if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[4] = 1;
                    }
                    else if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[5] = 1;
                    }
                    else if (this.productDisc.Hc1Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter
                        && this.productDisc.Hc1Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[6] = 1;
                    }
                }
                
                if (this.productDisc.Hc2Diameter > 0)
                {
                    if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[4] = 2;
                    }
                    else if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[5] = 2;
                    }
                    else if (this.productDisc.Hc2Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter
                        && this.productDisc.Hc2Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[6] = 2;
                    }
                }
                
                if (this.productDisc.Hc3Diameter > 0)
                {
                    if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc1Holes)
                    {
                        compareObject.boltCirclesBaseplate[4] = 3;
                    }
                    else if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc2Holes)
                    {
                        compareObject.boltCirclesBaseplate[5] = 3;
                    }
                    else if (this.productDisc.Hc3Diameter == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Diameter
                        && this.productDisc.Hc3Holes == ((ModularMold)compareObject.Mold).insertPlate?.Hc3Holes)
                    {
                        compareObject.boltCirclesBaseplate[6] = 3;
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
                            if (bolt.OuterDiameter <= this.productDisc.Hc1HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).baseplate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 1, this.productDisc.Hc1HoleDiameter - bolt.OuterDiameter));
                            }
                        }
                        else if (compareObject.boltCirclesBaseplate[i] == 2)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.Hc2HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).baseplate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 2, this.productDisc.Hc2HoleDiameter - bolt.OuterDiameter));
                            }
                        }
                        else if (compareObject.boltCirclesBaseplate[i] == 3)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.Hc3HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).baseplate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).baseplate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 3, this.productDisc.Hc3HoleDiameter - bolt.OuterDiameter));
                            }
                        }
                    }


                    for (int i = 4; i < 7; i++)
                    {
                        var propGewinde = "Hc" + (i - 3) + "Thread";
                        if (compareObject.boltCirclesBaseplate[i] == 1)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.Hc1HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).insertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).insertPlate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 1, this.productDisc.Hc1HoleDiameter - bolt.OuterDiameter));
                            }
                        }
                        else if (compareObject.boltCirclesBaseplate[i] == 2)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.Hc2HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).insertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).insertPlate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 2, this.productDisc.Hc2HoleDiameter - bolt.OuterDiameter));
                            }
                        }
                        else if (compareObject.boltCirclesBaseplate[i] == 3)
                        {
                            if (bolt.OuterDiameter <= this.productDisc.Hc3HoleDiameter
                            && bolt.Thread == ((ModularMold)compareObject.Mold).insertPlate.GetType().GetProperty(propGewinde).GetValue(((ModularMold)compareObject.Mold).insertPlate).ToString())
                            {
                                compareObject.bolts.Add(new System.Tuple<Bolt, int, decimal?>(bolt, 3, this.productDisc.Hc3HoleDiameter - bolt.OuterDiameter));
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
