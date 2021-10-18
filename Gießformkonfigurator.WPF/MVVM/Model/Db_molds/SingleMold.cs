//-----------------------------------------------------------------------
// <copyright file="SingleMold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Giessformkonfigurator.WPF.Enums;
using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giessformkonfigurator.WPF.MVVM.Model.Db_molds
{
    /// <summary>
    /// Derived Class from Mold. Superclass of SingleMoldDisc and SingleMoldCup.
    /// </summary>
    public abstract class SingleMold : Mold
    {
        /// <summary>
        /// Gets or Sets the potential Core of the SingleMoldObject.
        /// </summary>
        [NotMapped]
        public CoreSingleMold CoreSingleMold { get; set; }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public decimal? BoltDiameter { get; set; }

        public int? HcHoles { get; set; }

        /// <summary>
        /// Used to Output the essential SingleMold ínformations.
        /// </summary>
        /// <returns>SAP and Description.</returns>
        public override string ToString()
        {
            return "SAP: " + this.ID + ", Description: " + this.Description;
        }
    }
}
