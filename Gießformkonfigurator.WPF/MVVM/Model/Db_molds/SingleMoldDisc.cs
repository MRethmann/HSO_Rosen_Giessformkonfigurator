//-----------------------------------------------------------------------
// <copyright file="SingleMoldDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Giessformkonfigurator.WPF.Enums;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    [Table("SingleMoldDisc")]
    public partial class SingleMoldDisc : SingleMold
    {
        public SingleMoldDisc()
        {
            this.MoldType = MoldType.SingleMold;
            this.MoldTypeName = "Einteilige Gießform";
            this.ProductType = ProductType.Disc;
            this.ProductTypeName = "Scheibe";
        }

        public SingleMoldDisc(CoreSingleMold coreSingleMold)
        {
            this.CoreSingleMold = coreSingleMold;
            this.MoldType = MoldType.SingleMold;
            this.ProductType = ProductType.Disc;
        }

        public decimal Height { get; set; }

        public decimal OuterDiameter { get; set; }
    }
}
