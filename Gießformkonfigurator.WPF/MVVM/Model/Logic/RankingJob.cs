//-----------------------------------------------------------------------
// <copyright file="RankingJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Takes all modularMolds and singleMolds that fit the product and add rating information used for the grouping and display in the GUI.
    /// </summary>
    public class RankingJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RankingJob"/> class.
        /// </summary>
        /// <param name="product">Excepts product of type Cup or Disc.</param>
        /// <param name="compareJob">Uses compareJob Output data.</param>
        public RankingJob(Product product, CompareJob compareJob)
        {
            this.Product = product;
            this.RankingSettings = new RankingSettings();
            this.FactorOuterDiameter = this.RankingSettings.RankingFactorOuterDiameter;
            this.FactorInnerDiameter = this.RankingSettings.RankingFactorInnerDiameter;
            this.FactorBoltDiameter = this.RankingSettings.RankingFactorBolts;
            this.RankingJobInput = new List<CompareObject>(compareJob.CompareJobOutput);
            this.ToleranceSettings = new ToleranceSettings();
            this.AddRatingInformationDiscs();
            this.OrderOutputDataDiscs();

            // Updating rating information twice because there might be a better ring/core which was found
            this.AddRatingInformationDiscs();
        }

        public RankingJob(ProductCup productCup, CompareJob compareJob)
        {
            this.Product = productCup;
            this.RankingSettings = new RankingSettings();
            this.FactorOuterDiameter = this.RankingSettings.RankingFactorOuterDiameter;
            this.FactorInnerDiameter = this.RankingSettings.RankingFactorInnerDiameter;
            this.FactorBoltDiameter = this.RankingSettings.RankingFactorBolts;
            this.RankingJobInput = new List<CompareObject>(compareJob.CompareJobOutput);
            this.ToleranceSettings = new ToleranceSettings();
            this.AddRatingInformationCups();
            this.OrderOutputDataCups();
        }

        /// <summary>
        /// Gets or Sets the output list of rankingJob.
        /// </summary>
        public List<CompareObject> RankingJobOutput { get; set; }

        /// <summary>
        /// Gets or Sets the input lists from compareJob.
        /// </summary>
        private List<CompareObject> RankingJobInput { get; set; }

        /// <summary>
        /// Gets or Sets the temp List which is used for the ordering and grouping method.
        /// </summary>
        private List<CompareObject> FilteredOutput { get; set; } = new List<CompareObject>();

        private RankingSettings RankingSettings { get; set; }

        private ToleranceSettings ToleranceSettings { get; set; }

        private decimal? FactorOuterDiameter { get; set; }

        private decimal? FactorInnerDiameter { get; set; }

        private decimal? FactorBoltDiameter { get; set; }

        private Product Product { get; set; }

        /// <summary>
        /// Small compare method, that returns the bigger param.
        /// </summary>
        /// <param name="var1">Number one.</param>
        /// <param name="var2">Number two.</param>
        /// <returns>The greater number.</returns>
        public decimal? Compare(decimal? var1, decimal? var2)
        {
            if (var1 >= var2)
            {
                return var1;
            }
            else
            {
                return var2;
            }
        }

        /// <summary>
        /// Final Step to rate all molds based on their difference between actual Output after production and prefered Output on paper. Important Values are for example differences in outer and inner diameter and height.
        /// </summary>
        private void AddRatingInformationDiscs()
        {
            foreach (var compareObject in this.RankingJobInput)
            {
                compareObject.FinalRating = 0;
                compareObject.FinalRating = this.Compare(45.00m - ((compareObject.DifferenceOuterDiameter > this.ToleranceSettings?.Product_OuterDiameter_MAX ? compareObject.DifferenceOuterDiameter : 0) * this.FactorOuterDiameter), 0.00m);
                compareObject.FinalRating += this.Compare(45.00m - ((compareObject.DifferenceInnerDiameter > this.ToleranceSettings?.Product_InnerDiameter_MAX ? compareObject.DifferenceInnerDiameter : 0) * this.FactorInnerDiameter), 0.00m);

                if (compareObject.Mold is ModularMold)
                {
                    if ((compareObject.Product is ProductCup
                        && !string.IsNullOrWhiteSpace(((ProductCup)this.Product).BTC))
                        || (compareObject.Product is ProductDisc
                        && !string.IsNullOrWhiteSpace(((ProductDisc)this.Product).BTC)))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (compareObject.BoltCirclesBaseplate[i] == true)
                            {
                                var minDifference = compareObject.Bolts.Min(p => p.Item2);
                                compareObject.FinalRating += 10.00m;
                                compareObject.DifferenceBoltDiameter = minDifference;
                            }
                            else if (compareObject.BoltCirclesInsertPlate[i] == true)
                            {
                                var minDifference = compareObject.Bolts.Min(p => p.Item2);
                                compareObject.FinalRating += 10.00m;
                                compareObject.DifferenceBoltDiameter = minDifference;
                            }
                        }
                    }
                    else
                    {
                        compareObject.FinalRating += 10.00m;
                    }
                }
                else if (compareObject.Mold is SingleMold)
                {
                    if (string.IsNullOrWhiteSpace(this.Product.BTC))
                    {
                        compareObject.FinalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.DifferenceBoltDiameter = 0;
                    }
                    else if (!string.IsNullOrWhiteSpace(this.Product.BTC))
                        //TODO: Prüfen ob notwendig.
                        //&& (((SingleMoldDisc)compareObject.Mold).HcDiameter != null && ((SingleMoldDisc)compareObject.Mold).HcDiameter > 0)
                        //&& (((SingleMoldDisc)compareObject.Mold).HcHoles != null && ((SingleMoldDisc)compareObject.Mold).HcHoles > 0)
                        //&& (((SingleMoldDisc)compareObject.Mold).BoltDiameter != null && ((SingleMoldDisc)compareObject.Mold).BoltDiameter > 0))
                    {
                        compareObject.FinalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.DifferenceBoltDiameter = 0;
                    }
                }

                compareObject.FinalRating = Math.Round((decimal)compareObject.FinalRating, 2);
            }

            var tempList = this.RankingJobOutput?.OrderByDescending(x => x.FinalRating);
            this.RankingJobOutput = this.RankingJobOutput != null ? new List<CompareObject>(tempList) : null;
        }

        private void AddRatingInformationCups()
        {
            foreach (var compareObject in this.RankingJobInput)
            {
                compareObject.FinalRating = 0;
                compareObject.FinalRating = this.Compare(45.00m - ((compareObject.DifferenceOuterDiameter > this.ToleranceSettings?.Product_OuterDiameter_MAX ? compareObject.DifferenceOuterDiameter : 0) * this.FactorOuterDiameter), 0.00m);
                compareObject.FinalRating += this.Compare(45.00m - ((compareObject.DifferenceInnerDiameter > this.ToleranceSettings?.Product_InnerDiameter_MAX ? compareObject.DifferenceInnerDiameter : 0) * this.FactorInnerDiameter), 0.00m);

                if (compareObject.Mold is ModularMold)
                {
                    if ((compareObject.Product is ProductCup
                        && !string.IsNullOrWhiteSpace(((ProductCup)this.Product).BTC))
                        || (compareObject.Product is ProductDisc
                        && !string.IsNullOrWhiteSpace(((ProductDisc)this.Product).BTC)))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (compareObject.BoltCirclesBaseplate[i] == true)
                            {
                                var minDifference = compareObject.Bolts.Min(p => p.Item2);
                                compareObject.FinalRating += 10.00m;
                                compareObject.DifferenceBoltDiameter = minDifference;
                            }
                            else if (compareObject.BoltCirclesInsertPlate[i] == true)
                            {
                                var minDifference = compareObject.Bolts.Min(p => p.Item2);
                                compareObject.FinalRating += 10.00m;
                                compareObject.DifferenceBoltDiameter = minDifference;
                            }
                        }
                    }
                    else
                    {
                        compareObject.FinalRating += 10.00m;
                    }
                }
                else if (compareObject.Mold is SingleMold)
                {
                    if (string.IsNullOrWhiteSpace(this.Product.BTC))
                    {
                        compareObject.FinalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.DifferenceBoltDiameter = 0;
                    }
                    else if (!string.IsNullOrWhiteSpace(this.Product.BTC))
                    //TODO: Prüfen ob notwendig.
                    //&& (((SingleMoldDisc)compareObject.Mold).HcDiameter != null && ((SingleMoldDisc)compareObject.Mold).HcDiameter > 0)
                    //&& (((SingleMoldDisc)compareObject.Mold).HcHoles != null && ((SingleMoldDisc)compareObject.Mold).HcHoles > 0)
                    //&& (((SingleMoldDisc)compareObject.Mold).BoltDiameter != null && ((SingleMoldDisc)compareObject.Mold).BoltDiameter > 0))
                    {
                        compareObject.FinalRating += 10.00m;

                        // Used while adding post processing information. Means that no BTC needs to be added to the product.
                        compareObject.DifferenceBoltDiameter = 0;
                    }
                }

                compareObject.FinalRating = Math.Round((decimal)compareObject.FinalRating, 2);
            }

            var tempList = this.RankingJobOutput?.OrderByDescending(x => x.FinalRating);
            this.RankingJobOutput = this.RankingJobOutput != null ? new List<CompareObject>(tempList) : null;
        }

        private void OrderOutputDataDiscs()
        {
            List<CompareObject> listModularMolds = new List<CompareObject>();
            List<CompareObject> sortedList = new List<CompareObject>();

            // Create list with only modular molds
            foreach (CompareObject compareObject in this.RankingJobInput)
            {
                if (compareObject.Mold is ModularMold)
                {
                    listModularMolds.Add(compareObject);
                }
                else
                {
                    this.FilteredOutput.Add(compareObject);
                }
            }

            // Grouped by baseplates
            var groupedByBaseplate = listModularMolds.GroupBy(x => ((ModularMold)x.Mold).Baseplate.ID).Select(grp => grp.ToList()).ToList();

            // Grouped by insertplates
            foreach (List<CompareObject> list01 in groupedByBaseplate)
            {
                var groupedByInsertplate = list01.GroupBy(x => ((ModularMold)x.Mold).InsertPlate?.ID).Select(grp => grp.ToList()).ToList();

                // Grouped by Guide Rings
                foreach (List<CompareObject> list02 in groupedByInsertplate)
                {
                    var groupedByGuideRing = list02.GroupBy(x => ((ModularMold)x.Mold).GuideRing.ID).Select(grp => grp.ToList()).ToList();

                    // Grouped by Cores
                    foreach (List<CompareObject> list03 in groupedByGuideRing)
                    {
                        var groupedByCore = list03.GroupBy(x => ((ModularMold)x.Mold).Core.ID).Select(grp => grp.ToList()).ToList();

                        // Sorted by final rating and ordered
                        foreach (List<CompareObject> list04 in groupedByCore)
                        {
                            var orderedList = list04.OrderBy(x => x.FinalRating);
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
                        || ((ModularMold)nextObject.Mold).Baseplate.ID != ((ModularMold)currentObject.Mold).Baseplate.ID
                        || ((ModularMold)nextObject.Mold).InsertPlate?.ID != ((ModularMold)currentObject.Mold).InsertPlate?.ID
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2)
                    {
                        if (currentObject != null)
                        {
                            this.FilteredOutput.Add(currentObject);
                        }

                        currentObject = nextObject;
                    }

                    // Case: Baseplates and insertPlate are identical but core and guideRing are different
                    else if ((((ModularMold)nextObject.Mold).Baseplate.ID == ((ModularMold)currentObject.Mold).Baseplate.ID
                        && ((ModularMold)nextObject.Mold).InsertPlate?.ID == ((ModularMold)currentObject.Mold).InsertPlate?.ID
                        && ((ModularMold)nextObject.Mold).ListCoreRings.Count > 0 && ((ModularMold)nextObject.Mold).ListOuterRings.Count > 0)
                        || (((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item2
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item2))
                    {
                        if (!currentObject.AlternativeCores.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).Core))
                        {
                            var core = ((ModularMold)nextObject.Mold).Core;
                            var diffToProduct = Math.Round((decimal)nextObject.DifferenceInnerDiameter, 2);
                            currentObject.AlternativeCores.Add(new Tuple<Core, decimal>(core, diffToProduct));
                        }

                        if (!currentObject.AlternativeGuideRings.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).GuideRing))
                        {
                            var guideRing = ((ModularMold)nextObject.Mold).GuideRing;
                            var diffToProduct = Math.Round((decimal)nextObject.DifferenceOuterDiameter, 2);
                            currentObject.AlternativeGuideRings.Add(new Tuple<Ring, decimal>(guideRing, diffToProduct));
                        }
                    }

                    // Case: There are additional core- or outerings involved --> combination gets seperate table entry
                    else
                    {
                        if (currentObject != null)
                        {
                            this.FilteredOutput.Add(currentObject);
                        }
                    }
                }
                else
                {
                    this.FilteredOutput.Add(compareObject);
                }
            }

            // Last object will be added after algorithm is finished
            if (currentObject != null)
            {
                this.FilteredOutput.Add(currentObject);
            }

            // Order Lists of alternative Components
            foreach (var compareObject in this.FilteredOutput)
            {
                var sortedListCores = compareObject.AlternativeCores.OrderBy(x => x.Item2);
                var sortedListGuideRings = compareObject.AlternativeGuideRings.OrderBy(x => x.Item2);

                compareObject.AlternativeCores = new List<Tuple<Core, decimal>>(sortedListCores);
                compareObject.AlternativeGuideRings = new List<Tuple<Ring, decimal>>(sortedListGuideRings);

                if (compareObject.AlternativeCores.Count > 0)
                {
                    if (compareObject.AlternativeCores.First().Item2 < compareObject.DifferenceInnerDiameter)
                    {
                        ((ModularMold)compareObject.Mold).Core = compareObject.AlternativeCores.First().Item1;
                        compareObject.DifferenceInnerDiameter = compareObject.AlternativeCores.First().Item2;
                    }
                }

                if (compareObject.AlternativeGuideRings.Count > 0)
                {
                    if (compareObject.AlternativeGuideRings.First().Item2 < compareObject.DifferenceOuterDiameter)
                    {
                        ((ModularMold)compareObject.Mold).GuideRing = compareObject.AlternativeGuideRings.First().Item1;
                        compareObject.DifferenceOuterDiameter = compareObject.AlternativeGuideRings.First().Item2;
                    }
                }

                compareObject.AlternativeCores.Remove(compareObject.AlternativeCores.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).Core?.ID));
                compareObject.AlternativeGuideRings.Remove(compareObject.AlternativeGuideRings.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).GuideRing?.ID));

                if ((compareObject.DifferenceInnerDiameter > 0.1m
                    && compareObject.DifferenceInnerDiameter > this.ToleranceSettings?.Product_InnerDiameter_MAX)
                    || compareObject.DifferenceInnerDiameter < -0.1m)
                {
                    decimal decimalDiffInnerDiameter = Math.Round((decimal)compareObject.DifferenceInnerDiameter, 2);
                    string diffInnerDiameter = string.Format("{0:0.00}", decimalDiffInnerDiameter);
                    compareObject.PostProcessing.Add($"Innendurchmesser:  {diffInnerDiameter} mm");
                }

                if ((compareObject.DifferenceOuterDiameter > 0.1m
                    && compareObject.DifferenceOuterDiameter >= this.ToleranceSettings?.Product_OuterDiameter_MAX)
                    || compareObject.DifferenceOuterDiameter < -0.1m)
                {
                    decimal decimalDiffOuterDiameter = Math.Round((decimal)compareObject.DifferenceOuterDiameter, 2);
                    string diffOuterDiameter = string.Format("{0:0.00}", decimalDiffOuterDiameter);
                    compareObject.PostProcessing.Add($"Außendurchmesser: {diffOuterDiameter} mm");
                }

                if (compareObject.DifferenceBoltDiameter == null && !string.IsNullOrWhiteSpace(((ProductDisc)this.Product).BTC))
                {
                    string btc = ((ProductDisc)this.Product).BTC.ToString();
                    compareObject.PostProcessing.Add($"Lochkreis einarbeiten: {btc}");
                }
            }

            var listOrdered = this.FilteredOutput.OrderByDescending(x => x.FinalRating);
            this.RankingJobOutput = new List<CompareObject>(listOrdered);
        }

        private void OrderOutputDataCups()
        {
            List<CompareObject> listModularMolds = new List<CompareObject>();
            List<CompareObject> sortedList = new List<CompareObject>();

            // Create list with only modular molds
            foreach (CompareObject compareObject in this.RankingJobInput)
            {
                if (compareObject.Mold is ModularMold)
                {
                    listModularMolds.Add(compareObject);
                }
                else
                {
                    this.FilteredOutput.Add(compareObject);
                }
            }

            // Grouped by baseplates
            var groupedByBaseplate = listModularMolds.GroupBy(x => ((ModularMold)x.Mold).Baseplate.ID).Select(grp => grp.ToList()).ToList();

            // Grouped by insertplates
            foreach (List<CompareObject> list01 in groupedByBaseplate)
            {
                var groupedByInsertplate = list01.GroupBy(x => ((ModularMold)x.Mold).InsertPlate?.ID).Select(grp => grp.ToList()).ToList();

                // Grouped by Guide Rings
                foreach (List<CompareObject> list02 in groupedByInsertplate)
                {
                    var groupedByGuideRing = list02.GroupBy(x => ((ModularMold)x.Mold).GuideRing.ID).Select(grp => grp.ToList()).ToList();

                    // Grouped by Cores
                    foreach (List<CompareObject> list03 in groupedByGuideRing)
                    {
                        var groupedByCore = list03.GroupBy(x => ((ModularMold)x.Mold).Core.ID).Select(grp => grp.ToList()).ToList();

                        // Sorted by final rating and ordered
                        foreach (List<CompareObject> list04 in groupedByCore)
                        {
                            var orderedList = list04.OrderBy(x => x.FinalRating);
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
                        || ((ModularMold)nextObject.Mold).Baseplate.ID != ((ModularMold)currentObject.Mold).Baseplate.ID
                        || ((ModularMold)nextObject.Mold).InsertPlate?.ID != ((ModularMold)currentObject.Mold).InsertPlate?.ID
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListCoreRings?.FirstOrDefault()?.Item2
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item1
                        || ((ModularMold)nextObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2 != ((ModularMold)currentObject.Mold).ListOuterRings?.FirstOrDefault()?.Item2)
                    {
                        if (currentObject != null)
                        {
                            this.FilteredOutput.Add(currentObject);
                        }

                        currentObject = nextObject;
                    }

                    // Case: Baseplates and insertPlate are identical but core and guideRing are different
                    else if ((((ModularMold)nextObject.Mold).Baseplate.ID == ((ModularMold)currentObject.Mold).Baseplate.ID
                        && ((ModularMold)nextObject.Mold).InsertPlate?.ID == ((ModularMold)currentObject.Mold).InsertPlate?.ID
                        && ((ModularMold)nextObject.Mold).ListCoreRings.Count > 0 && ((ModularMold)nextObject.Mold).ListOuterRings.Count > 0)
                        || (((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListCoreRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListCoreRings.FirstOrDefault()?.Item2
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item1 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item1
                        && ((ModularMold)nextObject.Mold).ListOuterRings.FirstOrDefault()?.Item2 == ((ModularMold)currentObject.Mold).ListOuterRings.FirstOrDefault()?.Item2))
                    {
                        if (!currentObject.AlternativeCores.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).Core))
                        {
                            var core = ((ModularMold)nextObject.Mold).Core;
                            var diffToProduct = Math.Round((decimal)nextObject.DifferenceInnerDiameter, 2);
                            currentObject.AlternativeCores.Add(new Tuple<Core, decimal>(core, diffToProduct));
                        }

                        if (!currentObject.AlternativeGuideRings.Any(c => c.Item1 == ((ModularMold)nextObject.Mold).GuideRing))
                        {
                            var guideRing = ((ModularMold)nextObject.Mold).GuideRing;
                            var diffToProduct = Math.Round((decimal)nextObject.DifferenceOuterDiameter, 2);
                            currentObject.AlternativeGuideRings.Add(new Tuple<Ring, decimal>(guideRing, diffToProduct));
                        }
                    }

                    // Case: There are additional core- or outerings involved --> combination gets seperate table entry
                    else
                    {
                        if (currentObject != null)
                        {
                            this.FilteredOutput.Add(currentObject);
                        }
                    }
                }
                else
                {
                    this.FilteredOutput.Add(compareObject);
                }
            }

            // Last object will be added after algorithm is finished
            if (currentObject != null)
            {
                this.FilteredOutput.Add(currentObject);
            }

            // Order Lists of alternative Components
            foreach (var compareObject in this.FilteredOutput)
            {
                var sortedListCores = compareObject.AlternativeCores.OrderBy(x => x.Item2);
                var sortedListGuideRings = compareObject.AlternativeGuideRings.OrderBy(x => x.Item2);

                compareObject.AlternativeCores = new List<Tuple<Core, decimal>>(sortedListCores);
                compareObject.AlternativeGuideRings = new List<Tuple<Ring, decimal>>(sortedListGuideRings);

                if (compareObject.AlternativeCores.Count > 0)
                {
                    if (compareObject.AlternativeCores.First().Item2 < compareObject.DifferenceInnerDiameter)
                    {
                        ((ModularMold)compareObject.Mold).Core = compareObject.AlternativeCores.First().Item1;
                        compareObject.DifferenceInnerDiameter = compareObject.AlternativeCores.First().Item2;
                    }
                }

                if (compareObject.AlternativeGuideRings.Count > 0)
                {
                    if (compareObject.AlternativeGuideRings.First().Item2 < compareObject.DifferenceOuterDiameter)
                    {
                        ((ModularMold)compareObject.Mold).GuideRing = compareObject.AlternativeGuideRings.First().Item1;
                        compareObject.DifferenceOuterDiameter = compareObject.AlternativeGuideRings.First().Item2;
                    }
                }

                compareObject.AlternativeCores.Remove(compareObject.AlternativeCores.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).Core?.ID));
                compareObject.AlternativeGuideRings.Remove(compareObject.AlternativeGuideRings.Find(x => x.Item1.ID == ((ModularMold)compareObject?.Mold).GuideRing?.ID));

                if ((compareObject.DifferenceInnerDiameter > 0.1m
                    && compareObject.DifferenceInnerDiameter > this.ToleranceSettings?.Product_InnerDiameter_MAX)
                    || compareObject.DifferenceInnerDiameter < -0.1m)
                {
                    decimal decimalDiffInnerDiameter = Math.Round((decimal)compareObject.DifferenceInnerDiameter, 2);
                    string diffInnerDiameter = string.Format("{0:0.00}", decimalDiffInnerDiameter);
                    compareObject.PostProcessing.Add($"Innendurchmesser:  {diffInnerDiameter} mm");
                }

                if ((compareObject.DifferenceOuterDiameter > 0.1m
                    && compareObject.DifferenceOuterDiameter >= this.ToleranceSettings?.Product_OuterDiameter_MAX)
                    || compareObject.DifferenceOuterDiameter < -0.1m)
                {
                    decimal decimalDiffOuterDiameter = Math.Round((decimal)compareObject.DifferenceOuterDiameter, 2);
                    string diffOuterDiameter = string.Format("{0:0.00}", decimalDiffOuterDiameter);
                    compareObject.PostProcessing.Add($"Außendurchmesser: {diffOuterDiameter} mm");
                }

                if (compareObject.DifferenceBoltDiameter == null && !string.IsNullOrWhiteSpace(((ProductDisc)this.Product).BTC))
                {
                    string btc = ((ProductDisc)this.Product).BTC.ToString();
                    compareObject.PostProcessing.Add($"Lochkreis einarbeiten: {btc}");
                }
            }

            var listOrdered = this.FilteredOutput.OrderByDescending(x => x.FinalRating);
            this.RankingJobOutput = new List<CompareObject>(listOrdered);
        }
    }
}