//-----------------------------------------------------------------------
// <copyright file="SingleMoldDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SingleMoldDisc")]
    public partial class SingleMoldDisc : Mold
    {
        [NotMapped]
        public Core core { get; set; }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal? OuterDiameter { get; set; }

        public decimal? InnerDiameter { get; set; }

        public decimal? Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public decimal? BoltDiameter { get; set; }

        public int? HcHoles { get; set; }


        public SingleMoldDisc()
        {
            this.moldType = MoldType.Einteilige_Gießform;
            this.productType = ProductType.Disc;
        }

        public SingleMoldDisc(Core core)
        {
            this.core = core;
            this.moldType = MoldType.Einteilige_Gießform;
            this.productType = ProductType.Disc;
        }



    }
}
