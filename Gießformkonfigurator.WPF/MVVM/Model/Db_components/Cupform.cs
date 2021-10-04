//-----------------------------------------------------------------------
// <copyright file="Cupform.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cupform")]
    public partial class Cupform : Component
    {
        [StringLength(20)]
        public string CupType { get; set; }

        public decimal? InnerDiameter { get; set; }

        [StringLength(10)]
        public string ToleranceInnerDiameter { get; set; }

        public decimal? HoleCircle { get; set; }

        /// <summary>
        /// Führungsstift
        /// </summary>
        public bool HasGuideBolt { get; set; }

        /// <summary>
        /// Innengewinde
        /// </summary>
        public bool HasThread { get; set; }

        /// <summary>
        /// Konusführung
        /// </summary>
        public bool HasKonus { get; set; }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        /// <summary>
        /// Lochführung
        /// </summary>
        public bool HasHoleguide { get; set; }

        /// <summary>
        /// Innenkern
        /// </summary>
        public bool HasCore { get; set; }

        public override string ToString()
        {
            return ID.ToString() + " " + Description;
        }
    }
}
