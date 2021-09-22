//-----------------------------------------------------------------------
// <copyright file="CoreSingleMoldEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class CoreSingleMoldEntityConfiguration : EntityTypeConfiguration<CoreSingleMold>
    {
        public CoreSingleMoldEntityConfiguration()
        {
            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 2);

            this.Property(e => e.Height)
                .HasPrecision(10, 2);

            this.Property(e => e.FactorPU)
                .HasPrecision(10, 5);
        }
    }
}