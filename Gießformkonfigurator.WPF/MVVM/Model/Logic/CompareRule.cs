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
    class ProductCupModularMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductCup), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productCup = compareElements.OfType<ProductCup>().Single();
            var modularMold = compareElements.OfType<ModularMold>().Single();

            if (productCup.BTC != null)
            {
                return productCup.InnerDiameter > modularMold.core.OuterDiameter
                    && (productCup.InnerDiameter + 1) < modularMold.core.OuterDiameter
                    && productCup.BaseCup == modularMold.cupform.CupType;
            }
            else
            {
                return false;
            }
        }
    }

    // Only for ModularMolds
    class ProductDiscModularMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var product = compareElements.OfType<ProductDisc>().Single();
            var modularMold = compareElements.OfType<ModularMold>().Single();

            // TODO: Innenring als Attribut hinzuf¸gen
            if (product.HcDiameter != 0.0m)
            {
                return product.OuterDiameter <= modularMold.guideRing.InnerDiameter
                    && product.InnerDiameter > modularMold.core.OuterDiameter
                    && product.Height <= modularMold.guideRing.Height
                    && product.Height <= modularMold.core.Height;
            }
            else
            {
                return false;
            }
        }
    }

    class ProductSingleDiscCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(SingleMoldDisc) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productDisc = compareElements.OfType<ProductDisc>().Single();
            var singleMoldDisc = compareElements.OfType<SingleMoldDisc>().Single();

            return productDisc.OuterDiameter <= singleMoldDisc.OuterDiameter
                && productDisc.InnerDiameter >= singleMoldDisc.InnerDiameter
                && productDisc.HcDiameter <= singleMoldDisc.HcDiameter + 0.5m
                && productDisc.HcDiameter >= singleMoldDisc.HcDiameter - 0.5m
                && productDisc.HcHoleDiameter >= singleMoldDisc.BoltDiameter
                && productDisc.HcHoles == singleMoldDisc.HcHoles;
        }
    }
}