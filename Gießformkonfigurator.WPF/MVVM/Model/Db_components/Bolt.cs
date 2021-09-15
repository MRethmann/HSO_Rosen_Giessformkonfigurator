//-----------------------------------------------------------------------
// <copyright file="Bolt.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bolt")]
    public partial class Bolt : Component
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal Height { get; set; }

        public decimal? OuterDiameter { get; set; }

        public decimal FillHeightMax { get; set; }

        /// <summary>
        /// Gewinde
        /// </summary>
        public bool HasThread { get; set; }

        public string Thread { get; set; }

        /// <summary>
        /// Steckbolzen
        /// </summary>
        public bool HasGuideBolt { get; set; }

        public decimal? GuideHeight { get; set; }

        public decimal? GuideOuterDiameter { get; set; }

        public override string ToString()
        {
            return ID.ToString() + " " + Description;
        }
    }
}
