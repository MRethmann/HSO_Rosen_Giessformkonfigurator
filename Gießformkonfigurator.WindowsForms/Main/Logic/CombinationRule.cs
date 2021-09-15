namespace Gießformkonfigurator.WindowsForms.Main.Logik
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WindowsForms.Main.Db_components;

    abstract class CombinationRule
    {
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
            if (baseplate.Mit_Konusfuehrung)
            {
                return core.Mit_Konusfuehrung == true
                        && baseplate.Konus_Innen_Max > core.Konus_Außen_Max
                        && (baseplate.Konus_Innen_Max - 1) <= core.Konus_Außen_Max
                        && baseplate.Konus_Innen_Min > core.Konus_Außen_Min
                        && (baseplate.Konus_Innen_Min - 1) <= core.Konus_Außen_Min
                        && baseplate.Konus_Innen_Winkel == core.Konus_Außen_Winkel;
                // && (this.Konus_Innen_Winkel - 5) < kern.Konus_Außen_Winkel;
            }

            // Grundplatte mit Lochführung akzeptiert einen Kern mit Fuehrungsstift oder Lochfuehrung
            else if (baseplate.Mit_Lochfuehrung && core.Mit_Fuehrungsstift)
            {
                // TODO: Genaue Abweichung zwischen Innendurchmesser und Fuehrungsdurchmesser festlegen.
                return baseplate.Innendurchmesser >= core.Durchmesser_Fuehrung
                    && (baseplate.Innendurchmesser - 2) <= core.Durchmesser_Fuehrung
                    && baseplate.Hoehe >= core.Hoehe_Fuehrung;
            }
            else if (baseplate.Mit_Lochfuehrung && core.Mit_Lochfuehrung)
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

            if (baseplate.Mit_Lochfuehrung && insertPlate.Konus_Außen_Max == 0)
            {
                return baseplate.Innendurchmesser > insertPlate.Außendurchmesser
                    && (baseplate.Innendurchmesser - 1) <= insertPlate.Außendurchmesser
                    && baseplate.Hoehe == insertPlate.Hoehe;
            }
            else if (baseplate.Mit_Konusfuehrung && insertPlate.Außendurchmesser != 0)
            {
                return baseplate.Konus_Innen_Max > insertPlate.Konus_Außen_Max
                    && (baseplate.Konus_Innen_Max - 1) <= insertPlate.Konus_Außen_Max
                    && baseplate.Konus_Innen_Min > insertPlate.Konus_Außen_Min
                    && (baseplate.Konus_Innen_Min - 1) <= insertPlate.Konus_Außen_Min
                    && baseplate.Konus_Innen_Winkel == insertPlate.Konus_Außen_Winkel;
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

            return ring.Konus_Min > baseplate.Konus_Außen_Min
                     && ring.Konus_Min < baseplate.Konus_Außen_Max
                     && ring.Konus_Winkel == baseplate.Konus_Außen_Winkel
                     && ring.Konus_Hoehe < baseplate.Konus_Hoehe
                     && ring.Konus_Max < baseplate.Konus_Außen_Max;
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

            if (insertPlate.Mit_Konusfuehrung)
            {
                return core.Mit_Konusfuehrung == true
                            && insertPlate.Konus_Innen_Max > core.Konus_Außen_Max
                            && (insertPlate.Konus_Innen_Max - 1) <= core.Konus_Außen_Max
                            && insertPlate.Konus_Innen_Min > core.Konus_Außen_Min
                            && (insertPlate.Konus_Innen_Min - 1) <= core.Konus_Außen_Min
                            && insertPlate.Konus_Innen_Winkel == core.Konus_Außen_Winkel;
            }
            else if (insertPlate.Mit_Lochfuehrung)
            {
                return core.Mit_Fuehrungsstift == true
                    && insertPlate.Innendurchmesser == core.Durchmesser_Fuehrung
                    && insertPlate.Hoehe >= core.Hoehe_Fuehrung;
            }
            else
            {
                return false;
            }
        }
    }

    // TODO: Fertigstellen
    class InnerRingAddition : CombinationRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(Ring), typeof(Ring) };

        public override bool Combine(Component a, Component b)
        {
            var components = new[] { a, b };
            var fuehrungsring = components.OfType<Ring>().ElementAt(0);
            var innerRing = components.OfType<Ring>().ElementAt(1);

            // Der Innenring darf nicht zu groß und nicht zu klein sein.
            return innerRing.Außendurchmesser <= fuehrungsring.Innendurchmesser - 0.1m
                && innerRing.Außendurchmesser >= fuehrungsring.Innendurchmesser - 0.5m;
        }
    }
}