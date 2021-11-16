//-----------------------------------------------------------------------
// <copyright file="Baseplate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Baseplate")]
    public partial class Baseplate : Component
    {
        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal OuterKonusMax { get; set; }

        public decimal OuterKonusMin { get; set; }

        public decimal OuterKonusAngle { get; set; }

        public decimal KonusHeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the baseplate has a Konusfuehrung.
        /// </summary>
        public bool HasKonus { get; set; }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        public bool HasOuterEdge { get; set; }

        public string BTC1 { get; set; }

        public string BTC1Thread { get; set; }

        public string BTC2 { get; set; }

        public string BTC2Thread { get; set; }

        public string BTC3 { get; set; }

        public string BTC3Thread { get; set; }

        public decimal? Hc1Diameter { get; set; }

        public int? Hc1Holes { get; set; }

        [StringLength(10)]
        public string Hc1Thread { get; set; }

        public decimal? Hc2Diameter { get; set; }

        public int? Hc2Holes { get; set; }

        [StringLength(10)]
        public string Hc2Thread { get; set; }

        public decimal? Hc3Diameter { get; set; }

        public int? Hc3Holes { get; set; }

        [StringLength(10)]
        public string Hc3Thread { get; set; }

        public override string ToString()
        {
            return this.ID.ToString() + ": " + this.Description;
        }
    }
}
