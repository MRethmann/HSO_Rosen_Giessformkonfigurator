//-----------------------------------------------------------------------
// <copyright file="CompareObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Used for save all information on the comparison between mold and product. Always contains one mold and one product + additional informations such as rating, alternative Components and BTC.
    /// </summary>
    public class CompareObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareObject"/> class.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="mold"></param>
        public CompareObject(Product product, Mold mold)
        {
            this.Mold = mold;
            this.Product = product;
        }

        public Mold Mold { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Gets or Sets the alternative Guide Rings. Used in ranking job to group entries.
        /// </summary>
        public List<Tuple<Ring, decimal>> AlternativeGuideRings { get; set; } = new List<Tuple<Ring, decimal>>();

        /// <summary>
        /// Gets or Sets the alternative Cores. Used in ranking job to group entries.
        /// </summary>
        public List<Tuple<Core, decimal>> AlternativeCores { get; set; } = new List<Tuple<Core, decimal>>();

        /// <summary>
        /// Gets or Sets Difference between prefered inner Diameter and actual inner Diameter which requires post processing.
        /// </summary>
        public decimal? DifferenceInnerDiameter { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Difference between prefered outer Diameter and actual outer Diameter which requires post processing.
        /// </summary>
        public decimal? DifferenceOuterDiameter { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Difference is used to determine the best bolts. Every bolt can be used for the product without post processing.
        /// </summary>
        public decimal? DifferenceBoltDiameter { get; set; }

        public List<string> PostProcessing { get; set; } = new List<string>();

        /// <summary>
        /// Gets or Sets the final rating base on differences between product and mold.
        /// </summary>
        public decimal? FinalRating { get; set; }

        /// <summary>
        /// Gets or Sets array which shows which BoltCircle of the baseplate is assigned to which circle of the product. Array indexes 1-3 are used for this.
        /// </summary>
        public bool[] BoltCirclesBaseplate { get; set; } = new bool[4];

        /// <summary>
        /// Gets or Sets array which shows which BoltCircle of the insertPlate is assigned to which circle of the product. Array indexes 1-3 are used for this.
        /// </summary>
        public bool[] BoltCirclesInsertPlate { get; set; } = new bool[4];

        /// <summary>
        /// Gets or Sets the bolts which are useable for the boltCircle. The tuple contains the bolt and its deviation to the product holes.
        /// </summary>
        public List<Tuple<Bolt, decimal?>> Bolts { get; set; } = new List<Tuple<Bolt, decimal?>>();
    }
}
