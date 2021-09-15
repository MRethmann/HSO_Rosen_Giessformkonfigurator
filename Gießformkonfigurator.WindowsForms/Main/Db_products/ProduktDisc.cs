//-----------------------------------------------------------------------
// <copyright file="ProduktDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProduktDisc")]
    public partial class ProduktDisc : Produkt
    {
        [Key]
        [Column("SAP-Nr.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SAP_Nr_ { get; set; }

        public decimal Außendurchmesser { get; set; }

        public decimal Hoehe { get; set; }

        public decimal Innendurchmesser { get; set; }

        public decimal Lk1Durchmesser { get; set; }

        public decimal Lk1Bohrungen { get; set; }

        public string Lk1Gewinde { get; set; }

        public decimal Lk2Durchmesser { get; set; }

        public decimal Lk2Bohrungen { get; set; }

        public string Lk2Gewinde { get; set; }

        public decimal Lk3Durchmesser { get; set; }

        public decimal Lk3Bohrungen { get; set; }

        public string Lk3Gewinde { get; set; }

        [Required]
        [StringLength(4)]
        public string LK { get; set; }
    }
}
