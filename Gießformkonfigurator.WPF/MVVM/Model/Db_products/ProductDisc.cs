//-----------------------------------------------------------------------
// <copyright file="ProductDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System;

    public class ProductDisc : Product
    {
        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal InnerDiameter { get; set; }

        /// <summary>
        /// Gets or Sets dimension information which is calculated by singleMold pu factor.
        /// </summary>
        public ProductDisc SingleMoldDimensions { get; set; }

        /// <summary>
        /// Gets or Sets dimension information which is calculated by multiMold pu factor.
        /// </summary>
        public ProductDisc ModularMoldDimensions { get; set; }

        public void AddMultiMoldDimensions(decimal multiMoldFactorPu)
        {
            this.ModularMoldDimensions = new ProductDisc();
            this.ModularMoldDimensions.OuterDiameter = Math.Round(this.OuterDiameter * multiMoldFactorPu, 2);
            this.ModularMoldDimensions.InnerDiameter = Math.Round(this.InnerDiameter * multiMoldFactorPu, 2);
            this.ModularMoldDimensions.Height = Math.Round(this.Height * multiMoldFactorPu, 2);
            this.ModularMoldDimensions.MultiMoldFactorPU = this.MultiMoldFactorPU;

            if (!string.IsNullOrWhiteSpace(this.BTC))
            {
                this.ModularMoldDimensions.HcDiameter = Math.Round((decimal)this.HcDiameter * multiMoldFactorPu, 2);
                this.ModularMoldDimensions.HcHoleDiameter = Math.Round((decimal)this.HcHoleDiameter * multiMoldFactorPu, 2);
                this.ModularMoldDimensions.HcHoles = this.HcHoles;
                this.ModularMoldDimensions.BTC = this.BTC;
            }
        }

        public void AddSingleMoldDimensions(decimal singleMoldFactorPu)
        {
            this.SingleMoldDimensions = new ProductDisc();
            this.SingleMoldDimensions.OuterDiameter = Math.Round(this.OuterDiameter * singleMoldFactorPu, 2);
            this.SingleMoldDimensions.InnerDiameter = Math.Round(this.InnerDiameter * singleMoldFactorPu, 2);
            this.SingleMoldDimensions.Height = Math.Round(this.Height * singleMoldFactorPu, 2);
            this.SingleMoldDimensions.FactorPU = this.FactorPU;

            if (!string.IsNullOrWhiteSpace(this.BTC))
            {
                this.SingleMoldDimensions.HcDiameter = Math.Round((decimal)this.HcDiameter * singleMoldFactorPu, 2);
                this.SingleMoldDimensions.HcHoleDiameter = Math.Round((decimal)this.HcHoleDiameter * singleMoldFactorPu, 2);
                this.SingleMoldDimensions.HcHoles = this.HcHoles;
                this.SingleMoldDimensions.BTC = this.BTC;
            }
        }
    }
}
