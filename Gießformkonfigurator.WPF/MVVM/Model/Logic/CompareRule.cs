//-----------------------------------------------------------------------
// <copyright file="CompareRule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// All CompareRules that are placed within the CombinationRuleSet.
    /// </summary>
    abstract class CompareRule
    {
        public ToleranceSettings ToleranceSettings { get; set; } = new ToleranceSettings();

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
                return productCup.ModularMoldDimensions.InnerDiameter > modularMold.Core.OuterDiameter
                    && (productCup.ModularMoldDimensions.InnerDiameter + 1) < modularMold.Core.OuterDiameter
                    && productCup.BaseCup == modularMold.Cupform.CupType;
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
            var productDisc = compareElements.OfType<ProductDisc>().Single();
            var modularMold = compareElements.OfType<ModularMold>().Single();

            return productDisc.ModularMoldDimensions.OuterDiameter <= modularMold.GuideRing.InnerDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                && productDisc.ModularMoldDimensions.InnerDiameter >= modularMold.Core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                && productDisc.ModularMoldDimensions.Height <= (modularMold.GuideRing.FillHeightMax > 0 ? modularMold.GuideRing.FillHeightMax : modularMold.GuideRing.Height)
                && productDisc.ModularMoldDimensions.Height <= (modularMold.Core.FillHeightMax > 0 ? modularMold.Core.FillHeightMax : modularMold.Core.Height);
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
            if (productDisc.SingleMoldDimensions.OuterDiameter <= singleMoldDisc.OuterDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                && productDisc.SingleMoldDimensions.InnerDiameter >= singleMoldDisc.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                && productDisc.SingleMoldDimensions.Height <= singleMoldDisc.Height)
            {
                // Product and singleMoldDisc have BTC
                if (!string.IsNullOrWhiteSpace(productDisc.BTC)
                        && singleMoldDisc.HcDiameter != null && singleMoldDisc.HcDiameter > 0
                        && singleMoldDisc.HcHoles != null && singleMoldDisc.HcHoles > 0
                        && singleMoldDisc.BoltDiameter != null && singleMoldDisc.BoltDiameter > 0)
                {
                    return (productDisc.HcDiameter == null || productDisc.HcDiameter <= 0 || (productDisc.SingleMoldDimensions.HcDiameter <= singleMoldDisc.HcDiameter + this.ToleranceSettings.Hc_Diameter && productDisc.SingleMoldDimensions.HcDiameter >= singleMoldDisc.HcDiameter - this.ToleranceSettings.Hc_Diameter))
                        && (productDisc.HcHoleDiameter == null || productDisc.HcHoleDiameter <= 0 || (productDisc.SingleMoldDimensions.HcHoleDiameter >= singleMoldDisc.BoltDiameter - this.ToleranceSettings.Bolt_Diameter && productDisc.SingleMoldDimensions.HcHoleDiameter <= singleMoldDisc.BoltDiameter + this.ToleranceSettings.Bolt_Diameter))
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