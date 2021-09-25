//-----------------------------------------------------------------------
// <copyright file="Mold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;
using Gießformkonfigurator.WPF.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    
    

    public abstract class Mold
    {
        [NotMapped]
        public MoldType moldType { get; set; }

        [NotMapped]
        public string moldTypeName { get; set; }

        [NotMapped]
        public ProductType productType { get; set; }

        [NotMapped]
        public string productTypeName { get; set; }
    }
}
