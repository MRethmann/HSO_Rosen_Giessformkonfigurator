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

        public ToleranceSettings toleranceSettings { get; set; }

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
            toleranceSettings = new ToleranceSettings();
            this.addRatingInformation();
            this.orderOutputData();
            
            // Updating rating information because there might be a better ring/core which was found
            this.addRatingInformation();
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
                compareObject.finalRating = 0;
                compareObject.finalRating = this.compare(45.00m - (compareObject.differenceOuterDiameter > toleranceSettings?.product_OuterDiameter_MAX ? compareObject.differenceOuterDiameter : 0) * this.factorOuterDiameter, 0.00m);
                compareObject.finalRating += this.compare(45.00m - (compareObject.differenceInnerDiameter > toleranceSettings?.product_InnerDiameter_MAX ? compareObject.differenceInnerDiameter : 0) * this.factorInnerDiameter, 0.00m);

                if (compareObject.Mold is ModularMold)
                {
                    if (compareObject.Product is ProductCup && !String.IsNullOrWhiteSpace(((ProductDisc)product).BTC)
                        || compareObject.Product is ProductDisc && !String.IsNullOrWhiteSpace(((ProductDisc)product).BTC))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (compareObject.boltCirclesBaseplate[i] == true)
                            {
                                var minDifference = compareObject.bolts.Min(p => p.Item2);
                                compareObject.finalRating += 10.00m;
                                compareObject.differenceBoltDiameter = minDifference;
                            }
                            else if (compareObject.boltCirclesInsertPlate[i] == true)
                            {
                                var minDifference = compareObject.bolts.Min(p => p.Item2);
                                compareObject.finalRating += 10.00m;
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
                    if (String.IsNullOrWhiteSpace(product.BTC))
                    {
                        compareObject.finalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.differenceBoltDiameter = 0;
                    }
                    else if (!String.IsNullOrWhiteSpace(product.BTC) 
                        && (((SingleMold)compareObject.Mold).HcDiameter != null && ((SingleMold)compareObject.Mold).HcDiameter > 0)
                        && (((SingleMold)compareObject.Mold).HcHoles != null && ((SingleMold)compareObject.Mold).HcHoles > 0)
                        && (((SingleMold)compareObject.Mold).BoltDiameter != null && ((SingleMold)compareObject.Mold).BoltDiameter > 0))
                    {
                        compareObject.finalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.differenceBoltDiameter = 0;
                    }
                }

                compareObject.finalRating = Math.Round((Decimal) compareObject.finalRating, 2);    
            }

            var tempList = rankingJobOutput?.OrderByDescending(x => x.finalRating);
            rankingJobOutput = rankingJobOutput != null ? new List<CompareObject>(tempList) : null;
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

            
            // Grouped by baseplates
            var groupedByBaseplate = listModularMolds.GroupBy(x => ((ModularMold)x.Mold).baseplate.ID).Select(grp => grp.ToList()).ToList();

            // Grouped by insertplates 
            foreach (List<CompareObject> list01 in groupedByBaseplate)
            {
                var groupedByInsertplate = list01.GroupBy(x => ((ModularMold)x.Mold).insertPlate?.ID).Select(grp => grp.ToList()).ToList();

                // Grouped by Guide Rings
                foreach (List<CompareObject> list02 in groupedByInsertplate)
                {
                    var groupedByGuideRing = list02.GroupBy(x => ((ModularMold)x.Mold).guideRing?.ID).Select(grp => grp.ToList()).ToList();

                    // Grouped by Cores
                    foreach (List<CompareObject> list03 in groupedByGuideRing)
                    {
                        var groupedByCore = list03.GroupBy(x => ((ModularMold)x.Mold).core?.ID).Select(grp => grp.ToList()).ToList();

                        // Sorted by final rating and ordered
                        foreach (List<CompareObject> list04 in groupedByCore)
                        {
                            var orderedList = list04.OrderBy(x => x.finalRating);
                            sortedList.AddRange(orderedList);
                        }
                    }
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
                        || ((ModularMold)nextObject.Mold).insertPlate?.ID != ((ModularMold)currentObject.Mold).insertPlate?.ID
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2)
                    {
                        if (currentObject != null)
                        {
                            filteredOutput.Add(currentObject);
                        }
                        currentObject = nextObject;
                    }

                    // Case: Baseplates and insertPlate are identical but core and guideRing are different
                    else if (((ModularMold)nextObject.Mold).baseplate.ID == ((ModularMold)currentObject.Mold).baseplate.ID
                        && ((ModularMold)nextObject.Mold).insertPlate?.ID == ((ModularMold)currentObject.Mold).insertPlate?.ID
                        && ((ModularMold)nextObject.Mold).ListCoreRings.Count > 0  && ((ModularMold)nextObject.Mold).ListOuterRings.Count > 0
                        || (((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item2
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item2))
                    {
                        if (!currentObject.alternativeCores.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).core))
                            //Contains(((ModularMold)nextObject.Mold).core))
                        {
                            var core = ((ModularMold)nextObject.Mold).core;
                            var diffToProduct = Math.Round((Decimal)nextObject.differenceInnerDiameter, 2);
                            currentObject.alternativeCores.Add(new Tuple<Core, decimal>(core, diffToProduct));
                        }
                        
                        if (!currentObject.alternativeGuideRings.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).guideRing))
                        //Contains(((ModularMold)nextObject.Mold).guideRing))
                        {
                            var guideRing = ((ModularMold)nextObject.Mold).guideRing;
                            var diffToProduct = Math.Round((Decimal)nextObject.differenceOuterDiameter, 2);
                            currentObject.alternativeGuideRings.Add(new Tuple<Ring, decimal>(guideRing, diffToProduct));
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

            // Order Lists of alternative Components
            foreach (var compareObject in filteredOutput)
            {
                var sortedListCores = compareObject.alternativeCores.OrderBy(x => x.Item2);
                var sortedListGuideRings = compareObject.alternativeGuideRings.OrderBy(x => x.Item2);

                compareObject.alternativeCores = new List<Tuple<Core, decimal>>(sortedListCores);
                compareObject.alternativeGuideRings = new List<Tuple<Ring, decimal>>(sortedListGuideRings);

                if (compareObject.alternativeCores.Count > 0)
                {
                    if (compareObject.alternativeCores.First().Item2 < compareObject.differenceInnerDiameter)
                    {
                        ((ModularMold)compareObject.Mold).core = compareObject.alternativeCores.First().Item1;
                        compareObject.differenceInnerDiameter = compareObject.alternativeCores.First().Item2;
                    }
                }

                if (compareObject.alternativeGuideRings.Count > 0)
                {
                    if (compareObject.alternativeGuideRings.First().Item2 < compareObject.differenceOuterDiameter)
                    {
                        ((ModularMold)compareObject.Mold).guideRing = compareObject.alternativeGuideRings.First().Item1;
                        compareObject.differenceOuterDiameter = compareObject.alternativeGuideRings.First().Item2;
                    }
                }

                compareObject.alternativeCores.Remove(compareObject.alternativeCores.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).core?.ID));
                compareObject.alternativeGuideRings.Remove(compareObject.alternativeGuideRings.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).guideRing?.ID));

                if (compareObject.differenceInnerDiameter > toleranceSettings?.product_InnerDiameter_MAX)
                {
                    string diffInnerDiameter = Math.Round((Decimal)compareObject.differenceInnerDiameter, 2).ToString();
                    compareObject.postProcessing.Add($"Innendurchmesser:  {diffInnerDiameter} mm");
                }

                if (compareObject.differenceOuterDiameter > toleranceSettings?.product_OuterDiameter_MAX)
                {
                    string diffOuterDiameter = Math.Round((Decimal)compareObject.differenceOuterDiameter, 2).ToString();
                    compareObject.postProcessing.Add($"Außendurchmesser: {diffOuterDiameter} mm");
                }

                if (compareObject.differenceBoltDiameter == null && !String.IsNullOrWhiteSpace(((ProductDisc)product).BTC))
                {
                    string BTC = ((ProductDisc)product).BTC.ToString();
                    compareObject.postProcessing.Add($"Lochkreis einarbeiten: {BTC}");
                }

            }

            var listOrdered = filteredOutput.OrderByDescending(x => x.finalRating);
            this.rankingJobOutput = new List<CompareObject>(listOrdered);
        }
    }
}
