//-----------------------------------------------------------------------
// <copyright file="CompareObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using System;
    using System.Collections.Generic;

    class CompareObject
    {
        public Mold Mold { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Difference between prefered inner Diameter and actual inner Diameter which requires post processing.
        /// </summary>
        public decimal? differenceInnerDiameter { get; set; } = 0;

        /// <summary>
        /// Difference between prefered outer Diameter and actual outer Diameter which requires post processing.
        /// </summary>
        public decimal? differenceOuterDiameter { get; set; } = 0;

        /// <summary>
        /// Difference between prefered hole size and actual hole Size which requires post processing.
        /// </summary>
        public decimal? differenceBoltDiameter { get; set; }

        /// <summary>
        /// GGF. überflüssig. Shows if mold has holeCircle or not. If not it needs to be added manually in post processing.
        /// </summary>
        public bool holeCircle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? finalRating { get; set; }


        /// <summary>
        /// Array shows which BoltCircle of the mold is assigned to which circle of the product. Array index 1-3 is used for the boltcircles of the baseplate. Array index 4-6 is used for boltcircles of the insertPlate.
        /// </summary>
        public int[] boltCirclesBaseplate { get; set; } = new int[7];

        /// <summary>
        /// 
        /// </summary>
        public List<Tuple<Bolt, int, decimal?>> bolts { get; set; } = new List<Tuple<Bolt, int, decimal?>>();

        public CompareObject(Product product, Mold mold)
        {
            this.Mold = mold;
            this.Product = product;
        }
    }
}
