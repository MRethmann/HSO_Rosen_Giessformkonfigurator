//-----------------------------------------------------------------------
// <copyright file="SearchJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using System.Collections.Generic;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Primary class that triggers all algorithms used for the mold search in a specific order and returns the final output to the viewModel.
    /// </summary>
    public class SearchJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchJob"/> class.
        /// </summary>
        public SearchJob(Product product)
        {
            this.Product = product;

            this.FilterJob = new FilterJob(this.Product);

            this.CombinationJob = new CombinationJob(this.Product, this.FilterJob);

            this.CompareJob = new CompareJob(this.Product, this.CombinationJob);

            this.RankingJob = new RankingJob(this.Product, this.CompareJob);

            this.FinalOutput = new List<CompareObject>(this.RankingJob.RankingJobOutput);
        }

        public FilterJob FilterJob { get; set; }

        public CombinationJob CombinationJob { get; set; }

        public CompareJob CompareJob { get; set; }

        public RankingJob RankingJob { get; set; }

        public Product Product { get; set; } = new Product();

        public List<CompareObject> FinalOutput { get; set; }
    }
}
