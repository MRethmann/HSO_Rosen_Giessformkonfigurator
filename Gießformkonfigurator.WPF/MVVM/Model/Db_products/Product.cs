//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Product
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal? FactorPU { get; set; }

        [NotMapped]
        public decimal? HcDiameter { get; set; }

        [NotMapped]
        public int? HcHoles { get; set; }

        [NotMapped]
        public decimal? HcHoleDiameter { get; set; }

        [NotMapped]
        public decimal MultiMoldFactorPU { get; set; }

        [StringLength(10)]
        public string BTC { get; set; }

        public override string ToString()
        {
            return "SAP: " + this.ID + ", Description: " + this.Description;
        }
    }
}
