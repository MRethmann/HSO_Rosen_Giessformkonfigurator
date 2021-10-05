﻿//-----------------------------------------------------------------------
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

        public List<Tuple<Ring, decimal>> alternativeGuideRings { get; set; } = new List<Tuple<Ring, decimal>>();

        public List<Tuple<Core, decimal>> alternativeCores { get; set; } = new List<Tuple<Core, decimal>>();

        /// <summary>
        /// Difference between prefered inner Diameter and actual inner Diameter which requires post processing.
        /// </summary>
        public decimal? differenceInnerDiameter { get; set; } = 0;


        /// <summary>
        /// Difference between prefered outer Diameter and actual outer Diameter which requires post processing.
        /// </summary>
        public decimal? differenceOuterDiameter { get; set; } = 0;

        /// <summary>
        /// Difference is used to determine the best bolts. Every bolt can be used for the product without post processing.
        /// </summary>
        public decimal? differenceBoltDiameter { get; set; }

        public List<string> postProcessing { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public decimal? finalRating { get; set; }

        /// <summary>
        /// GGF. überflüssig. Shows if mold has holeCircle or not. If not it needs to be added manually in post processing.
        /// </summary>
        public bool holeCircle { get; set; }

        /// <summary>
        /// Array shows which BoltCircle of the mold is assigned to which circle of the product. Array index 1-3 is used for the boltcircles of the baseplate. Array index 4-6 is used for boltcircles of the insertPlate.
        /// </summary>
        public bool[] boltCirclesBaseplate { get; set; } = new bool[4];

        public bool[] boltCirclesInsertPlate { get; set; } = new bool[4];

        /// <summary>
        /// Bolts
        /// </summary>
        public List<Tuple<Bolt, decimal?>> bolts { get; set; } = new List<Tuple<Bolt, decimal?>>();

        public CompareObject(Product product, Mold mold)
        {
            this.Mold = mold;
            this.Product = product;
        }
    }
}
