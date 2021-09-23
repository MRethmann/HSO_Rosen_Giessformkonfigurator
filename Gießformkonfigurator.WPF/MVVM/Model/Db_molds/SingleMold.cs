//-----------------------------------------------------------------------
// <copyright file="SingleMold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Enums;

namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    public partial class SingleMold : Mold
    {
        public SingleMold()
        {
            this.moldType = MoldType.Einteilige_Gießform;
        }
    }
}
