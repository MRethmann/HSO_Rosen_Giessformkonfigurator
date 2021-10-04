//-----------------------------------------------------------------------
// <copyright file="SingleMoldDisc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SingleMoldDisc")]
    public partial class SingleMoldDisc : SingleMold
    {
        public SingleMoldDisc()
        {
            this.moldType = MoldType.SingleMold;
            this.moldTypeName = "Einteilige Gießform";
            this.productType = ProductType.Disc;
            this.productTypeName = "Scheibe";
        }

        public SingleMoldDisc(CoreSingleMold coreSingleMold)
        {
            this.coreSingleMold = coreSingleMold;
            this.moldType = MoldType.SingleMold;
            this.productType = ProductType.Disc;
        }



    }
}
