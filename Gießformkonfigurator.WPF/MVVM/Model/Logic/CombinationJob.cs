//-----------------------------------------------------------------------
// <copyright file="CombinationJob.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
#pragma warning disable SA1519 // Braces should not be omitted from multi-line child statement
#pragma warning disable SA1623 // Property summary documentation should match accessors
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;
    using log4net;

    /// <summary>
    /// Combines modular components to create multi molds.
    /// </summary>
    public class CombinationJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CombinationJob));

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationJob"/> class.
        /// </summary>
        /// <param name="productDisc">Expects Disc Product.</param>
        /// <param name="filterJob">Uses output of filterjob.</param>
        public CombinationJob(ProductDisc productDisc, FilterJob filterJob)
        {
            this.ProduktDisc = productDisc;

            this.ListBaseplates = new List<Baseplate>(filterJob.ListBaseplates);
            this.ListRings = new List<Ring>(filterJob.ListRings);
            this.ListInsertPlates = new List<InsertPlate>(filterJob.ListInsertPlates);
            this.ListCores = new List<Core>(filterJob.ListCores);
            this.ListBolts = new List<Bolt>(filterJob.ListBolts);
            this.ListCoresSingleMold = new List<CoreSingleMold>(filterJob.ListCoresSingleMold);
            this.ListSingleMoldDiscs = new List<SingleMoldDisc>(filterJob.ListSingleMoldDiscs);

            this.CombineModularDiscMold();
            this.CombineSingleDiscMold();

            Log.Info("CombinationJobOutput: " + (this.SingleMoldDiscOutput.Count + this.ModularMoldDiscOutput.Count).ToString());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationJob"/> class.
        /// </summary>
        /// <param name="productCup">Expects Cup Product.</param>
        /// <param name="filterJob">Uses output of filterjob.</param>
        public CombinationJob(ProductCup productCup, FilterJob filterJob)
        {
            this.ProduktCup = productCup;

            this.ListRings = new List<Ring>(filterJob.ListRings);
            this.ListInsertPlates = new List<InsertPlate>(filterJob.ListInsertPlates);
            this.ListCores = new List<Core>(filterJob.ListCores);
            this.ListBolts = new List<Bolt>(filterJob.ListBolts);
            this.ListCupforms = new List<Cupform>(filterJob.ListCupforms);
            this.ListSingleMoldCups = new List<SingleMoldCup>(filterJob.ListSingleMoldCups);
            this.ListCoresSingleMold = new List<CoreSingleMold>(filterJob.ListCoresSingleMold);

            this.CombineModularCupMold();
            this.CombineSingleCupMold();

            Log.Info("CombinationJobOutput: " + (this.SingleMoldCupOutput.Count + this.ModularMoldCupOutput.Count).ToString());
        }

        public List<Baseplate> ListBaseplates { get; set; }

        public List<Ring> ListRings { get; set; }

        public List<InsertPlate> ListInsertPlates { get; set; }

        public List<Core> ListCores { get; set; }

        public List<Bolt> ListBolts { get; set; }

        public List<Cupform> ListCupforms { get; set; }

        /// <summary>
        /// Gets or Sets the list of single mold disc received from the filterJob.
        /// </summary>
        public List<SingleMoldDisc> ListSingleMoldDiscs { get; set; }

        public List<SingleMoldCup> ListSingleMoldCups { get; set; }

        public List<CoreSingleMold> ListCoresSingleMold { get; set; }

        /// <summary>
        /// Gets or Sets the final output of the modular mold combination algorithm.
        /// </summary>
        public List<ModularMold> ModularMoldDiscOutput { get; set; }

        public List<ModularMold> ModularMoldCupOutput { get; set; }

        /// <summary>
        /// Gets or Sets the final output of the single mold combination algorithm.
        /// </summary>
        public List<SingleMoldDisc> SingleMoldDiscOutput { get; set; }

        public List<SingleMoldCup> SingleMoldCupOutput { get; set; }

        private ProductDisc ProduktDisc { get; set; }

        private ProductCup ProduktCup { get; set; }

        private CombinationSettings CombinationSettings { get; set; } = new CombinationSettings();

        /// <summary>
        /// Gets of Sets the ruleset that is used to combine the components.
        /// </summary>
        private CombinationRuleset CombinationRuleSet { get; set; } = new CombinationRuleset();

        /// <summary>
        /// Speichert den aktuellen Listenindex.
        /// </summary>
        private int Index { get; set; }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine components to create modular molds for disc products.
        /// </summary>
        [STAThread]
        private void CombineModularDiscMold()
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
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, discMoldsTemp01[iTemp].InsertPlate, null));
                        }
                    }

                    // Ohne Einlegeplatte
                    else if (discMoldsTemp01[iTemp].InsertPlate == null)
                    {
                        // Grundplatten mit Konusfuehrung und Lochfuehrung
                        if (discMoldsTemp01[iTemp].Baseplate.HasKonus == true && this.CombinationRuleSet.Combine(discMoldsTemp01[iTemp].Baseplate, this.ListCores[iKerne]))
                        {
                            discMoldsTemp02.Add(new ModularMold(discMoldsTemp01[iTemp].Baseplate, discMoldsTemp01[iTemp].GuideRing, null, this.ListCores[iKerne]));
                        }
                    }
                }
            }

            // MultiMolds (Baseplates, GuideRings, InsertPlates, Cores) --> OuterRings + CoreRings
            // TODO: Low Priority -  Es gibt noch keine Logik, um einen Zusatzring zu nutzen, wenn die Grundplatte/Einlegeplatte bereits einen Kern hat. Aktuell gibt es keine Komponenten, die diese Logik nutzen würden.
            this.Index = discMoldsTemp02.Count;

            foreach (var mold in discMoldsTemp02)
            {
                foreach (var ring in this.ListRings.Where(x => x.HasKonus == false))
                {
                    // Alle Ringe entfernen, die potentiell außerhalb des Guide Rings liegen könnten
                    if (ring.OuterDiameter < mold.GuideRing.InnerDiameter + 1)
                    {
                        if (this.CombinationRuleSet.Combine(mold.GuideRing, ring))
                        {
                            var differenceOuterDiameter = ring.InnerDiameter - this.ProduktDisc.ModularMoldDimensions.OuterDiameter;
                            mold.ListOuterRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceOuterDiameter));
                        }
                        else if (this.CombinationRuleSet.Combine(mold.Core, ring))
                        {
                            var differenceInnerDiameter = this.ProduktDisc.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter;
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
                    foreach (var ring in this.ListRings.Where(x => x.HasKonus == false))
                    {
                        if (ring.InnerDiameter + 1 > mold.Core.OuterDiameter)
                        {
                            if (ring.OuterDiameter > mold.Core.OuterDiameter)
                            {
                                if (this.CombinationRuleSet.Combine(mold.ListCoreRings[i].Item1, ring))
                                {
                                    var differenceInnerDiameter = this.ProduktDisc.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter;
                                    mold.ListCoreRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListCoreRings[i].Item1, ring, differenceInnerDiameter));
                                }
                            }
                        }
                    }
                }

                var counter02 = mold.ListOuterRings.Count;
                for (int i = 0; i < counter02; i++)
                {
                    foreach (var ring in this.ListRings.Where(x => x.HasKonus == false))
                    {
                        if (ring.OuterDiameter < mold.GuideRing.InnerDiameter + 1)
                        {
                            if (ring.InnerDiameter < mold.GuideRing.OuterDiameter)
                            {
                                if (this.CombinationRuleSet.Combine(mold.ListOuterRings[i].Item1, ring))
                                {
                                    var differenceOuterDiameter = ring.InnerDiameter - this.ProduktDisc.ModularMoldDimensions.OuterDiameter;
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
                var listCoreRings = mold.ListCoreRings.OrderBy(o => o.Item3);
                mold.ListCoreRings = new List<Tuple<Ring, Ring, decimal?>>(listCoreRings);
                var listOuterRings = mold.ListOuterRings.OrderBy(o => o.Item3);
                mold.ListOuterRings = new List<Tuple<Ring, Ring, decimal?>>(listOuterRings);
            }

            this.ModularMoldDiscOutput = new List<ModularMold>(discMoldsTemp02);

            foreach (var modularMold in this.ModularMoldDiscOutput)
            {
                Log.Info("Grundplatte: " + modularMold.Baseplate?.ID.ToString() + " + Einlegeplatte: " + modularMold.InsertPlate?.ID.ToString() + " + Führungsring: " + modularMold.GuideRing?.ID.ToString() + " + Kern: " + modularMold.Core?.ID.ToString());
            }
        }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine components to create modular molds for cup products.
        /// </summary>
        private void CombineModularCupMold()
        {
            List<ModularMold> cupMoldsTemp01 = new List<ModularMold>();
            List<ModularMold> cupMoldsTemp02 = new List<ModularMold>();

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

                cupMoldsTemp01.Add(new ModularMold(cupform, null, null));
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
                            cupMoldsTemp02.Add(new ModularMold(modularMold.Cupform, core, modularMold.InsertPlate));
                        }
                        else if (modularMold.InsertPlate.HasCore)
                        {
                            cupMoldsTemp02.Add(new ModularMold(modularMold.Cupform, null, modularMold.InsertPlate));
                        }
                    }

                    // Case that cupform has no insertPlate --> compare cupform directly with core
                    else if (modularMold.InsertPlate == null)
                    {
                        if (this.CombinationRuleSet.Combine(modularMold.Cupform, core))
                        {
                            cupMoldsTemp02.Add(new ModularMold(modularMold.Cupform, core, null));
                        }
                    }
                }

                // If cupform and core do not match, check if the cupform has a GuideBolt and add it anyways --> Guidebolt works as a core.
                if (modularMold.Cupform.HasGuideBolt)
                {
                    cupMoldsTemp02.Add(new ModularMold(modularMold.Cupform, null, null));
                }
            }

            foreach (var mold in cupMoldsTemp02)
            {
                foreach (var ring in this.ListRings.Where(x => x.HasKonus == false))
                {
                    // Case 1: Mold has modular Core.
                    if (mold.Core != null)
                    {
                        if (ring.InnerDiameter > mold.Core.OuterDiameter - 2)
                        {
                            if (this.CombinationRuleSet.Combine(mold.Core, ring))
                            {
                                var differenceInnerDiameter = this.ProduktCup.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter;
                                mold.ListCoreRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceInnerDiameter));
                            }
                        }
                    }

                    // Case 2: Cupform has fixed Core/GuideBolt.
                    else
                    {
                        if (mold.Cupform.HasGuideBolt
                        && ring.InnerDiameter > mold.Cupform.InnerDiameter - 2)
                        {
                            if (this.CombinationRuleSet.Combine(mold.Cupform, ring))
                            {
                                var differenceInnerDiameter = this.ProduktCup.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter;
                                mold.ListCoreRings.Add(new Tuple<Ring, Ring, decimal?>(ring, null, differenceInnerDiameter));
                            }
                        }
                    }
                }
            }

            foreach (var mold in cupMoldsTemp02)
            {
                var counter01 = mold.ListCoreRings.Count;
                for (int i = 0; i < counter01; i++)
                {
                    foreach (var ring in this.ListRings.Where(x => x.HasKonus == false))
                    {
                        if (ring.InnerDiameter + 1 > mold.Core.OuterDiameter)
                        {
                            if (ring.OuterDiameter > mold.Core.OuterDiameter)
                            {
                                if (this.CombinationRuleSet.Combine(mold.ListCoreRings[i].Item1, ring))
                                {
                                    var differenceInnerDiameter = this.ProduktCup.ModularMoldDimensions.InnerDiameter - ring.OuterDiameter;
                                    mold.ListCoreRings.Insert(0, new Tuple<Ring, Ring, decimal?>(mold.ListCoreRings[i].Item1, ring, differenceInnerDiameter));
                                }
                            }
                        }
                    }
                }
            }

            this.ModularMoldCupOutput = new List<ModularMold>(cupMoldsTemp02);

            foreach (var modularMold in this.ModularMoldCupOutput)
            {
                Log.Info("Cupform: " + modularMold.Cupform?.ID.ToString());
            }
        }

        /// <summary>
        /// Uses the pre filtered database entries (filterJob) to combine singleMoldDisc with optional Cores.
        /// </summary>
        private void CombineSingleDiscMold()
        {
            this.SingleMoldDiscOutput = new List<SingleMoldDisc>();

            foreach (var singleMoldDisc in this.ListSingleMoldDiscs)
            {
                foreach (var coreSingleMold in this.ListCoresSingleMold)
                {
                    if (coreSingleMold.InnerDiameter >= singleMoldDisc.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MIN
                    && coreSingleMold.InnerDiameter <= singleMoldDisc.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MAX)
                    {
                        if ((!string.IsNullOrWhiteSpace(singleMoldDisc.BTC) && coreSingleMold.OuterDiameter < (singleMoldDisc.HcDiameter - (singleMoldDisc.BoltDiameter / 2)))
                            || string.IsNullOrWhiteSpace(singleMoldDisc.BTC))
                        {
                            var newSingleMoldDisc = singleMoldDisc.Clone();
                            newSingleMoldDisc.CoreSingleMold = coreSingleMold;
                            this.SingleMoldDiscOutput.Add(newSingleMoldDisc);
                        }
                    }
                }

                this.SingleMoldDiscOutput.Add(singleMoldDisc);
            }

            foreach (var singleMold in this.SingleMoldDiscOutput)
            {
                Log.Info(singleMold.ID.ToString() + " + CoreSingleMold: " + singleMold.CoreSingleMold?.ID.ToString());
            }
        }

        private void CombineSingleCupMold()
        {
            this.SingleMoldCupOutput = new List<SingleMoldCup>();

            foreach (var singleMoldCup in this.ListSingleMoldCups)
            {
                foreach (var coreSingleMold in this.ListCoresSingleMold)
                {
                    if (coreSingleMold.InnerDiameter >= singleMoldCup.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MIN
                    && coreSingleMold.InnerDiameter <= singleMoldCup.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MAX)
                    {
                        if ((!string.IsNullOrWhiteSpace(singleMoldCup.BTC) && coreSingleMold.OuterDiameter < (singleMoldCup.HcDiameter - (singleMoldCup.BoltDiameter / 2)))
                            || string.IsNullOrWhiteSpace(singleMoldCup.BTC))
                        {
                            var newSingleMoldCup = singleMoldCup.Clone();
                            newSingleMoldCup.CoreSingleMold = coreSingleMold;
                            this.SingleMoldCupOutput.Add(newSingleMoldCup);
                        }
                    }
                }

                this.SingleMoldCupOutput.Add(singleMoldCup);
            }

            foreach (var singleMold in this.SingleMoldCupOutput)
            {
                Log.Info(singleMold.ID.ToString() + " + CoreSingleMold: " + singleMold.CoreSingleMold?.ID.ToString());
            }
        }
    }
}