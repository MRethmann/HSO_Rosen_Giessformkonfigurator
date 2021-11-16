//-----------------------------------------------------------------------
// <copyright file="Bolt.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bolt")]
    public partial class Bolt : Component
    {
        public decimal Height { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal FillHeightMax { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the bolt has a Gewinde.
        /// </summary>
        public bool HasThread { get; set; }

        [StringLength(10)]
        public string Thread { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the bolt has a Steckbolzen.
        /// </summary>
        public bool HasGuideBolt { get; set; }

        public decimal? GuideHeight { get; set; }

        public decimal? GuideOuterDiameter { get; set; }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Description;
        }
    }
}
