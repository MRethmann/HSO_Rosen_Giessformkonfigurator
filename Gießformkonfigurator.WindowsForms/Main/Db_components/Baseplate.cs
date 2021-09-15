//-----------------------------------------------------------------------
// <copyright file="Grundplatte.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Gießformkonfigurator.WindowsForms.Main.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Baseplate")]
    public partial class Baseplate : Component
    {
        [Key]
        [Column("SAP-Nr.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SAP_Nr_ { get; set; }

        [StringLength(255)]
        public string Bezeichnung_RoCon { get; set; }

        public decimal Außendurchmesser { get; set; }

        public decimal Hoehe { get; set; }

        public decimal Konus_Außen_Max { get; set; }

        public decimal Konus_Außen_Min { get; set; }

        public decimal Konus_Außen_Winkel { get; set; }

        public decimal Konus_Hoehe { get; set; }

        public bool Mit_Konusfuehrung { get; set; }

        public decimal? Konus_Innen_Max { get; set; }

        public decimal? Konus_Innen_Min { get; set; }

        public decimal? Konus_Innen_Winkel { get; set; }

        public bool Mit_Lochfuehrung { get; set; }

        public decimal? Innendurchmesser { get; set; }

        [StringLength(10)]
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

        /// <summary>
        /// Ruft die Methode Kombiniere() des Objekts auf. Beinhaltet die Parameter, welche mit der übergebenen Komponente verglichen werden.
        /// </summary>
        /// <param name="guideRing">Vergleichskomponente</param>
        /// <returns>Gibt einen True zurück, wenn die Komponenten kombiniert werden können.</returns>
        public bool Kombiniere(Ring guideRing)
        {
            return guideRing.Konus_Min > this.Konus_Außen_Min
                && guideRing.Konus_Min < this.Konus_Außen_Max
                && guideRing.Konus_Winkel == this.Konus_Außen_Winkel
                && guideRing.Konus_Hoehe < this.Konus_Hoehe
                && guideRing.Konus_Max < this.Konus_Außen_Max;
        }

        // TODO: Wie groß muss der Größenunterschied sein, damit die Platten ineinander passen?
        public bool Kombiniere(InsertPlate insertPlate)
        {
            return this.Konus_Innen_Max > insertPlate.Konus_Außen_Max
                    && (this.Konus_Innen_Max - 1) <= insertPlate.Konus_Außen_Max
                    && this.Konus_Innen_Min > insertPlate.Konus_Außen_Min
                    && (this.Konus_Innen_Min - 1) <= insertPlate.Konus_Außen_Min
                    && this.Konus_Innen_Winkel == insertPlate.Konus_Außen_Winkel;
                    // && (this.Konus_Innen_Winkel - 1) < einlegeplatte.Konus_Außen_Winkel;
        }

        public bool Kombiniere(Core core)
        {
            if (this.Mit_Konusfuehrung)
            {
                return core.Mit_Konusfuehrung == true
                        && this.Konus_Innen_Max > core.Konus_Außen_Max
                        && (this.Konus_Innen_Max - 1) <= core.Konus_Außen_Max
                        && this.Konus_Innen_Min > core.Konus_Außen_Min
                        && (this.Konus_Innen_Min - 1) <= core.Konus_Außen_Min
                        && this.Konus_Innen_Winkel == core.Konus_Außen_Winkel;
                        // && (this.Konus_Innen_Winkel - 5) < kern.Konus_Außen_Winkel;
            }

            // Grundplatte mit Lochführung akzeptiert einen Kern mit Fuehrungsstift oder Lochfuehrung
            else if (this.Mit_Lochfuehrung && core.Mit_Fuehrungsstift)
            {
                // TODO: Genaue Abweichung zwischen Innendurchmesser und Fuehrungsdurchmesser festlegen.
                return this.Innendurchmesser >= core.Durchmesser_Fuehrung
                    && (this.Innendurchmesser - 2) <= core.Durchmesser_Fuehrung
                    && this.Hoehe >= core.Hoehe_Fuehrung;
            }
            else if (this.Mit_Lochfuehrung && core.Mit_Lochfuehrung)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
