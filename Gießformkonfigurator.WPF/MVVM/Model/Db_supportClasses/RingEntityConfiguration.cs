//-----------------------------------------------------------------------
// <copyright file="RingEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class RingEntityConfiguration : EntityTypeConfiguration<Ring>
    {
        public RingEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.ToleranceOuterDiameter)
                .IsUnicode(false);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.ToleranceInnerDiameter)
                .IsUnicode(false);

            this.Property(e => e.Height)
                .HasPrecision(10, 2);

            this.Property(e => e.FillHeightMax)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMax)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMin)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusAngle)
                .HasPrecision(5, 2);

            this.Property(e => e.KonusHeight)
                .HasPrecision(10, 2);
        }
    }
}