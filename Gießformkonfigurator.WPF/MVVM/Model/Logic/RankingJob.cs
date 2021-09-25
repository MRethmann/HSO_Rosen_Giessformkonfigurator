//-----------------------------------------------------------------------
// <copyright file="RankingJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class RankingJob
    {
        public List<CompareObject> rankingJobOutput { get; set; }

        public ApplicationSettings applicationSettings { get; set; }

        public decimal? factorOuterDiameter { get; set; }

        public decimal? factorInnerDiameter { get; set; }

        public decimal? factorBoltDiameter { get; set; }

        public RankingJob(Product product, CompareJob compareJob)
        {
            applicationSettings = new ApplicationSettings();
            this.factorOuterDiameter = applicationSettings.rankingFactorOuterDiameter;
            this.factorInnerDiameter = applicationSettings.rankingFactorInnerDiameter;
            this.factorBoltDiameter = applicationSettings.rankingFactorBolts;
            this.rankingJobOutput = new List<CompareObject>(compareJob.compareJobOutput);
            this.rateMolds();
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
        public void rateMolds()
        {
            foreach (var compareObject in this.rankingJobOutput)
            {
                compareObject.finalRating = this.compare(34.00m - compareObject.differenceOuterDiameter * this.factorOuterDiameter, 0.00m);
                compareObject.finalRating += this.compare(34.00m - compareObject.differenceInnerDiameter * this.factorInnerDiameter, 0.00m);

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
                                compareObject.finalRating += this.compare(32.00m - minDifference * this.factorBoltDiameter, 0.00m);
                                compareObject.differenceBoltDiameter = minDifference;
                            }
                            else if (compareObject.boltCirclesInsertPlate[i] == true)
                            {
                                var minDifference = compareObject.bolts.Min(p => p.Item2);
                                compareObject.finalRating += this.compare(32.00m - minDifference * this.factorBoltDiameter, 0.00m);
                                compareObject.differenceBoltDiameter = minDifference;
                            }
                        }
                    }
                    else
                    {
                        compareObject.finalRating += 32.00m;
                    }
                }
                else if (compareObject.Mold is SingleMold)
                {
                    compareObject.finalRating += this.compare(32.00m - compareObject.differenceBoltDiameter * this.factorBoltDiameter, 0.00m);
                }

                compareObject.finalRating = Math.Round((Decimal) compareObject.finalRating, 2);

                if (compareObject.differenceInnerDiameter > 0)
                    compareObject.postProcessing.Add("Innendurchmesser bearbeiten");

                if (compareObject.differenceOuterDiameter > 0)
                    compareObject.postProcessing.Add("Außendurchmesser bearbeiten");

                if (compareObject.differenceBoltDiameter > 0)
                    compareObject.postProcessing.Add("Lochkreis einarbeiten");
            }
        }
    }
}
