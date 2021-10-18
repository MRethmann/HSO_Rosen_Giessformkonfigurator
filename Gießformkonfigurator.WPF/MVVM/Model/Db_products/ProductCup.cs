//-----------------------------------------------------------------------
// <copyright file="ProductCup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductCup")]
    public class ProductCup : Product
    {
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
        public ProductCup ModularMoldDimensions { get; set; }

        public void AddMultiMoldDimensions(decimal multiMoldFactorPu)
        {
            throw new NotImplementedException();
        }

        public void AddSingleMoldDimensions(decimal singleMoldFactorPu)
        {
            throw new NotImplementedException();
        }
    }
}
