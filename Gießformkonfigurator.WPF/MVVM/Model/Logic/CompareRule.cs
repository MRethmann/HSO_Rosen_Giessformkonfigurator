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
    using Gieﬂformkonfigurator.WPF.Core;
    using Gieﬂformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gieﬂformkonfigurator.WPF.MVVM.Model.Db_products;

    abstract class CompareRule
    {
        public ToleranceSettings toleranceSettings { get; set; } = new ToleranceSettings();

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

            return product.OuterDiameter <= modularMold.guideRing.InnerDiameter + toleranceSettings.product_OuterDiameter_MIN //Tolerance ring innerDiameter MIN
                && product.InnerDiameter >= modularMold.core.OuterDiameter - toleranceSettings.product_InnerDiameter_MIN //Tolerance core outerDiameter MIN 
                && product.Height <= (modularMold.guideRing.FillHeightMax > 0 ? modularMold.guideRing.FillHeightMax : modularMold.guideRing.Height)
                && product.Height <= (modularMold.core.FillHeightMax > 0 ? modularMold.core.FillHeightMax : modularMold.core.Height);
        }
    }

    class ProductDiscSingleMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(SingleMoldDisc) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productDisc = compareElements.OfType<ProductDisc>().Single();
            var singleMoldDisc = compareElements.OfType<SingleMoldDisc>().Single();

            // General comparison between single Mold and product
            if (productDisc.OuterDiameter <= singleMoldDisc.OuterDiameter + toleranceSettings.product_OuterDiameter_MIN
                && productDisc.InnerDiameter >= singleMoldDisc.InnerDiameter - toleranceSettings.product_InnerDiameter_MIN)
            {
                // Product and singleMoldDisc have BTC
                if (!String.IsNullOrWhiteSpace(productDisc.BTC) 
                        && (singleMoldDisc.HcDiameter != null && singleMoldDisc.HcDiameter > 0)
                        && (singleMoldDisc.HcHoles != null && singleMoldDisc.HcHoles > 0)
                        && (singleMoldDisc.BoltDiameter != null && singleMoldDisc.BoltDiameter > 0))
                {
                    return (productDisc.HcDiameter == null || productDisc.HcDiameter <= 0 || (productDisc.HcDiameter <= singleMoldDisc.HcDiameter + toleranceSettings.hc_Diameter && productDisc.HcDiameter >= singleMoldDisc.HcDiameter - toleranceSettings.hc_Diameter))
                        && (productDisc.HcHoleDiameter == null || productDisc.HcHoleDiameter <= 0 || (productDisc.HcHoleDiameter >= singleMoldDisc.BoltDiameter - toleranceSettings.bolt_Diameter && productDisc.HcHoleDiameter <= singleMoldDisc.BoltDiameter + toleranceSettings.bolt_Diameter))
                        && (productDisc.HcHoles == null || productDisc.HcHoles <= 0 || productDisc.HcHoles == singleMoldDisc.HcHoles);
                }

                // Product has no fitting BTC
                else
                {
                    return singleMoldDisc.HcDiameter == null || singleMoldDisc.HcDiameter <= 0
                    || singleMoldDisc.HcHoles == null || singleMoldDisc.HcHoles <= 0
                    || singleMoldDisc.BoltDiameter == null || singleMoldDisc.BoltDiameter <= 0;
                }
            }
            else
            {
                return false;
            }
        }
    }
}