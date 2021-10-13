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
        /// <param name="productDisc">Expects Disc Product.</param>
        public SearchJob(ProductDisc productDisc)
        {
            this.ProductDisc = productDisc;

            this.FilterJob = new FilterJob(this.ProductDisc);

            this.CombinationJob = new CombinationJob(this.ProductDisc, this.FilterJob);

            this.CompareJob = new CompareJob(this.ProductDisc, this.CombinationJob);

            this.RankingJob = new RankingJob(this.ProductDisc, this.CompareJob);

            this.FinalOutput = new List<CompareObject>(this.RankingJob.RankingJobOutput);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchJob"/> class.
        /// </summary>
        /// <param name="productCup">Expects Cup Product.</param>
        public SearchJob(ProductCup productCup)
        {
            this.ProductCup = productCup;

            this.FilterJob = new FilterJob(this.ProductCup);

            this.CombinationJob = new CombinationJob(this.ProductCup, this.FilterJob);

            this.CompareJob = new CompareJob(this.ProductCup, this.CombinationJob);

            this.RankingJob = new RankingJob(this.ProductCup, this.CompareJob);

            this.FinalOutput = new List<CompareObject>(this.RankingJob.RankingJobOutput);
        }

        public FilterJob FilterJob { get; set; }

        public CombinationJob CombinationJob { get; set; }

        public CompareJob CompareJob { get; set; }

        public RankingJob RankingJob { get; set; }

        public ProductDisc ProductDisc { get; set; } = new ProductDisc();

        public ProductCup ProductCup { get; set; } = new ProductCup();

        public List<CompareObject> FinalOutput { get; set; }
    }
}
