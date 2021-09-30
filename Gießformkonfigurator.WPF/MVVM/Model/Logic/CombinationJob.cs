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
    /// Objekt zur Kombination der Komponenten zur Erstellung der mehrteiligen Gießformen (MGießformen).
    /// </summary>
    class CombinationJob
    {
        // Frage: Wie kann man das universell umsetzen?
        public ProductDisc produktDisc { get; set; }
        public ProductCup produktCup { get; set; }
        public List<Baseplate> listBaseplates { get; set; } = new List<Baseplate>();
        public List<Ring> listRings { get; set; } = new List<Ring>();
        public List<InsertPlate> listInsertPlates { get; set; } = new List<InsertPlate>();
        public List<Core> listCores { get; set; } = new List<Core>();
        public List<Bolt> listBolts { get; set; } = new List<Bolt>();
        public List<Cupform> listCupforms { get; set; } = new List<Cupform>();
        public List<ModularMold> modularMoldsOutput { get; set; }
        public List<SingleMoldDisc> singleMoldDiscOutput { get; set; }
        public List<SingleMoldCup> singleMoldCupOutput { get; set; }
        public List<SingleMoldDisc> listSingleMoldDiscs { get; set; }
        public List<SingleMoldCup> listSingleMoldCups { get; set; }
        public List<CoreSingleMold> listCoresSingleMold { get; set; }
        public CombinationRuleset combinationRuleSet { get; set; }
        public ApplicationSettings applicationSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationJob"/> class.
        /// </summary>
        /// <param name="produktParam">Das ausgewählte Produkt wird übergeben.
        /// <param name="sapnr">PK in der DB.</param>
        public CombinationJob(Product product, FilterJob filterJob)
        {
            this.produktDisc = (ProductDisc) product;

            listBaseplates = new List<Baseplate>(filterJob.listBaseplates);
            listRings = new List<Ring>(filterJob.listRings);
            listInsertPlates = new List<InsertPlate>(filterJob.listInsertPlates);
            listCores = new List<Core>(filterJob.listCores);
            listBolts = new List<Bolt>(filterJob.listBolts);
            listCupforms = new List<Cupform>(filterJob.listCupforms);
            listCoresSingleMold = new List<CoreSingleMold>(filterJob.listCoresSingleMold);
            listSingleMoldCups = new List<SingleMoldCup>(filterJob.listSingleMoldCups);
            listSingleMoldDiscs = new List<SingleMoldDisc>(filterJob.listSingleMoldDiscs);

            combinationRuleSet = new CombinationRuleset();
            applicationSettings = new ApplicationSettings();

            this.CombineModularDiscMold();
            this.CombineModularCupMold();
            this.CombineSingleDiscMold();
            this.CombineSingleCupMold();
        }

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
            // Listen, welche zur Zwischenspeicherung der mehrteiligen Gießformen genutzt werden, bevor sie vervollständigt wurden und ausgegeben werden können.
            List<ModularMold> discMoldsTemp01 = new List<ModularMold>();
            List<ModularMold> discMoldsTemp02 = new List<ModularMold>();

            // Grundplatten --> Fuehrungsringe
            for (int iGP = 0; iGP < this.listBaseplates.Count; iGP++)
            {
                for (int iRinge = 0; iRinge < this.listRings.Count; iRinge++)
                {
                    if (combinationRuleSet.Combine(this.listBaseplates[iGP], this.listRings[iRinge]))
                    {
                        discMoldsTemp01.Add(new ModularMold(this.listBaseplates[iGP], this.listRings[iRinge], null, null));
                    }
                }
            }

            // Mehrteilige Gießformen --> Einlegeplatten
            this.Index = discMoldsTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                if (discMoldsTemp01[iTemp].baseplate.HasKonus)
                {
                    for (int iEP = 0; iEP < this.listInsertPlates.Count; iEP++)
                    {
                        if (combinationRuleSet.Combine(discMoldsTemp01[iTemp].baseplate, this.listInsertPlates[iEP]))
                        {
                            discMoldsTemp01.Add(new ModularMold(discMoldsTemp01[iTemp].baseplate, discMoldsTemp01[iTemp].guideRing, this.listInsertPlates[iEP], null));
                        }
                    }
                }
            }

            // Mehrteilige Gießformen --> Kerne
            this.Index = discMoldsTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                for (int iKerne = 0; iKerne < this.listCores.Count; iKerne++)
                {
                    // Einlegeplatte vorhanden
                    if (discMoldsTemp01[iTemp].insertPlate != null)
                    {
                        // Einlegeplatten mit Konusfuehrung und Lochfuehrung
                        if ((discMoldsTemp01[iTemp].insertPlate.HasKonus || discMoldsTemp01[iTemp].insertPlate.HasHoleguide) && combinationRuleSet.Combine(discMoldsTemp01[iTemp].insertPlate, this.listCores[iKerne]))
                        {
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].baseplate, discMoldsTemp01[iTemp].guideRing, discMoldsTemp01[iTemp].insertPlate, this.listCores[iKerne]));
                        }

                        // Einlegeplatte mit Kern
                        else if (discMoldsTemp01[iTemp].insertPlate.HasCore == true)
                        {
                            // TODO: Hier am besten einen "virtuelle" Kern erstellen, der die Maße der Einlegeplatte bekommt.
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].baseplate, discMoldsTemp01[iTemp].guideRing, discMoldsTemp01[iTemp].insertPlate, null));
                        }
                    }

                    // Ohne Einlegeplatte
                    else if (discMoldsTemp01[iTemp].insertPlate == null)
                    {
                        // Grundplatten mit Konusfuehrung und Lochfuehrung
                        if ((discMoldsTemp01[iTemp].baseplate.HasKonus == true || discMoldsTemp01[iTemp].baseplate.HasHoleguide == true) && combinationRuleSet.Combine(discMoldsTemp01[iTemp].baseplate, this.listCores[iKerne]))
                        {
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].baseplate, discMoldsTemp01[iTemp].guideRing, null, this.listCores[iKerne]));
                        }

                        // Grundplatte mit Kern
                        else if (discMoldsTemp01[iTemp].baseplate.HasCore == true)
                        {
                            // TODO: Hier am besten einen "virtuelle" Kern erstellen, der die Maße der Grundplatte bekommt.
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].baseplate, discMoldsTemp01[iTemp].guideRing, null, null));
                        }
                    }
                }
            }

            // Mehrteilige Gießformen --> Innenringe
            this.Index = discMoldsTemp02.Count;

            foreach (var mold in discMoldsTemp02)
            {
                foreach (var ring in listRings)
                {
                    // Alle Ringe entfernen, die potentiell außerhalb des Guide Rings liegen könnten
                    if (ring.OuterDiameter < mold.guideRing.InnerDiameter + 1)
                    {
                        if (combinationRuleSet.Combine(mold.guideRing, ring))
                        {
                            var differenceOuterDiameter = ring.InnerDiameter - produktDisc.OuterDiameter;
                            mold.ListOuterRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceOuterDiameter));
                        }
                        else if (combinationRuleSet.Combine(mold.core, ring))
                        {
                            var differenceInnerDiameter = produktDisc.InnerDiameter - ring.OuterDiameter;
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
                    foreach (var ring in listRings)
                    {
                        if (ring.InnerDiameter + 1 > mold.core.OuterDiameter)
                        {
                            if (ring.OuterDiameter > mold.core.OuterDiameter)
                            {
                                if (combinationRuleSet.Combine(mold.ListCoreRings[i].Item1, ring))
                                {
                                    var differenceInnerDiameter = produktDisc.InnerDiameter - ring.OuterDiameter;
                                    mold.ListCoreRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListCoreRings[i].Item1, ring, differenceInnerDiameter));
                                }
                            }
                        }
                    }
                }

                var counter02 = mold.ListOuterRings.Count;
                for (int i = 0; i < counter02; i++)
                {
                    foreach (var ring in listRings)
                    {
                        if (ring.OuterDiameter < mold.guideRing.InnerDiameter + 1)
                        {
                            if (ring.InnerDiameter < mold.guideRing.OuterDiameter)
                            {
                                if (combinationRuleSet.Combine(mold.ListOuterRings[i].Item1, ring))
                                {
                                    var differenceOuterDiameter = ring.InnerDiameter - produktDisc.OuterDiameter;
                                    mold.ListOuterRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListOuterRings[i].Item1, ring, differenceOuterDiameter));
                                }
                            }
                        }
                    }
                }
            }

            foreach (var mold in discMoldsTemp02)
            {
                mold.ListCoreRings.OrderBy(o => o.Item3);
                mold.ListOuterRings.OrderBy(o => o.Item3);
            }

            this.modularMoldsOutput = new List<ModularMold>(discMoldsTemp02);
        }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine components to create modular molds for cup products.
        /// </summary>
        public void CombineModularCupMold()
        {
            List<ModularMold> cupMoldsTemp01 = new List<ModularMold>();

            // Combine cupform with insertPlates
            foreach (var cupform in this.listCupforms)
            {
                foreach (var insertPlate in this.listInsertPlates)
                {
                    if (combinationRuleSet.Combine(cupform, insertPlate))
                    {
                        cupMoldsTemp01.Add(new ModularMold(cupform, null, insertPlate));
                    }
                }
            }

            // Combine cupform with cores
            foreach (var modularMold in cupMoldsTemp01)
            {
                foreach (var core in this.listCores)
                {
                    // Case that cupform has an insertPlate --> compare insertPlate with core
                    if (modularMold.insertPlate != null)
                    {
                        if (combinationRuleSet.Combine(modularMold.insertPlate, core))
                        {
                            this.modularMoldsOutput.Add(new ModularMold(modularMold.cupform, core, modularMold.insertPlate));
                        }
                    }

                    // Case that cupform has no insertPlate --> compare cupform directly with core
                    else if (modularMold.insertPlate == null)
                    {
                        if (combinationRuleSet.Combine(modularMold.cupform, core))
                        {
                            this.modularMoldsOutput.Add(new ModularMold(modularMold.cupform, core, modularMold.insertPlate));
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
            singleMoldDiscOutput = new List<SingleMoldDisc>();

            foreach (var singleMoldDisc in this.listSingleMoldDiscs)
            {
                foreach (var coreSingleMold in this.listCoresSingleMold)
                {
                    if (singleMoldDisc.InnerDiameter <= coreSingleMold.InnerDiameter
                        && singleMoldDisc.InnerDiameter <= coreSingleMold.InnerDiameter - 2
                        && coreSingleMold.OuterDiameter < (singleMoldDisc.HcDiameter - singleMoldDisc.BoltDiameter/2))
                    {
                        singleMoldDisc.coreSingleMold = coreSingleMold;
                        singleMoldDiscOutput.Add(singleMoldDisc);
                    }
                }
                singleMoldDiscOutput.Add(singleMoldDisc);
            }
        }

        public void CombineSingleCupMold()
        {
            singleMoldCupOutput = new List<SingleMoldCup>();
        }

    }
}