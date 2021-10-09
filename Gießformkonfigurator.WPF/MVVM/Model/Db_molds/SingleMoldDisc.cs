//-----------------------------------------------------------------------
// <copyright file="SingleMoldDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

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
    }
}
