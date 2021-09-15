//-----------------------------------------------------------------------
// <copyright file="RankingJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class RankingJob
    {
        public List<CompareObject> rankingJobOutput { get; set; }

        public decimal? factorOuterDiameter { get; set; } = 0.2m;

        public decimal? factorInnerDiameter { get; set; } = 0.2m;

        public decimal? factorBoltDiameter { get; set; } = 0;

        public RankingJob(Product product, CompareJob compareJob)
        {
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
                compareObject.finalRating = this.compare(35.00m - compareObject.differenceOuterDiameter * this.factorOuterDiameter, 0.00m);
                compareObject.finalRating += this.compare(35.00m - compareObject.differenceInnerDiameter * this.factorInnerDiameter, 0.00m);

                for (int i = 1; i <= 6; i++)
                {
                    if (compareObject.boltCirclesBaseplate[i] != 0)
                    {
                        var tempList01 = compareObject.bolts.Where(bolt => bolt.Item2 == compareObject.boltCirclesBaseplate[i]);
                        var minDifference = tempList01.Min(p => p.Item3);
                        compareObject.finalRating += this.compare(10.00m - minDifference * this.factorBoltDiameter, 0.00m);
                    } 
                }
            }
        }
    }
}
