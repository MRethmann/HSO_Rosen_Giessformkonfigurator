//-----------------------------------------------------------------------
// <copyright file="Ring.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ring")]
    public partial class Ring : Component
    {
        [Key]
        [Column("SAP-Nr.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SAP_Nr_ { get; set; }

        [Required]
        [StringLength(255)]
        public string Bezeichnung_RoCon { get; set; }

        public decimal Außendurchmesser { get; set; }

        public string Toleranz_Außendurchmesser { get; set; }

        public decimal Innendurchmesser { get; set; }

        [StringLength(10)]
        public string Toleranz_Innendurchmesser { get; set; }

        public decimal Hoehe { get; set; }

        public decimal Gießhoehe_Max { get; set; }

        public bool mit_Konusfuehrung { get; set; }

        public decimal? Konus_Max { get; set; }

        public decimal? Konus_Min { get; set; }

        public decimal? Konus_Winkel { get; set; }

        public decimal? Konus_Hoehe { get; set; }
    }
}
