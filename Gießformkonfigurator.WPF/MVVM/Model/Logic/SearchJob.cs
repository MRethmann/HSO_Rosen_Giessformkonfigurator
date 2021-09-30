//-----------------------------------------------------------------------
// <copyright file="ProgramLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System.Collections.Generic;

    class SearchJob
    {
        public FilterJob filterJob { get; set; }

        public CombinationJob combinationJob { get; set; }

        public CompareJob compareJob { get; set; }

        public RankingJob rankingJob { get; set; }

        public Product product { get; set; } = new Product();

        public List<CompareObject> finalOutput { get; set; }


        public SearchJob(Product product)
        {
            this.product = product;

            filterJob = new FilterJob(this.product);

            combinationJob = new CombinationJob(this.product, this.filterJob);

            compareJob = new CompareJob(this.product, this.combinationJob);

            rankingJob = new RankingJob(this.product, this.compareJob);

            finalOutput = new List<CompareObject>(this.rankingJob.rankingJobOutput);
        }
    }
}
