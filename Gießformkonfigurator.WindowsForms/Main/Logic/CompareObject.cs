//-----------------------------------------------------------------------
// <copyright file="CompareObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms.Main.Logic
{
    using Gießformkonfigurator.WindowsForms.Main.Db_molds;
    using Gießformkonfigurator.WindowsForms.Main.Db_products;

    public class CompareObject
    {
        public CompareObject(Mold gf, Produkt pr)
        {
            this.Gießform = gf;
            this.Produkt = pr;
        }

        public Mold Gießform { get; set; }

        public Produkt Produkt { get; set; }
    }
}
