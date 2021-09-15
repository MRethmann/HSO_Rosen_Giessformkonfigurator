//-----------------------------------------------------------------------
// <copyright file="GießformDBContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses
{
    using System.Data.Entity;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Implementiert die Klasse DbContext. Konfiguration der DB Grundstruktur.
    /// </summary>
    public partial class GießformDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GießformDBContext"/> class.
        /// Stellt DB Verbindung her. Connection String befindet sich in der App.Config.
        /// </summary>
        public GießformDBContext()
            : base("name=GießformDB")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Bolt> Bolts { get; set; }

        public virtual DbSet<InsertPlate> InsertPlates { get; set; }

        public virtual DbSet<Baseplate> Baseplates { get; set; }

        public virtual DbSet<Core> Cores { get; set; }

        public virtual DbSet<ProductCup> ProductCups { get; set; }

        public virtual DbSet<ProductDisc> ProductDiscs { get; set; }

        public virtual DbSet<Ring> Rings { get; set; }

        public virtual DbSet<Cupform> Cupforms { get; set; }

        public virtual DbSet<BoltCircleType> BoltCircleTypes { get; set; }

        /// <summary>
        /// Initialisiert die EntityConfigurations für alle DB-Objekte.
        /// </summary>
        /// <param name="modelBuilder">tbd.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BaseplateEntityConfiguration());

            modelBuilder.Configurations.Add(new InsertPlateEntityConfiguration());

            modelBuilder.Configurations.Add(new BoltEntityConfiguration());

            modelBuilder.Configurations.Add(new CoreEntityConfiguration());

            modelBuilder.Configurations.Add(new ProductCupEntityConfiguration());

            modelBuilder.Configurations.Add(new ProductDiscEntityConfiguration());

            modelBuilder.Configurations.Add(new RingEntityConfiguration());

            modelBuilder.Configurations.Add(new CupformEntityConfiguration());

            modelBuilder.Configurations.Add(new BoltCircleTypeEntityConfiguration());
        }
    }
}
