//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="multiMoldFactorPu">Factor is set via user input.</param>
        /// <param name="singleMoldFactorPu">Factor is being calculated by product dimension. Larger products use the higher factor.</param>
        public Product(decimal multiMoldFactorPu, decimal singleMoldFactorPu)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class. Standard constructor.
        /// </summary>
        public Product()
        {
        }

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
