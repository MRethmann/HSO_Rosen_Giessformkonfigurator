//-----------------------------------------------------------------------
// <copyright file="BoltEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class BoltEntityConfiguration : EntityTypeConfiguration<Bolt>
    {
        public BoltEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.Height)
                .HasPrecision(10, 2);

            this.Property(e => e.OuterDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.FillHeightMax)
                .HasPrecision(10, 2);

            this.Property(e => e.Thread)
                .IsUnicode(false);

            this.Property(e => e.GuideHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.GuideOuterDiameter)
                .HasPrecision(10, 2);
        }
    }
}