//-----------------------------------------------------------------------
// <copyright file="SearchJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using log4net;

    /// <summary>
    /// Primary class that triggers all algorithms used for the mold search in a specific order and returns the final output to the viewModel.
    /// </summary>
    public class SearchJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SearchJob));

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchJob"/> class.
        /// </summary>
        /// <param name="productDisc">Expects Disc Product.</param>
        public SearchJob(ProductDisc productDisc)
        {
            this.ProductDisc = productDisc;

            this.AdjustProductInformation(productDisc);

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

            this.AdjustProductInformation(productCup);

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

        public void AdjustProductInformation(ProductDisc productDisc)
        {
            if (!string.IsNullOrWhiteSpace(this.ProductDisc.BTC))
            {
                this.ProductDisc.BTC = this.ProductDisc.BTC.Trim();
                BoltCircleType boltCircleInformation = new BoltCircleType();

                try
                {
                    using (var db = new GießformDBContext())
                    {
                        boltCircleInformation = db.BoltCircleTypes.Find(this.ProductDisc.BTC);

                        if (boltCircleInformation == null)
                        {
                            MessageBox.Show($"Es konnten keine Informationen zum Lochkreis {this.ProductDisc.BTC} in der Datenbank gefunden werden." + Environment.NewLine + "Die Suche wird ohne Lochkreis durchgeführt.");
                            this.ProductDisc.BTC = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                this.ProductDisc.HcDiameter = boltCircleInformation?.Diameter;
                this.ProductDisc.HcHoleDiameter = boltCircleInformation?.HoleDiameter;
                this.ProductDisc.HcHoles = boltCircleInformation?.HoleQty;
            }

            this.ProductDisc.AddMultiMoldDimensions(this.ProductDisc.MultiMoldFactorPU);
            this.ProductDisc.AddSingleMoldDimensions((decimal)this.ProductDisc.FactorPU);
        }

        public void AdjustProductInformation(ProductCup productCup)
        {
            if (!string.IsNullOrWhiteSpace(this.ProductCup.BTC))
            {
                this.ProductCup.BTC = this.ProductCup.BTC.Trim();
                BoltCircleType boltCircleInformation = new BoltCircleType();

                try
                {
                    using (var db = new GießformDBContext())
                    {
                        boltCircleInformation = db.BoltCircleTypes.Find(this.ProductCup.BTC);

                        if (boltCircleInformation == null)
                        {
                            MessageBox.Show($"Es konnten keine Informationen zum Lochkreis {this.ProductCup.BTC} in der Datenbank gefunden werden." + Environment.NewLine + "Die Suche wird ohne Lochkreis durchgeführt.");
                            this.ProductCup.BTC = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                this.ProductCup.HcDiameter = boltCircleInformation?.Diameter;
                this.ProductCup.HcHoleDiameter = boltCircleInformation?.HoleDiameter;
                this.ProductCup.HcHoles = boltCircleInformation?.HoleQty;
            }

            this.ProductCup.AddMultiMoldDimensions(this.ProductCup.MultiMoldFactorPU);
            this.ProductCup.AddSingleMoldDimensions((decimal)this.ProductCup.FactorPU);
        }
    }
}
