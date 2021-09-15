//-----------------------------------------------------------------------
// <copyright file="BolzenEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WindowsForms.Main.Db_components;

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