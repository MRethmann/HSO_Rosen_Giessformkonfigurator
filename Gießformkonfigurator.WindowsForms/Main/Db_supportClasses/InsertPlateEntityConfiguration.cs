//-----------------------------------------------------------------------
// <copyright file="EinlegeplatteEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WindowsForms.Main.Db_components;

    class InsertPlateEntityConfiguration : EntityTypeConfiguration<InsertPlate>
    {
        public InsertPlateEntityConfiguration()
        {

            this.Property(e => e.Bezeichnung_RoCon)
            .IsUnicode(false);

            this.Property(e => e.Außendurchmesser)
            .HasPrecision(10, 2);

            this.Property(e => e.Toleranz_Außendurchmesser)
                .IsUnicode(false);

            this.Property(e => e.Hoehe)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Außen_Max)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Außen_Min)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Außen_Winkel)
                .HasPrecision(5, 2);

            this.Property(e => e.Konus_Hoehe)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Innen_Max)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Innen_Min)
                .HasPrecision(10, 2);

            this.Property(e => e.Konus_Innen_Winkel)
                .HasPrecision(5, 2);

            this.Property(e => e.Innendurchmesser)
                .HasPrecision(10, 2);

            this.Property(e => e.Toleranz_Innendurchmesser)
                .IsUnicode(false);

            this.Property(e => e.Lk1Bohrungen)
                .HasPrecision(10, 2);

            this.Property(e => e.Lk1Durchmesser)
                .HasPrecision(10, 2);

            this.Property(e => e.Lk2Bohrungen)
                .HasPrecision(10, 2);

            this.Property(e => e.Lk2Durchmesser)
                .HasPrecision(10, 2);

            this.Property(e => e.Lk3Bohrungen)
                .HasPrecision(10, 2);

            this.Property(e => e.Lk3Durchmesser)
                .HasPrecision(10, 2);
        }
    }
}
