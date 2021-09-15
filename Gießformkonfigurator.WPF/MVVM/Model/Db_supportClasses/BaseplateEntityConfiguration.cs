//-----------------------------------------------------------------------
// <copyright file="BaseplateEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class BaseplateEntityConfiguration : EntityTypeConfiguration<Baseplate>
    {
        public BaseplateEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
            .HasPrecision(10, 2);

            this.Property(e => e.Height)
            .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusMax)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusMin)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusAngle)
                .HasPrecision(5, 2);

            this.Property(e => e.KonusHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMax)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMin)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusAngle)
                .HasPrecision(5, 2);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.ToleranceInnerDiameter)
                .IsUnicode(false);

            this.Property(e => e.Hc1Holes)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc1Diameter)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc2Holes)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc2Diameter)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc3Holes)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc3Diameter)
                .HasPrecision(10, 2);

        }
    }

}