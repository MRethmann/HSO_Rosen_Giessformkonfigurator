//-----------------------------------------------------------------------
// <copyright file="ProductDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductDisc")]
    public class ProductDisc : Product
    {
        public ProductDisc()
        {
        }

        public ProductDisc(decimal multiMoldFactorPu, decimal singleMoldFactorPu)
            : base(multiMoldFactorPu, singleMoldFactorPu)
        {
            this.MultiMoldDimensions = new ProductDisc();
            this.MultiMoldDimensions.OuterDiameter = Math.Round(this.OuterDiameter * multiMoldFactorPu);
            this.MultiMoldDimensions.InnerDiameter = Math.Round(this.InnerDiameter * multiMoldFactorPu);
            this.MultiMoldDimensions.Height = Math.Round(this.Height * multiMoldFactorPu);
            this.MultiMoldDimensions.HcDiameter = this?.HcDiameter != null ? Math.Round((decimal)this.HcDiameter * multiMoldFactorPu) : 0.0m;
            this.MultiMoldDimensions.HcHoleDiameter = this?.HcHoleDiameter != null ? Math.Round((decimal)this.HcHoleDiameter * multiMoldFactorPu) : 0.0m;

            this.SingleMoldDimensions = new ProductDisc();
            this.SingleMoldDimensions.OuterDiameter = Math.Round(this.OuterDiameter * singleMoldFactorPu);
            this.SingleMoldDimensions.InnerDiameter = Math.Round(this.InnerDiameter * singleMoldFactorPu);
            this.SingleMoldDimensions.Height = Math.Round(this.Height * singleMoldFactorPu);
            this.SingleMoldDimensions.HcDiameter = this?.HcDiameter != null ? Math.Round((decimal)this.HcDiameter * singleMoldFactorPu) : 0.0m;
            this.SingleMoldDimensions.HcHoleDiameter = this?.HcHoleDiameter != null ? Math.Round((decimal)this.HcHoleDiameter * singleMoldFactorPu) : 0.0m;
        }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }

        /// <summary>
        /// Gets or Sets dimension information which is calculated by singleMold pu factor.
        /// </summary>
        [NotMapped]
        public ProductDisc SingleMoldDimensions { get; set; }

        /// <summary>
        /// Gets or Sets dimension information which is calculated by multiMold pu factor.
        /// </summary>
        [NotMapped]
        public ProductDisc MultiMoldDimensions { get; set; }
    }
}
