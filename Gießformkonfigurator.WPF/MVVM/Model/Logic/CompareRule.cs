//-----------------------------------------------------------------------
// <copyright file="CompareRule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gie�formkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gie�formkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gie�formkonfigurator.WPF.MVVM.Model.Db_products;

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
            var gie�form = compareElements.OfType<ModularMold>().Single();

            if (product.HoleCircle != null)
            {
                return product.InnerDiameter > gie�form.core.OuterDiameter
                    && (product.InnerDiameter + 1) < gie�form.core.OuterDiameter
                    && product.BaseCup == gie�form.cupform.CupType;
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
            var gie�form = compareElements.OfType<ModularMold>().Single();

            // TODO: Innenring als Attribut hinzuf�gen
            if (product.Hc1Diameter != 0.0m)
            {
                return product.OuterDiameter <= gie�form.guideRing.InnerDiameter
                    && product.InnerDiameter > gie�form.core.OuterDiameter
                    && product.Height <= gie�form.guideRing.Height
                    && product.Height <= gie�form.core.Height;
            }
            else
            {
                return false;
            }
        }
    }
}