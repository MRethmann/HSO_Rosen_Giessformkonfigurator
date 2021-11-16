//-----------------------------------------------------------------------
// <copyright file="ProductCupEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;

    class ProductCupEntityConfiguration : EntityTypeConfiguration<ProductCup>
    {
        public ProductCupEntityConfiguration()
        {
            this.Property(e => e.Description)
                .IsUnicode(false);

            this.Property(e => e.CupType)
                .IsUnicode(false);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.FactorPU)
                .HasPrecision(10, 5);

            this.Property(e => e.Size)
                .HasPrecision(10, 2);

            this.Property(e => e.BTC)
                .IsUnicode(false);
        }
    }
}