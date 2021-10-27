//-----------------------------------------------------------------------
// <copyright file="CupformEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    class CupformEntityConfiguration : EntityTypeConfiguration<Cupform>
    {
        public CupformEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.Size)
            .HasPrecision(10, 2);

            this.Property(e => e.CupType)
            .IsUnicode(false);

            this.Property(e => e.InnerDiameter)
            .HasPrecision(10, 2);

            this.Property(e => e.BTC)
            .IsUnicode(false);
        }
    }
}