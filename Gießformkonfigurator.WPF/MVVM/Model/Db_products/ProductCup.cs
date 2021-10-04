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
    public partial class ProductCup : Product
    {
        [Column("BaseCup")]
        [StringLength(100)]
        public string BaseCup { get; set; }

        public decimal InnerDiameter { get; set; }

    }
}
