//-----------------------------------------------------------------------
// <copyright file="SingleMoldCup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SingleMoldCup")]
    public partial class SingleMoldCup : SingleMold
    {
        public SingleMoldCup()
        {
            this.moldType = MoldType.SingleMold;
            this.moldTypeName = "Einteilige Gießform";
            this.productType = ProductType.Cup;
            this.productTypeName = "Cup";
        }

        public SingleMoldCup(CoreSingleMold coreSingleMold)
        {
            this.coreSingleMold = coreSingleMold;
            this.moldType = MoldType.SingleMold;
            this.moldTypeName = "Einteilige Gießform";
            this.productType = ProductType.Cup;
            this.productTypeName = "Cup";
        }

    }
}