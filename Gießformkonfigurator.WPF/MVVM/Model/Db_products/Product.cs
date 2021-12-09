//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_products
{
    public abstract class Product
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal? FactorPU { get; set; }

        public decimal? HcDiameter { get; set; }

        public int? HcHoles { get; set; }

        public decimal? HcHoleDiameter { get; set; }

        public decimal MultiMoldFactorPU { get; set; }

        public string BTC { get; set; }

        public override string ToString()
        {
            return "SAP: " + this.ID + ", Description: " + this.Description;
        }
    }
}
