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
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal? Hc1Diameter { get; set; }

        public decimal? Hc1Holes { get; set; }

        public decimal? Hc1HoleDiameter { get; set; }

        public decimal? Hc2Diameter { get; set; }

        public decimal? Hc2Holes { get; set; }

        public decimal? Hc2HoleDiameter { get; set; }

        public decimal? Hc3Diameter { get; set; }

        public decimal? Hc3Holes { get; set; }

        public decimal? Hc3HoleDiameter { get; set; }
    }
}
