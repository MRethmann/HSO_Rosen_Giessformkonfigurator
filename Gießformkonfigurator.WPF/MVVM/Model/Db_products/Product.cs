//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal? FactorPU { get; set; }

        public string BTC { get; set; }

        public override string ToString()
        {
            return "SAP: " + this.ID + ", Description: " + this.Description;
        }
    }
}
