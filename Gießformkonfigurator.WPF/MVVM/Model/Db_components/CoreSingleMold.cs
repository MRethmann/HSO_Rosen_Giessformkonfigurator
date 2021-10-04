//-----------------------------------------------------------------------
// <copyright file="Core.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gießformkonfigurator.WPF.MVVM.Model.Db_components
{
    [Table("CoreSingleMold")]
    public partial class CoreSingleMold : Component
    {
        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? FactorPU { get; set; }

        public override string ToString()
        {
            return ID.ToString() + " " + Description;
        }
    }
}
