//-----------------------------------------------------------------------
// <copyright file="RankingJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class RankingJob
    {
        public List<CompareObject> rankingJobInput { get; set; }

        public List<CompareObject> rankingJobOutput { get; set; }

        public List<CompareObject> filteredOutput { get; set; } = new List<CompareObject>();

        public ApplicationSettings applicationSettings { get; set; }

        public decimal? factorOuterDiameter { get; set; }

        public decimal? factorInnerDiameter { get; set; }

        public decimal? factorBoltDiameter { get; set; }

        public Product product { get; set; }

        public RankingJob(Product product, CompareJob compareJob)
        {
            this.product = product;
            applicationSettings = new ApplicationSettings();
            this.factorOuterDiameter = applicationSettings.rankingFactorOuterDiameter;
            this.factorInnerDiameter = applicationSettings.rankingFactorInnerDiameter;
            this.factorBoltDiameter = applicationSettings.rankingFactorBolts;
            this.rankingJobInput = new List<CompareObject>(compareJob.compareJobOutput);
            this.addRatingInformation();
            this.orderOutputData();
        }

        public decimal? compare(decimal? var1, decimal? var2)
        {
            if (var1 >= var2)
                return var1;
            else
                return var2;
        }

        /// <summary>
        /// Final Step to rate all molds based on their difference between actual Output after production and prefered Output on paper. Important Values are for example differences in outer and inner diameter and height. 
        /// </summary>
        public void addRatingInformation()
        {
            foreach (var compareObject in this.rankingJobInput)
            {
                compareObject.finalRating = this.compare(45.00m - compareObject.differenceOuterDiameter * this.factorOuterDiameter, 0.00m);
                compareObject.finalRating += this.compare(45.00m - compareObject.differenceInnerDiameter * this.factorInnerDiameter, 0.00m);

                if (compareObject.Mold is ModularMold)
                {
                    if (compareObject.Product is ProductCup && ((ProductCup)compareObject.Product).BTC != null
                        || compareObject.Product is ProductDisc && ((ProductDisc)compareObject.Product).BTC != null)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (compareObject.boltCirclesBaseplate[i] == true)
                            {
                                var minDifference = compareObject.bolts.Min(p => p.Item2);
                                compareObject.finalRating += this.compare(10.00m - minDifference * this.factorBoltDiameter, 0.00m);
                                compareObject.differenceBoltDiameter = minDifference;
                            }
                            else if (compareObject.boltCirclesInsertPlate[i] == true)
                            {
                                var minDifference = compareObject.bolts.Min(p => p.Item2);
                                compareObject.finalRating += this.compare(10.00m - minDifference * this.factorBoltDiameter, 0.00m);
                                compareObject.differenceBoltDiameter = minDifference;
                            }
                        }
                    }
                    else
                    {
                        compareObject.finalRating += 10.00m;
                    }
                }
                else if (compareObject.Mold is SingleMold)
                {
                    compareObject.finalRating += this.compare(10.00m - compareObject.differenceBoltDiameter * this.factorBoltDiameter, 0.00m);
                }

                compareObject.finalRating = Math.Round((Decimal) compareObject.finalRating, 2);

                if (compareObject.differenceInnerDiameter > 1)
                {
                    string diffInnerDiameter = Math.Round((Decimal)compareObject.differenceInnerDiameter, 2).ToString();
                    compareObject.postProcessing.Add($"Innendurchmesser bearbeiten: {diffInnerDiameter}");
                }

                if (compareObject.differenceOuterDiameter > 1)
                {
                    string diffOuterDiameter = Math.Round((Decimal)compareObject.differenceOuterDiameter, 2).ToString();
                    compareObject.postProcessing.Add($"Außendurchmesser bearbeiten: {diffOuterDiameter}");
                }   

                if (compareObject.differenceBoltDiameter == null && ((ProductDisc)product).BTC != null)
                {
                    string BTC = ((ProductDisc)product).BTC.ToString();
                    compareObject.postProcessing.Add($"Lochkreis einarbeiten: {BTC}");
                }
                    
            }
        }

        public void orderOutputData()
        {
            List<CompareObject> listModularMolds = new List<CompareObject>();
            List<CompareObject> sortedList = new List<CompareObject>();

            // Create list with only modular molds
            foreach (CompareObject compareObject in rankingJobInput)
            {
                if (compareObject.Mold is ModularMold)
                {
                    listModularMolds.Add(compareObject);
                }
                else
                {
                    filteredOutput.Add(compareObject);
                }
                    
            }

            // lists will be split into groups based on the contained baseplate
            var groupedLists01 = listModularMolds.GroupBy(x => ((ModularMold)x.Mold).baseplate.ID).Select(grp => grp.ToList()).ToList();

            // Grouped lists will be sorted by finalRating and merged into one list 
            foreach (List<CompareObject> list01 in groupedLists01)
            {
                var tempList = list01.GroupBy(x => ((ModularMold)x.Mold).insertPlate?.ID).Select(grp => grp.ToList()).ToList();
                
                foreach (List<CompareObject> list02 in tempList)
                {
                    var orderedList = list02.OrderByDescending(x => x.finalRating);
                    sortedList.AddRange(orderedList);
                }
            }

            // Algorithm to combine CompareObjects with identical baseplates into one object to reduce table entries on output
            CompareObject nextObject;
            CompareObject currentObject = null;

            foreach (CompareObject compareObject in sortedList)
            {
                if (compareObject.Mold is ModularMold)
                {
                    nextObject = compareObject;

                    // Case: Different baseplate or insertPlates
                    if (currentObject == null 
                        || ((ModularMold)nextObject.Mold).baseplate.ID != ((ModularMold)currentObject.Mold).baseplate.ID
                        || ((ModularMold)nextObject.Mold).insertPlate?.ID != ((ModularMold)currentObject.Mold).insertPlate?.ID)
                    {
                        if (currentObject != null)
                        {
                            filteredOutput.Add(currentObject);
                        }
                        currentObject = nextObject;
                    }

                    // Case: Baseplates and insertPlate are identical but core and guideRing are different
                    else if (((ModularMold)nextObject.Mold).baseplate.ID == ((ModularMold)currentObject.Mold).baseplate.ID
                        && ((ModularMold)nextObject.Mold).insertPlate?.ID == ((ModularMold)currentObject.Mold).insertPlate?.ID)
                        //&& ((ModularMold)compareObject.Mold).ListCoreRings.Count <= 0 && ((ModularMold)compareObject.Mold).ListOuterRings.Count <= 0)
                    {
                        if (!currentObject.alternativeCores.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).core))
                            //Contains(((ModularMold)nextObject.Mold).core))
                        {
                            var core = ((ModularMold)nextObject.Mold).core;
                            var diffToProduct = Math.Round((Decimal)nextObject.differenceInnerDiameter, 2).ToString();
                            currentObject.alternativeCores.Add(new Tuple<Core, string>(core, diffToProduct));
                        }
                        
                        if (!currentObject.alternativeGuideRings.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).guideRing))
                        //Contains(((ModularMold)nextObject.Mold).guideRing))
                        {
                            var guideRing = ((ModularMold)nextObject.Mold).guideRing;
                            var diffToProduct = Math.Round((Decimal)nextObject.differenceOuterDiameter, 2).ToString();
                            currentObject.alternativeGuideRings.Add(new Tuple<Ring, string>(guideRing, diffToProduct));
                        }
                    }
                    // Case: There are additional core- or outerings involved --> combination gets seperate table entry
                    else
                    {
                        if (currentObject != null)
                        {
                            filteredOutput.Add(currentObject);
                        }
                    }
                }
                else
                {
                    filteredOutput.Add(compareObject);
                }
            }

            // Last object will be added after algorithm is finished
            if (currentObject != null)
            {
                filteredOutput.Add(currentObject);
            }

            var listOrdered = filteredOutput.OrderByDescending(x => x.finalRating);
            this.rankingJobOutput = new List<CompareObject>(listOrdered);
        }
    }
}
