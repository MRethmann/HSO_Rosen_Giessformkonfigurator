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

            // Normal Konus which has a degree somwhere below 90. Seperate comparison because different tolerances are used.
            // KonusHeight is intentionally not used. Discussed and decided with Andreas Schmidt.
            if (baseplate.HasKonus && core.HasKonus && baseplate.InnerKonusAngle != 90)
            {
                return baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && baseplate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && baseplate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && baseplate.InnerKonusAngle == core.OuterKonusAngle;
            }

            // 90 Degree Konus which was formerly being handled by the GuideBolt attribute. Different logic than normal konus because different tolerances are used.
            else if (baseplate.HasKonus && core.HasKonus && baseplate.InnerKonusAngle == 90)
            {
                return baseplate.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MIN >= core.OuterKonusMax
                    && baseplate.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MAX <= core.OuterKonusMax
                    && baseplate.InnerKonusAngle == core.OuterKonusAngle;
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

            // Insertplate has outer konus which needs to match inner konus of baseplate (more likely)
            if (baseplate.HasKonus)
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
                return baseplate.OuterKonusAngle == 90
                    && baseplate.OuterKonusMin - this.CombinationSettings.Tolerance_Flat_MIN >= ring.OuterDiameter
                    && baseplate.OuterKonusMin - this.CombinationSettings.Tolerance_Flat_MAX <= ring.OuterDiameter;
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

            if (insertPlate.HasKonus && core.HasKonus && insertPlate.InnerKonusAngle != 90)
            {
                return core.HasKonus == true
                    && insertPlate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && insertPlate.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && insertPlate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && insertPlate.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && insertPlate.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (insertPlate.HasKonus && core.HasKonus && insertPlate.InnerKonusAngle == 90)
            {
                return insertPlate.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MIN >= core.OuterKonusMax
                    && insertPlate.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MAX <= core.OuterKonusMax
                    && insertPlate.InnerKonusAngle == core.OuterKonusAngle;
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

            if (cupform.HasKonus && core.HasKonus && cupform.InnerKonusAngle != 90)
            {
                return cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMax
                    && cupform.InnerKonusMax + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMax
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MAX >= core.OuterKonusMin
                    && cupform.InnerKonusMin + this.CombinationSettings.Tolerance_Konus_MIN <= core.OuterKonusMin
                    && cupform.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (cupform.HasKonus && core.HasKonus && cupform.InnerKonusAngle == 90)
            {
                // Checking for Height is not relevant for cupforms.
                return cupform.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MIN >= core.OuterKonusMax
                    && cupform.InnerKonusMax - this.CombinationSettings.Tolerance_Flat_MAX <= core.OuterKonusMax
                    && cupform.InnerKonusAngle == core.OuterKonusAngle;
            }
            else if (cupform.HasGuideBolt && core.HasHoleguide)
            {
                return core.AdapterDiameter - this.CombinationSettings.Tolerance_Flat_MIN >= cupform.InnerDiameter
                    && core.AdapterDiameter - this.CombinationSettings.Tolerance_Flat_MAX <= cupform.InnerDiameter;
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