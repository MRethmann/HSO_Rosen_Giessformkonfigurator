//-----------------------------------------------------------------------
// <copyright file="Mold.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Gießformkonfigurator.WPF.Core;

namespace Gießformkonfigurator.WPF.MVVM.Model.Db_molds
{
    abstract class Mold : ObservableObject
    {
        public string type { get; set; }

    }
}
