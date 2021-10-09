﻿//-----------------------------------------------------------------------
// <copyright file="CombinationJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
#pragma warning disable SA1519 // Braces should not be omitted from multi-line child statement
#pragma warning disable SA1623 // Property summary documentation should match accessors
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Combines modular components to create multi molds.
    /// </summary>
    public class CombinationJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationJob"/> class.
        /// </summary>
        /// <param name="produktParam">Das ausgewählte Produkt wird übergeben.
        /// <param name="sapnr">PK in der DB.</param>
        public CombinationJob(Product product, FilterJob filterJob)
        {
            this.ProduktDisc = (ProductDisc)product;

            this.ListBaseplates = new List<Baseplate>(filterJob.ListBaseplates);
            this.ListRings = new List<Ring>(filterJob.ListRings);
            this.ListInsertPlates = new List<InsertPlate>(filterJob.ListInsertPlates);
            this.ListCores = new List<Core>(filterJob.ListCores);
            this.ListBolts = new List<Bolt>(filterJob.ListBolts);
            this.ListCupforms = new List<Cupform>(filterJob.ListCupforms);
            this.ListCoresSingleMold = new List<CoreSingleMold>(filterJob.ListCoresSingleMold);
            this.ListSingleMoldCups = new List<SingleMoldCup>(filterJob.ListSingleMoldCups);
            this.ListSingleMoldDiscs = new List<SingleMoldDisc>(filterJob.ListSingleMoldDiscs);

            this.CombinationRuleSet = new CombinationRuleset();

            this.CombineModularDiscMold();
            this.CombineModularCupMold();
            this.CombineSingleDiscMold();
            this.CombineSingleCupMold();
        }

        // Frage: Wie kann man das universell umsetzen?
        public ProductDisc ProduktDisc { get; set; }

        public ProductCup ProduktCup { get; set; }

        public List<Baseplate> ListBaseplates { get; set; } = new List<Baseplate>();

        public List<Ring> ListRings { get; set; } = new List<Ring>();

        public List<InsertPlate> ListInsertPlates { get; set; } = new List<InsertPlate>();

        public List<Core> ListCores { get; set; } = new List<Core>();

        public List<Bolt> ListBolts { get; set; } = new List<Bolt>();

        public List<Cupform> ListCupforms { get; set; } = new List<Cupform>();

        /// <summary>
        /// Gets or Sets the final output of the modular mold combination algorithm.
        /// </summary>
        public List<ModularMold> ModularMoldsOutput { get; set; }

        /// <summary>
        /// Gets or Sets the final output of the single mold combination algorithm.
        /// </summary>
        public List<SingleMoldDisc> SingleMoldDiscOutput { get; set; }

        public List<SingleMoldCup> SingleMoldCupOutput { get; set; }

        /// <summary>
        /// Gets or Sets the list of single mold disc received from the filterJob.
        /// </summary>
        public List<SingleMoldDisc> ListSingleMoldDiscs { get; set; }

        public List<SingleMoldCup> ListSingleMoldCups { get; set; }

        public List<CoreSingleMold> ListCoresSingleMold { get; set; }

        /// <summary>
        /// Gets of Sets the ruleset that is used to combine the components.
        /// </summary>
        public CombinationRuleset CombinationRuleSet { get; set; }

        /// <summary>
        /// Speichert den aktuellen Listenindex.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine components to create modular molds for disc products.
        /// </summary>
        [STAThread]
        public void CombineModularDiscMold()
        {
            // Lists, used to temporarely save multi molds while they are prepared for final output.
            // Listen, welche zur Zwischenspeicherung der mehrteiligen Gießformen genutzt werden, bevor sie vervollständigt wurden und ausgegeben werden können.
            List<ModularMold> discMoldsTemp01 = new List<ModularMold>();
            List<ModularMold> discMoldsTemp02 = new List<ModularMold>();

            // Baseplates --> GuideRings
            for (int iGP = 0; iGP < this.ListBaseplates.Count; iGP++)
            {
                for (int iRinge = 0; iRinge < this.ListRings.Count; iRinge++)
                {
                    if (this.CombinationRuleSet.Combine(this.ListBaseplates[iGP], this.ListRings[iRinge]))
                    {
                        discMoldsTemp01.Add(new ModularMold(this.ListBaseplates[iGP], this.ListRings[iRinge], null, null));
                    }
                }
            }

            // MultiMolds (Baseplates with guideRings) --> InsertPlates
            this.Index = discMoldsTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                if (discMoldsTemp01[iTemp].Baseplate.HasKonus)
                {
                    for (int iEP = 0; iEP < this.ListInsertPlates.Count; iEP++)
                    {
                        if (this.CombinationRuleSet.Combine(discMoldsTemp01[iTemp].Baseplate, this.ListInsertPlates[iEP]))
                        {
                            discMoldsTemp01.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, this.ListInsertPlates[iEP], null));
                        }
                    }
                }
            }

            // MultiMolds (Baseplates, GuideRings, InsertPlates) --> Cores
            this.Index = discMoldsTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                for (int iKerne = 0; iKerne < this.ListCores.Count; iKerne++)
                {
                    // Einlegeplatte vorhanden
                    if (discMoldsTemp01[iTemp].InsertPlate != null)
                    {
                        // Einlegeplatten mit Konusfuehrung und Lochfuehrung
                        if ((discMoldsTemp01[iTemp].InsertPlate.HasKonus || discMoldsTemp01[iTemp].InsertPlate.HasHoleguide) && this.CombinationRuleSet.Combine(discMoldsTemp01[iTemp].InsertPlate, this.ListCores[iKerne]))
                        {
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, discMoldsTemp01[iTemp].InsertPlate, this.ListCores[iKerne]));
                        }

                        // Einlegeplatte mit Kern
                        else if (discMoldsTemp01[iTemp].InsertPlate.HasCore == true)
                        {
                            // TODO: Hier am besten einen "virtuelle" Kern erstellen, der die Maße der Einlegeplatte bekommt.
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, discMoldsTemp01[iTemp].InsertPlate, null));
                        }
                    }

                    // Ohne Einlegeplatte
                    else if (discMoldsTemp01[iTemp].InsertPlate == null)
                    {
                        // Grundplatten mit Konusfuehrung und Lochfuehrung
                        if ((discMoldsTemp01[iTemp].Baseplate.HasKonus == true || discMoldsTemp01[iTemp].Baseplate.HasHoleguide == true) && this.CombinationRuleSet.Combine(discMoldsTemp01[iTemp].Baseplate, this.ListCores[iKerne]))
                        {
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, null, this.ListCores[iKerne]));
                        }

                        // Grundplatte mit Kern
                        else if (discMoldsTemp01[iTemp].Baseplate.HasCore == true)
                        {
                            // TODO: Hier am besten einen "virtuelle" Kern erstellen, der die Maße der Grundplatte bekommt.
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, null, null));
                        }
                    }
                }
            }

            // MultiMolds (Baseplates, GuideRings, InsertPlates, Cores) --> OuterRings + CoreRings
            this.Index = discMoldsTemp02.Count;

            foreach (var mold in discMoldsTemp02)
            {
                foreach (var ring in this.ListRings)
                {
                    // Alle Ringe entfernen, die potentiell außerhalb des Guide Rings liegen könnten
                    if (ring.OuterDiameter < mold.GuideRing.InnerDiameter + 1)
                    {
                        if (this.CombinationRuleSet.Combine(mold.GuideRing, ring))
                        {
                            var differenceOuterDiameter = ring.InnerDiameter - this.ProduktDisc.OuterDiameter;
                            mold.ListOuterRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceOuterDiameter));
                        }
                        else if (this.CombinationRuleSet.Combine(mold.Core, ring))
                        {
                            var differenceInnerDiameter = this.ProduktDisc.InnerDiameter - ring.OuterDiameter;
                            mold.ListCoreRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceInnerDiameter));
                        }
                    }
                }
            }

            foreach (var mold in discMoldsTemp02)
            {
                var counter01 = mold.ListCoreRings.Count;
                for (int i = 0; i < counter01; i++)
                {
                    foreach (var ring in this.ListRings)
                    {
                        if (ring.InnerDiameter + 1 > mold.Core.OuterDiameter)
                        {
                            if (ring.OuterDiameter > mold.Core.OuterDiameter)
                            {
                                if (this.CombinationRuleSet.Combine(mold.ListCoreRings[i].Item1, ring))
                                {
                                    var differenceInnerDiameter = this.ProduktDisc.InnerDiameter - ring.OuterDiameter;
                                    mold.ListCoreRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListCoreRings[i].Item1, ring, differenceInnerDiameter));
                                }
                            }
                        }
                    }
                }

                var counter02 = mold.ListOuterRings.Count;
                for (int i = 0; i < counter02; i++)
                {
                    foreach (var ring in this.ListRings)
                    {
                        if (ring.OuterDiameter < mold.GuideRing.InnerDiameter + 1)
                        {
                            if (ring.InnerDiameter < mold.GuideRing.OuterDiameter)
                            {
                                if (this.CombinationRuleSet.Combine(mold.ListOuterRings[i].Item1, ring))
                                {
                                    var differenceOuterDiameter = ring.InnerDiameter - this.ProduktDisc.OuterDiameter;
                                    mold.ListOuterRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListOuterRings[i].Item1, ring, differenceOuterDiameter));
                                }
                            }
                        }
                    }
                }
            }

            // Order lists of coreRings and outerRings by rating.
            foreach (var mold in discMoldsTemp02)
            {
                mold.ListCoreRings.OrderBy(o => o.Item3);
                mold.ListOuterRings.OrderBy(o => o.Item3);
            }

            this.ModularMoldsOutput = new List<ModularMold>(discMoldsTemp02);
        }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine components to create modular molds for cup products.
        /// </summary>
        public void CombineModularCupMold()
        {
            List<ModularMold> cupMoldsTemp01 = new List<ModularMold>();

            // Combine cupform with insertPlates
            foreach (var cupform in this.ListCupforms)
            {
                foreach (var insertPlate in this.ListInsertPlates)
                {
                    if (this.CombinationRuleSet.Combine(cupform, insertPlate))
                    {
                        cupMoldsTemp01.Add(new ModularMold(cupform, null, insertPlate));
                    }
                }
            }

            // Combine cupform with cores
            foreach (var modularMold in cupMoldsTemp01)
            {
                foreach (var core in this.ListCores)
                {
                    // Case that cupform has an insertPlate --> compare insertPlate with core
                    if (modularMold.InsertPlate != null)
                    {
                        if (this.CombinationRuleSet.Combine(modularMold.InsertPlate, core))
                        {
                            this.ModularMoldsOutput.Add(new ModularMold(modularMold.Cupform, core, modularMold.InsertPlate));
                        }
                    }

                    // Case that cupform has no insertPlate --> compare cupform directly with core
                    else if (modularMold.InsertPlate == null)
                    {
                        if (this.CombinationRuleSet.Combine(modularMold.Cupform, core))
                        {
                            this.ModularMoldsOutput.Add(new ModularMold(modularMold.Cupform, core, modularMold.InsertPlate));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine singleMoldDisc with optional Cores.
        /// </summary>
        public void CombineSingleDiscMold()
        {
            this.SingleMoldDiscOutput = new List<SingleMoldDisc>();

            foreach (var singleMoldDisc in this.ListSingleMoldDiscs)
            {
                foreach (var coreSingleMold in this.ListCoresSingleMold)
                {
                    if (singleMoldDisc.InnerDiameter <= coreSingleMold.InnerDiameter
                        && singleMoldDisc.InnerDiameter <= coreSingleMold.InnerDiameter - 2
                        && coreSingleMold.OuterDiameter < (singleMoldDisc.HcDiameter - (singleMoldDisc.BoltDiameter / 2)))
                    {
                        singleMoldDisc.CoreSingleMold = coreSingleMold;
                        this.SingleMoldDiscOutput.Add(singleMoldDisc);
                    }
                }

                this.SingleMoldDiscOutput.Add(singleMoldDisc);
            }
        }

        public void CombineSingleCupMold()
        {
            this.SingleMoldCupOutput = new List<SingleMoldCup>();
        }
    }
}