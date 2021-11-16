//-----------------------------------------------------------------------
// <copyright file="Mold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.Enums;

    /// <summary>
    /// Superclass of ModularMold and SingleMold.
    /// </summary>
    public abstract class Mold
    {
        /// <summary>
        /// Gets or Sets type of Mold. Can be MultiMold or SingleMold.
        /// </summary>
        [NotMapped]
        public MoldType MoldType { get; set; }

        /// <summary>
        /// Gets or Sets MoldType as a string to be used for output in GUI.
        /// </summary>
        [NotMapped]
        public string MoldTypeName { get; set; }

        /// <summary>
        /// Gets or Sets type of Product. Can be Cup or Disc.
        /// </summary>
        [NotMapped]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Gets or Sets ProductType as a string to be used for output in GUI.
        /// </summary>
        [NotMapped]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the mold has a fitting BTC for the product or not.
        /// </summary>
        [NotMapped]
        public bool HasFittingBTC { get; set; } = false;
    }
}
