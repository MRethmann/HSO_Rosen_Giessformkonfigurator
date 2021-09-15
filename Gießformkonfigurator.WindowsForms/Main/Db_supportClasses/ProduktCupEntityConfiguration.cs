//-----------------------------------------------------------------------
// <copyright file="ProduktCupEntityConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_supportClasses
{
    using System.Data.Entity.ModelConfiguration;
    using Gießformkonfigurator.WindowsForms.Main.Db_products;

    class ProduktCupEntityConfiguration : EntityTypeConfiguration<ProduktCup>
    {
        public ProduktCupEntityConfiguration()
        {
                this.Property(e => e.Grund_Cup)
                .IsUnicode(false);

                this.Property(e => e.Innendurchmesser)
                .HasPrecision(10, 2);

                this.Property(e => e.LK)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}