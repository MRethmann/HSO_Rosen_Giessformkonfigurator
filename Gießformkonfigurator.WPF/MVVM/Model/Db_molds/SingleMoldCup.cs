//-----------------------------------------------------------------------
// <copyright file="SingleMoldCup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SingleMoldCup")]
    public partial class SingleMoldCup : SingleMold
    {
        [NotMapped]
        public CoreSingleMold coreSingleMold { get; set; }

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


        public SingleMoldCup()
        {
            this.moldType = MoldType.Einteilige_Gießform_Cup;
            this.productType = ProductType.Cup;
        }

        public SingleMoldCup(CoreSingleMold coreSingleMold)
        {
            this.coreSingleMold = coreSingleMold;
            this.moldType = MoldType.Einteilige_Gießform_Cup;
            this.productType = ProductType.Cup;
        }

    }
}