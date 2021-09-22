//-----------------------------------------------------------------------
// <copyright file="Mold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.Enums;

namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    
    

    public abstract class Mold
    {
        public MoldType moldType { get; set; }

        public ProductType productType { get; set; }
    }
}
