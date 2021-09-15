//-----------------------------------------------------------------------
// <copyright file="BoltCircleTypeEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class BoltCircleTypeEntityConfiguration : EntityTypeConfiguration<BoltCircleType>
    {
        public BoltCircleTypeEntityConfiguration()
        {
            this.Property(e => e.TypeDescription)
            .IsUnicode(false);

            this.Property(e => e.Angle)
                .HasPrecision(10, 2);

            this.Property(e => e.HolepairAngle)
                .HasPrecision(10, 2);

            this.Property(e => e.HoleDiameter)
                .HasPrecision(10, 2);

        }
    }
}