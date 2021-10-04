//-----------------------------------------------------------------------
// <copyright file="ProductDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductDisc")]
    public partial class ProductDisc : Product
    {
        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }
    }
}
