//-----------------------------------------------------------------------
// <copyright file="Einlegeplatte.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Gießformkonfigurator.WindowsForms.Main.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Insertplate")]
    public partial class InsertPlate : Component
    {
        [Key]
        [Column("SAP-Nr.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SAP_Nr_ { get; set; }

        [StringLength(100)]
        public string Bezeichnung_RoCon { get; set; }

        public decimal Außendurchmesser { get; set; }

        [StringLength(5)]
        public string Toleranz_Außendurchmesser { get; set; }

        public decimal Hoehe { get; set; }

        public decimal? Konus_Außen_Max { get; set; }

        public decimal? Konus_Außen_Min { get; set; }

        public decimal? Konus_Außen_Winkel { get; set; }

        public decimal? Konus_Hoehe { get; set; }

        public bool Mit_Konusfuehrung { get; set; }

        public decimal? Konus_Innen_Max { get; set; }

        public decimal? Konus_Innen_Min { get; set; }

        public decimal? Konus_Innen_Winkel { get; set; }

        public bool Mit_Lochfuehrung { get; set; }

        public decimal? Innendurchmesser { get; set; }

        [StringLength(5)]
        public string Toleranz_Innendurchmesser { get; set; }

        public bool Mit_Kern { get; set; }

        public decimal Lk1Durchmesser { get; set; }

        public decimal Lk1Bohrungen { get; set; }

        public string Lk1Gewinde { get; set; }

        public decimal Lk2Durchmesser { get; set; }

        public decimal Lk2Bohrungen { get; set; }

        public string Lk2Gewinde { get; set; }

        public decimal Lk3Durchmesser { get; set; }

        public decimal Lk3Bohrungen { get; set; }

        public string Lk3Gewinde { get; set; }

        public bool Kombiniere(Core kern)
        {
            if (this.Mit_Konusfuehrung)
            {
                return kern.Mit_Konusfuehrung == true
                            && this.Konus_Innen_Max > kern.Konus_Außen_Max
                            && (this.Konus_Innen_Max - 1) <= kern.Konus_Außen_Max
                            && this.Konus_Innen_Min > kern.Konus_Außen_Min
                            && (this.Konus_Innen_Min - 1) <= kern.Konus_Außen_Min
                            && this.Konus_Innen_Winkel == kern.Konus_Außen_Winkel;
            }
            else if (this.Mit_Lochfuehrung)
            {
                return kern.Mit_Fuehrungsstift == true
                    && this.Innendurchmesser == kern.Durchmesser_Fuehrung
                    && this.Hoehe >= kern.Hoehe_Fuehrung;
            }
            else
            {
                return false;
            }
        }
    }
}
