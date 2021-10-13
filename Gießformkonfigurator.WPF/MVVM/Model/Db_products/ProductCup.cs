//-----------------------------------------------------------------------
// <copyright file="ProductCup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductCup")]
    public class ProductCup : Product
    {
        public ProductCup()
        {
        }

        public ProductCup(decimal multiMoldFactorPu, decimal singleMoldFactorPu)
            : base(multiMoldFactorPu, singleMoldFactorPu)
        {
            // TODO: Add dimension information.
            this.MultiMoldDimensions = new ProductCup();

            this.SingleMoldDimensions = new ProductCup();
        }

        [Column("BaseCup")]
        [StringLength(100)]
        public string BaseCup { get; set; }

        public decimal InnerDiameter { get; set; }

        /// <summary>
        /// Used to save dimension information which is calculated by singleMold pu factor.
        /// </summary>
        [NotMapped]
        public ProductCup SingleMoldDimensions { get; set; }

        /// <summary>
        /// Used to save dimension information which is calculated by multiMold pu factor.
        /// </summary>
        [NotMapped]
        public ProductCup MultiMoldDimensions { get; set; }
    }
}
