//-----------------------------------------------------------------------
// <copyright file="MGießform.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Db_molds
{
    using Gießformkonfigurator.WindowsForms.Main.Db_components;
    using System.Collections.Generic;

    public class ModularMold : Mold
    {
        private ModularMold mGießform;
        private string type;

        public Baseplate Grundplatte{ get; set; }

        public Ring Fuehrungsring { get; set; }

        public InsertPlate Einlegeplatte { get; set; }

        public Core Innenkern { get; set; }

        public Cupform Cupform { get; set; }

        public List<Ring> ListInnerRings = new List<Ring>();

        public ModularMold(Baseplate gp, Ring fr, InsertPlate el, Core ik)
        {
            this.type = "MGießform Disk";
            this.Grundplatte = gp;
            this.Fuehrungsring = fr;
            this.Einlegeplatte = el;
            this.Innenkern = ik;
        }

        public ModularMold(Cupform cf, Core ik, Bolt bl)
        {
            this.type = "MGießform Cup";
            this.Cupform = cf;
            this.Innenkern = ik;
        }
    }
}
