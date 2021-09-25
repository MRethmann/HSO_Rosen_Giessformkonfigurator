//-----------------------------------------------------------------------
// <copyright file="ModularMold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    class ModularMold : Mold
    {
        public Baseplate baseplate { get; set; }

        public Ring guideRing { get; set; }

        public InsertPlate insertPlate { get; set; }

        public Core core { get; set; }

        public Cupform cupform { get; set; }

        public List<Tuple<Ring, Ring, decimal?>> ListOuterRings { get; set; } = new List<Tuple<Ring, Ring, decimal?>>();

        public List<Tuple<Ring, Ring, decimal?>> ListCoreRings { get; set; } = new List<Tuple<Ring, Ring, decimal?>>();

        public ModularMold(Baseplate baseplate, Ring ring, InsertPlate insertPlate, Core core)
        {
            this.baseplate = baseplate;
            this.guideRing = ring;
            this.insertPlate = insertPlate;
            this.core = core;
            this.moldType = MoldType.MultiMold;
            this.moldTypeName = "Mehrteilige Gießform";
            this.productType = ProductType.Disc;
            this.productTypeName = "Scheibe";
        }

        public ModularMold(Cupform cupform, Core core, InsertPlate insertPlate)
        {
            this.cupform = cupform;
            this.core = core;
            this.insertPlate = insertPlate;
            this.moldType = MoldType.MultiMold;
            this.moldTypeName = "Mehrteilige Gießform";
            this.productType = ProductType.Cup;
            this.productTypeName = "Cup";
        }

        /*public override string ToString()
        {
            return this.Grundplatte this.Fuehrungsring, this.Einlegeplatte, this.Innenkern, this.Cupform, this.ListInnerRings[0], this.ListInnerRings[1], this.ListInnerRings[2];
        }*/
    }
}
