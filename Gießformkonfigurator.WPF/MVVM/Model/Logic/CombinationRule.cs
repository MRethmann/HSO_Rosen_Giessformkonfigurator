//-----------------------------------------------------------------------
// <copyright file="CombinationRule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    /// <summary>
    /// All CombinationRules that are placed within the CombinationRuleSet.
    /// </summary>
    public abstract class CombinationRule
    {
        public CombinationSettings CombinationSettings { get; set; } = new CombinationSettings();

        protected abstract IEnumerable<Type> Typen { get; }

        /// <summary>
        /// Checks if the parameters are subclasses of Component class.
        /// </summary>
        /// <param name="teilTyp1">Component 1.</param>
        /// <param name="teilTyp2">Component 2.</param>
        /// <returns>True, if both parameters are valid objects to combine.</returns>
        public virtual bool Akzeptiert(Type teilTyp1, Type teilTyp2)
        {
            if (!teilTyp1.IsSubclassOf(typeof(Component)) || !teilTyp2.IsSubclassOf(typeof(Component)))
            {
                return false;
            }

            return this.Typen.ElementAt(0) == teilTyp1
                && this.Typen.ElementAt(1) == teilTyp2;
        }

        /// <summary>
        /// Combines to components based on their combinationRule.
        /// </summary>
        /// <param name="a">Component 1.</param>
        /// <param name="b">Component 2.</param>
        /// <returns>True, if the combination works.</returns>
        public abstract bool Combine(Component a, Component b);
    }

    public class BaseplateCoreCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(Core) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var core = components.OfType<Core>().Single();

            if (baseplate.HasKonus && core.HasKonus)
            {
                return baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && baseplate.InnerKonusAngle == core.OuterKonusAngle;

                // TODO: Low Priority - Höhe des Konus wird aktuell nicht abgefragt und muss ergänzt werden. Hier müsste ggf. ein neuer Toleranzwert gewählt werden.
            }

            // Grundplatte mit Lochführung akzeptiert einen Kern mit Fuehrungsstift
            else if (baseplate.HasHoleguide && core.HasGuideBolt)
            {
                return baseplate.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MIN >= core.GuideDiameter
                    && baseplate.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MAX <= core.GuideDiameter;

                // TODO: High Priority -  Höhe des GuideBolt muss ergänzt werden (Baseplate, Insertplate). Die Ausgabe der Gießformen leidet allerdings darunter. Daten prüfen.
                // && baseplate.Height > core.GuideHeight;
            }
            else
            {
                return false;
            }
        }
    }

    public class BaseplateInsertPlateCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(InsertPlate) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var insertPlate = components.OfType<InsertPlate>().Single();

            // Insertplate has no outer konus and therefore will be placed loose inside the baseplate. OuterKonus is used as workaround to show if insertPlate has OuterKonus.
            if (baseplate.HasHoleguide && (insertPlate.OuterKonusMax == 0 || insertPlate.OuterKonusMax == null))
            {
                // TODO: Low Priority -  Einlegeplatten können "lose" in die Grundplatte gelegt werden. Ausnahmefall der ggf. ergänzt werden kann.
                /*return baseplate.InnerDiameter > insertPlate.OuterDiameter
                    && (baseplate.InnerDiameter - 1) <= insertPlate.OuterDiameter;*/
                return false;
            }

            // Insertplate has outer konus which needs to match inner konus of baseplate (more likely)
            else if (baseplate.HasKonus)
            {
                return baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= insertPlate.OuterKonusMax
                    && baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= insertPlate.OuterKonusMax
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= insertPlate.OuterKonusMin
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= insertPlate.OuterKonusMin
                    && baseplate.InnerKonusAngle == insertPlate.OuterKonusAngle;
            }
            else
            {
                return false;
            }
        }
    }

    public class BaseplateRingCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Baseplate), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseplate = components.OfType<Baseplate>().Single();
            var ring = components.OfType<Ring>().Single();

            if (ring.HasKonus && baseplate.HasOuterEdge == false)
            {
                return ring.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= baseplate.OuterKonusMax
                    && ring.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= baseplate.OuterKonusMax
                    && ring.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= baseplate.OuterKonusMin
                    && ring.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= baseplate.OuterKonusMin
                    && ring.InnerKonusAngle == baseplate.OuterKonusAngle;
            }
            else if (ring.HasKonus == false && baseplate.HasOuterEdge)
            {
                // TODO: High Priority - Test baseplates with OuterEdge
                return baseplate.OuterKonusAngle == 90
                    && baseplate.OuterKonusMax - this.CombinationSettings.Tolerance_Flat_MIN >= ring.OuterDiameter
                    && baseplate.OuterKonusMax - this.CombinationSettings.Tolerance_Flat_MAX <= ring.OuterDiameter;
            }
            else
            {
                return false;
            }

        }
    }

    public class InsertPlateCoreCombination : CombinationRule
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
                    && insertPlate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && insertPlate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && insertPlate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && insertPlate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && insertPlate.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (insertPlate.HasHoleguide && core.HasGuideBolt)
            {
                return insertPlate.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MIN >= core.GuideDiameter
                    && insertPlate.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MAX <= core.GuideDiameter;
                    //&& insertPlate.Height >= core.GuideHeight;
            }
            else
            {
                return false;
            }
        }
    }

    public class RingAddition : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Ring), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var baseRing = components.OfType<Ring>().ElementAt(0);
            var additionRing = components.OfType<Ring>().ElementAt(1);

            return (additionRing.OuterDiameter <= baseRing.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MIN
                && additionRing.OuterDiameter >= baseRing.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MAX)
                || (additionRing.InnerDiameter >= baseRing.OuterDiameter + this.CombinationSettings.Tolerance_Flat_MIN
                && additionRing.InnerDiameter <= baseRing.OuterDiameter + this.CombinationSettings.Tolerance_Flat_MAX);
        }
    }

    public class CoreRingAddition : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Core), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var core = (Core) a;
            var additionRing = (Ring) b;

            return additionRing.InnerDiameter >= core.OuterDiameter + this.CombinationSettings.Tolerance_Flat_MIN
                && additionRing.InnerDiameter <= core.OuterDiameter + this.CombinationSettings.Tolerance_Flat_MAX;
        }
    }

    public class CupformCoreCombination : CombinationRule
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
                    && cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && cupform.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (cupform.HasHoleguide && core.HasGuideBolt)
            {
                // Checking for Height is not relevant for cupforms.
                return cupform.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MIN >= core.GuideDiameter
                    && cupform.InnerDiameter - this.CombinationSettings.Tolerance_Flat_MAX <= core.GuideDiameter;
            }
            else if (cupform.HasGuideBolt && core.HasHoleguide)
            {
                return core.AdapterDiameter - this.CombinationSettings.Tolerance_Flat_MIN >= cupform.InnerDiameter
                    && core.AdapterDiameter - this.CombinationSettings.Tolerance_Flat_MAX <= cupform.InnerDiameter;
            }
            else if (cupform.HasThread)
            {
                // TODO: Low Priority - Kerne mit Gewinde zur Cupform Kombination hinzufügen. Bisher noch keine Datensätze dazu.
                return false;
            }
            else
            {
                return false;
            }
        }
    }

    public class CupformInsertPlateCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Cupform), typeof(InsertPlate) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var cupform = components.OfType<Cupform>().Single();
            var insertPlate = components.OfType<InsertPlate>().Single();

            if (cupform.HasKonus)
            {
                // Konus Height missing in database.
                return cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= insertPlate.OuterKonusMax
                    && cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= insertPlate.OuterKonusMax
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= insertPlate.OuterKonusMin
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= insertPlate.OuterKonusMin
                    && cupform.InnerKonusAngle == insertPlate.OuterKonusAngle;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Used to combine a Cupform with a fixed core and a core Ring.
    /// </summary>
    public class CupformRingCombination : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Cupform), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var cupform = components.OfType<Cupform>().Single();
            var additionRing = components.OfType<Ring>().Single();

            return additionRing.InnerDiameter >= cupform.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MIN
                && additionRing.InnerDiameter <= cupform.InnerDiameter + this.CombinationSettings.Tolerance_Flat_MAX;
        }
    }
}