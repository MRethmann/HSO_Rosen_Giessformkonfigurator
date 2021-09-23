//-----------------------------------------------------------------------
// <copyright file="Baseplate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Baseplate")]
    public partial class Baseplate : Component
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? OuterKonusMax { get; set; }

        public decimal? OuterKonusMin { get; set; }

        public decimal? OuterKonusAngle { get; set; }

        public decimal? KonusHeight { get; set; }

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

        public decimal? InnerDiameter { get; set; }

        [StringLength(10)]
        public string ToleranceInnerDiameter { get; set; }

        /// <summary>
        /// Intergrierter Kern
        /// </summary>
        public bool HasCore { get; set; }

        public decimal? Hc1Diameter { get; set; }

        public int? Hc1Holes { get; set; }

        public string Hc1Thread { get; set; }

        public decimal? Hc2Diameter { get; set; }

        public int? Hc2Holes { get; set; }

        public string Hc2Thread { get; set; }

        public decimal? Hc3Diameter { get; set; }

        public int? Hc3Holes { get; set; }

        public string Hc3Thread { get; set; }


        public override string ToString()
        {
            return ID.ToString() + " " + Description;
        }
    }
}
