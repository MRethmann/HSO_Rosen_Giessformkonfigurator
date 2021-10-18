//-----------------------------------------------------------------------
// <copyright file="Core.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Core")]
    public partial class Core : Component
    {
        public decimal OuterDiameter { get; set; }

        [StringLength(10)]
        public string ToleranceOuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? FillHeightMax { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the core has a Konusfuehrung.
        /// </summary>
        public bool HasKonus { get; set; }

        public decimal? OuterKonusMax { get; set; }

        public decimal? OuterKonusMin { get; set; }

        public decimal? OuterKonusAngle { get; set; }

        public decimal? KonusHeight { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the core has a Fuehrungsstift.
        /// </summary>
        public bool HasGuideBolt { get; set; }

        public decimal? GuideHeight { get; set; }

        public decimal? GuideDiameter { get; set; }

        public string ToleranceGuideDiameter { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the core has a Lochfuehrung.
        /// </summary>
        public bool HasHoleguide { get; set; }

        public decimal? AdapterDiameter { get; set; }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Description;
        }
    }
}
