//-----------------------------------------------------------------------
// <copyright file="InsertPlateEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    class InsertPlateEntityConfiguration : EntityTypeConfiguration<InsertPlate>
    {
        public InsertPlateEntityConfiguration()
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

            this.Property(e => e.OuterKonusHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMax)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusMin)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerKonusAngle)
                .HasPrecision(5, 2);

            this.Property(e => e.InnerKonusHeight)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc1Diameter)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc2Diameter)
                .HasPrecision(10, 2);

            this.Property(e => e.Hc3Diameter)
                .HasPrecision(10, 2);

            this.Property(e => e.BTC1)
                .IsUnicode(false);

            this.Property(e => e.BTC2)
                .IsUnicode(false);

            this.Property(e => e.BTC3)
                .IsUnicode(false);

            this.Property(e => e.BTC1Thread)
                .IsUnicode(false);

            this.Property(e => e.BTC2Thread)
                .IsUnicode(false);

            this.Property(e => e.BTC3Thread)
                .IsUnicode(false);
        }
    }
}
