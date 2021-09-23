//-----------------------------------------------------------------------
// <copyright file="CombinationRule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    abstract class CombinationRule
    {
        public ApplicationSettings applicationSettings { get; set; }

        public CombinationRule()
        {
            applicationSettings = new ApplicationSettings();
        }

        protected abstract IEnumerable<Type> Typen { get; }

        public virtual bool Akzeptiert(Type teilTyp1, Type teilTyp2)
        {
            if (!teilTyp1.IsSubclassOf(typeof(Component)) || !teilTyp2.IsSubclassOf(typeof(Component)))
            {
                return false;
            }

            return this.Typen.ElementAt(0) == teilTyp1
                && this.Typen.ElementAt(1) == teilTyp2;
        }

        public abstract bool Combine(Component a, Component b);
    }

    /// <summary>
    /// No Documentation necessary.
    /// </summary>
    class BaseplateCoreCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(Core) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var core = components.OfType<Core>().Single();

            if (baseplate.HasKonus && core.HasKonus)
            {
                // TODO: Change area of matching to variable which can be changed in application settings
                return baseplate.InnerKonusMax > core.OuterKonusMax
                        && (baseplate.InnerKonusMax - 2) <= core.OuterKonusMax
                        && baseplate.InnerKonusMin > core.OuterKonusMin
                        && (baseplate.InnerKonusMin - 2) <= core.OuterKonusMin
                        && baseplate.InnerKonusAngle == core.OuterKonusAngle;
            }

            // Grundplatte mit Lochführung akzeptiert einen Kern mit Fuehrungsstift
            else if (baseplate.HasHoleguide && core.HasGuideBolt)
            {
                // TODO: Genaue Abweichung zwischen Innendurchmesser und Fuehrungsdurchmesser festlegen.
                return baseplate.InnerDiameter >= core.GuideDiameter
                    && (baseplate.InnerDiameter - 2) <= core.GuideDiameter
                    && baseplate.Height >= core.GuideHeight;
            }

            // TODO: Abklären, ob dieser Fall zustande kommen könnte.
            else if (baseplate.HasHoleguide && core.HasHoleguide)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }

    class BaseplateInsertPlateCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(InsertPlate) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var insertPlate = components.OfType<InsertPlate>().Single();

            // Insertplate has no outer konus and therefore will be placed loose inside the baseplate
            if (baseplate.HasHoleguide && insertPlate.OuterKonusMax == 0)
            {
                return baseplate.InnerDiameter > insertPlate.OuterDiameter
                    && (baseplate.InnerDiameter - 1) <= insertPlate.OuterDiameter
                    && baseplate.Height == insertPlate.Height;
            }

            // Insertplate has outer konus which needs to match inner konus of baseplate (more likely)
            else if (baseplate.HasKonus && insertPlate.OuterDiameter != 0)
            {
                return baseplate.InnerKonusMax > insertPlate.OuterKonusMax
                    && (baseplate.InnerKonusMax - 1) <= insertPlate.OuterKonusMax
                    && baseplate.InnerKonusMin > insertPlate.OuterKonusMin
                    && (baseplate.InnerKonusMin - 1) <= insertPlate.OuterKonusMin
                    && baseplate.InnerKonusAngle == insertPlate.OuterKonusAngle;
            }

            else
            {
                return false;
            }
        }
    }

    class BaseplateRingCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var ring = components.OfType<Ring>().Single();

            return ring.HasKonus
                     && ring.InnerKonusMin > baseplate.OuterKonusMin
                     && ring.InnerKonusMin < baseplate.OuterKonusMax
                     && ring.InnerKonusAngle == baseplate.OuterKonusAngle
                     && ring.KonusHeight < baseplate.KonusHeight
                     && ring.InnerKonusMax < baseplate.OuterKonusMax;
        }
    }

    class InsertPlateCoreCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(InsertPlate), typeof(Core) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var insertPlate = components.OfType<InsertPlate>().Single();
            var core = components.OfType<Core>().Single();

            if (insertPlate.HasKonus)
            {
                return core.HasKonus == true
                            && insertPlate.InnerKonusMax > core.OuterKonusMax
                            && (insertPlate.InnerKonusMax - 1) <= core.OuterKonusMax
                            && insertPlate.InnerKonusMin > core.OuterKonusMin
                            && (insertPlate.InnerKonusMin - 1) <= core.OuterKonusMin
                            && insertPlate.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (insertPlate.HasHoleguide)
            {
                return core.HasGuideBolt == true
                    && insertPlate.InnerDiameter == core.GuideDiameter
                    && insertPlate.Height >= core.GuideHeight;
            }
            else
            {
                return false;
            }
        }
    }

    // TODO: Fertigstellen
    class RingAddition : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Ring), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseRing = components.OfType<Ring>().ElementAt(0);
            var additionRing = components.OfType<Ring>().ElementAt(1);

            // Der Innenring darf nicht zu groß und nicht zu klein sein.
            return (additionRing.OuterDiameter <= baseRing.InnerDiameter - 0.1m
                && additionRing.OuterDiameter >= baseRing.InnerDiameter - 2m)
                || (additionRing.InnerDiameter >= baseRing.OuterDiameter
                && additionRing.InnerDiameter - 2 < baseRing.OuterDiameter);
        }
    }

    class CoreRingAddition : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Core), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var core = (Core) a;
            var additionRing = (Ring) b;

            // Der Innenring darf nicht zu groß und nicht zu klein sein.
            return additionRing.InnerDiameter >= core.OuterDiameter
                && additionRing.InnerDiameter - 2 < core.OuterDiameter;
        }
    }

    class CupformCoreCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Cupform), typeof(Core) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var cupform = components.OfType<Cupform>().Single();
            var core = components.OfType<Core>().Single();

            if (cupform.HasKonus)
            {
                return core.HasKonus == true
                            && cupform.InnerKonusMax > core.OuterKonusMax
                            && (cupform.InnerKonusMax - 1) <= core.OuterKonusMax
                            && cupform.InnerKonusMin > core.OuterKonusMin
                            && (cupform.InnerKonusMin - 1) <= core.OuterKonusMin
                            && cupform.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (cupform.HasHoleguide)
            {
                return core.HasGuideBolt == true
                    && cupform.InnerDiameter == core.GuideDiameter;
            }
            else if (cupform.HasCore)
            {
                return false;
            }
            else if (cupform.HasGuideBolt)
            {
                return core.HasHoleguide == true
                    && core.OuterDiameter == cupform.InnerDiameter;
            }
            else if (cupform.HasThread)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}