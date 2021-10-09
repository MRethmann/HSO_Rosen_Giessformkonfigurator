//-----------------------------------------------------------------------
// <copyright file="SingleMoldCup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Gießformkonfigurator.WPF.Enums;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    [Table("SingleMoldCup")]
    public partial class SingleMoldCup : SingleMold
    {
        public SingleMoldCup()
        {
            this.MoldType = MoldType.SingleMold;
            this.MoldTypeName = "Einteilige Gießform";
            this.ProductType = ProductType.Cup;
            this.ProductTypeName = "Cup";
        }

        public SingleMoldCup(CoreSingleMold coreSingleMold)
        {
            this.CoreSingleMold = coreSingleMold;
            this.MoldType = MoldType.SingleMold;
            this.MoldTypeName = "Einteilige Gießform";
            this.ProductType = ProductType.Cup;
            this.ProductTypeName = "Cup";
        }
    }
}