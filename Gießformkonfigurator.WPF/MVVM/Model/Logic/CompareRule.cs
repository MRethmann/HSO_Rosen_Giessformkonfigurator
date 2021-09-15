//-----------------------------------------------------------------------
// <copyright file="CompareRule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gieﬂformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gieﬂformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gieﬂformkonfigurator.WPF.MVVM.Model.Db_products;

    abstract class CompareRule
    {
        protected abstract IEnumerable<Type> Typen { get; }

        public virtual bool Akzeptiert(Type teilTyp1, Type teilTyp2)
        {
            if (!teilTyp1.IsSubclassOf(typeof(Product)) || !teilTyp2.IsSubclassOf(typeof(Mold)))
            {
                return false;
            }

            return this.Typen.ElementAt(0) == teilTyp1
                && this.Typen.ElementAt(1) == teilTyp2;
        }

        public abstract bool Compare(Product a, Mold b);
    }

    // Only for ModularMolds
    class ProduktCupCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductCup), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var product = compareElements.OfType<ProductCup>().Single();
            var gieﬂform = compareElements.OfType<ModularMold>().Single();

            if (product.HoleCircle != null)
            {
                return product.InnerDiameter > gieﬂform.core.OuterDiameter
                    && (product.InnerDiameter + 1) < gieﬂform.core.OuterDiameter
                    && product.BaseCup == gieﬂform.cupform.CupType;
            }
            else
            {
                return false;
            }
        }
    }

    // Only for ModularMolds
    class ProduktDiscCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var product = compareElements.OfType<ProductDisc>().Single();
            var gieﬂform = compareElements.OfType<ModularMold>().Single();

            // TODO: Innenring als Attribut hinzuf¸gen
            if (product.Hc1Diameter != 0.0m)
            {
                return product.OuterDiameter <= gieﬂform.guideRing.InnerDiameter
                    && product.InnerDiameter > gieﬂform.core.OuterDiameter
                    && product.Height <= gieﬂform.guideRing.Height
                    && product.Height <= gieﬂform.core.Height;
            }
            else
            {
                return false;
            }
        }
    }
}