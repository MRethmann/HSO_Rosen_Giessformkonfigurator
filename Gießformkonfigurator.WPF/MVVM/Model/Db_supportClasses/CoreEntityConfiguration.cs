//-----------------------------------------------------------------------
// <copyright file="CoreEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    class CoreEntityConfiguration : EntityTypeConfiguration<Core>
    {
        public CoreEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.ToleranceOuterDiameter)
                .IsUnicode(false);

            this.Property(e => e.Height)
                .HasPrecision(10, 2);

            this.Property(e => e.FillHeightMax)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusMax)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusMin)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterKonusAngle)
                .HasPrecision(5, 2);

            this.Property(e => e.KonusHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.GuideHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.GuideDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.ToleranceGuideDiameter)
                .IsUnicode(false);

            this.Property(e => e.AdapterDiameter)
                .HasPrecision(10, 2);
        }
    }
}