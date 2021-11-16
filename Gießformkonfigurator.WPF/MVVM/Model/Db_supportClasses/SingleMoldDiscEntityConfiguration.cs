//-----------------------------------------------------------------------
// <copyright file="BaseplateEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;

    class SingleMoldDiscEntityConfiguration : EntityTypeConfiguration<SingleMoldDisc>
    {
        public SingleMoldDiscEntityConfiguration()
        {
            this.Property(e => e.Description)
                .IsUnicode(false);

            this.Property(e => e.OuterDiameter)
                .HasPrecision(10, 4);

            this.Property(e => e.Height)
                .HasPrecision(10, 4);

            this.Property(e => e.InnerDiameter)
                .HasPrecision(10, 4);

            this.Property(e => e.BTC)
                .IsUnicode(false);
        }
    }

}