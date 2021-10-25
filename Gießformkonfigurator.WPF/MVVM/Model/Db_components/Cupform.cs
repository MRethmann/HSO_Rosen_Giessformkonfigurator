//-----------------------------------------------------------------------
// <copyright file="Cupform.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cupform")]
    public partial class Cupform : Component
    {
        [StringLength(10)]
        public string CupType { get; set; }

        [StringLength(10)]
        public string Size { get; set; }

        [StringLength(10)]
        public string BTC { get; set; }

        public bool HasFixedBTC { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the Cupform has a Innengewinde.
        /// </summary>
        public bool HasThread { get; set; }

        [StringLength(10)]
        public string Thread { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the Cupform has a Konusfuehrung.
        /// </summary>
        public bool HasKonus { get; set; }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the Cupform has a Lochfuehrung.
        /// </summary>
        public bool HasHoleguide { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the Cupform has a Fuehrungsstift/Core.
        /// </summary>
        public bool HasGuideBolt { get; set; }

        public decimal? InnerDiameter { get; set; }

        [StringLength(10)]
        public string ToleranceInnerDiameter { get; set; }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Description;
        }
    }
}
