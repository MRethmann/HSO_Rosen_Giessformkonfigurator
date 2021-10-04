//-----------------------------------------------------------------------
// <copyright file="Ring.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_components
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ring")]
    public partial class Ring : Component
    {
        public decimal OuterDiameter { get; set; }

        public string ToleranceOuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        [StringLength(10)]
        public string ToleranceInnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal FillHeightMax { get; set; }

        public bool HasKonus { get; set; }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        public decimal? KonusHeight { get; set; }

        public override string ToString()
        {
            return ID.ToString() + " " + Description;
        }
    }
}
