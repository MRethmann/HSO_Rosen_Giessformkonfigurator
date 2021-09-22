//-----------------------------------------------------------------------
// <copyright file="BaseplateEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;

    class SingleMoldCupEntityConfiguration : EntityTypeConfiguration<SingleMoldCup>
    {
        public SingleMoldCupEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
            .HasPrecision(10, 2);

            this.Property(e => e.Height)
            .HasPrecision(10, 2);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.HcHoles)
                .HasPrecision(10, 2);

            this.Property(e => e.HcDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.BoltDiameter)
                .HasPrecision(10, 2);
        }
    }

}