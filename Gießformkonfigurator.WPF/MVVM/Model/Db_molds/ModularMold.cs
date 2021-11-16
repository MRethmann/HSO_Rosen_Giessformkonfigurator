//-----------------------------------------------------------------------
// <copyright file="ModularMold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Giessformkonfigurator.WPF.Enums;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    ///  Consists of at least three components (Baseplate, GuideRing, Core) and a maximum of 9.
    /// </summary>
    public class ModularMold : Mold
    {
        public Baseplate Baseplate { get; set; }

        public Ring GuideRing { get; set; }

        public InsertPlate InsertPlate { get; set; }

        public Core Core { get; set; }

        public Cupform Cupform { get; set; }

        public List<Tuple<Ring, Ring, decimal?>> ListOuterRings { get; set; } = new List<Tuple<Ring, Ring, decimal?>>();

        public List<Tuple<Ring, Ring, decimal?>> ListCoreRings { get; set; } = new List<Tuple<Ring, Ring, decimal?>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularMold"/> class.
        /// </summary>
        /// <param name="baseplate">Baseplate.</param>
        /// <param name="ring">Ring.</param>
        /// <param name="insertPlate">Insertplate.</param>
        /// <param name="core">Core.</param>
        public ModularMold(Baseplate baseplate, Ring ring, InsertPlate insertPlate, Core core)
        {
            this.Baseplate = baseplate;
            this.GuideRing = ring;
            this.InsertPlate = insertPlate;
            this.Core = core;
            this.MoldType = MoldType.MultiMold;
            this.MoldTypeName = "Mehrteilige Gießform";
            this.ProductType = ProductType.Disc;
            this.ProductTypeName = "Scheibe";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularMold"/> class.
        /// </summary>
        /// <param name="cupform">Cupform.</param>
        /// <param name="core">Core.</param>
        /// <param name="insertPlate">Insertplate.</param>
        public ModularMold(Cupform cupform, Core core, InsertPlate insertPlate)
        {
            this.Cupform = cupform;
            this.Core = core;
            this.InsertPlate = insertPlate;
            this.MoldType = MoldType.MultiMold;
            this.MoldTypeName = "Mehrteilige Gießform";
            this.ProductType = ProductType.Cup;
            this.ProductTypeName = "Cup";
        }

        /*public override string ToString()
        {
            return "Grundplatte: " + this.Baseplate?.ID.ToString() + " + Einlegeplatte: " + this.InsertPlate?.ID.ToString() + " + FÜhrungsring: " + this.GuideRing?.ID.ToString() + " + Kern: " + this.Core?.ID.ToString();
        }*/
    }
}
