//-----------------------------------------------------------------------
// <copyright file="SingleMoldDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    class SingleMoldDisc : Mold
    {
        public Core core { get; set; }

        public int ID { get; set; }

        public string Description { get; set; }

        public decimal? OuterDiameter { get; set; }

        public decimal? InnerDiameter { get; set; }

        public decimal? Height { get; set; }

        public decimal? HcDiameter { get; set; }

        public decimal? BoltDiameter { get; set; }

        public decimal? HcHoles { get; set; }


        public SingleMoldDisc()
        {
            this.moldType = MoldType.Einteilige_Gießform;
            this.productType = ProductType.Disc;
        }

        public SingleMoldDisc(Core core)
        {
            this.core = core;
            this.moldType = MoldType.Einteilige_Gießform;
            this.productType = ProductType.Disc;
        }



    }
}
