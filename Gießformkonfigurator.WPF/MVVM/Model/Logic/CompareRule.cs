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
    public abstract class CompareRule
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

    /// <summary>
    /// Checks if the basic variant of modular mold cup (cupform, core) fits the product.
    /// TODO: Change logic so the comparison uses the best coreRing/outerRing as compare parameter.
    /// </summary>
    public class ProductCupModularMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductCup), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productCup = compareElements.OfType<ProductCup>().Single();
            var modularMold = compareElements.OfType<ModularMold>().Single();

            var btcList = new List<string>()
            {
                modularMold.Cupform.BTC1,
                modularMold.Cupform.BTC2,
                modularMold.Cupform.BTC3,
            };

            // Product.BTC = TRUE -- Cupform.BTC = TRUE
            if (!string.IsNullOrWhiteSpace(productCup.BTC)
                && (!string.IsNullOrWhiteSpace(modularMold.Cupform.BTC1)
                || !string.IsNullOrWhiteSpace(modularMold.Cupform.BTC2)
                || !string.IsNullOrWhiteSpace(modularMold.Cupform.BTC3)))
            {
                // Cupform has no fitting BTC.
                if (btcList.Contains(productCup.BTC) == false && modularMold.Cupform.HasFixedBTC)
                {
                    return false;
                }
                else
                {
                    if (modularMold.Core == null)
                    {
                        return productCup.ModularMoldDimensions.InnerDiameter >= modularMold.Cupform.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                        && productCup.CupType.Equals(modularMold.Cupform.CupType)
                        && productCup.Size == modularMold.Cupform.Size;
                    }
                    else
                    {
                        return productCup.ModularMoldDimensions.InnerDiameter >= modularMold.Core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                        && productCup.CupType.Equals(modularMold.Cupform.CupType)
                        && productCup.Size == modularMold.Cupform.Size;
                    }
                }
            }

            // Product.BTC = FALSE -- Cupform.FixedBTC = TRUE
            else if (string.IsNullOrWhiteSpace(productCup.BTC) && modularMold.Cupform.HasFixedBTC)
            {
                return false;
            }

            // Product.BTC = FALSE -- Cupform.BTC = FALSE
            else
            {
                if (modularMold.Core == null)
                {
                    return productCup.ModularMoldDimensions.InnerDiameter >= modularMold.Cupform.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                    && productCup.CupType.Equals(modularMold.Cupform.CupType)
                    && productCup.Size == modularMold.Cupform.Size;
                }
                else
                {
                    return productCup.ModularMoldDimensions.InnerDiameter >= modularMold.Core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                    && productCup.CupType.Equals(modularMold.Cupform.CupType)
                    && productCup.Size == modularMold.Cupform.Size;
                }
            }
        }
    }

    /// <summary>
    /// Checks if the basic variant of modular mold disc (baseplate, ring, core) fits the product.
    /// Probably a logical mistake in this algorithm. The comparison always checks for core dimensions, even when coreRings or outerRings are used.
    /// TODO: Change logic so the comparison in line 82/83 uses the best coreRing/outerRing as compare parameter.
    /// </summary>
    public class ProductDiscModularMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(ModularMold) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productDisc = compareElements.OfType<ProductDisc>().Single();
            var modularMold = compareElements.OfType<ModularMold>().Single();

            return productDisc.ModularMoldDimensions.OuterDiameter <= modularMold.GuideRing.InnerDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                && productDisc.ModularMoldDimensions.InnerDiameter >= modularMold.Core.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN
                && productDisc.ModularMoldDimensions.Height <= (modularMold.GuideRing.FillHeightMax > 0 ? modularMold.GuideRing.FillHeightMax + this.ToleranceSettings.Product_Height_MIN : modularMold.GuideRing.Height + this.ToleranceSettings.Product_Height_MIN)
                && productDisc.ModularMoldDimensions.Height <= (modularMold.Core.FillHeightMax > 0 ? modularMold.Core.FillHeightMax + this.ToleranceSettings.Product_Height_MIN : modularMold.Core.Height + this.ToleranceSettings.Product_Height_MIN);
        }
    }

    public class ProductDiscSingleMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductDisc), typeof(SingleMoldDisc) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productDisc = compareElements.OfType<ProductDisc>().Single();
            var singleMoldDisc = compareElements.OfType<SingleMoldDisc>().Single();

            // General comparison between single Mold and product
            if (productDisc.SingleMoldDimensions.OuterDiameter <= singleMoldDisc.OuterDiameter + this.ToleranceSettings.Product_OuterDiameter_MIN
                && ((singleMoldDisc.CoreSingleMold == null && productDisc.SingleMoldDimensions.InnerDiameter >= singleMoldDisc.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                || (singleMoldDisc.CoreSingleMold != null && productDisc.SingleMoldDimensions.InnerDiameter >= singleMoldDisc.CoreSingleMold.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN))
                && productDisc.SingleMoldDimensions.Height <= singleMoldDisc.Height + this.ToleranceSettings.Product_Height_MIN)
            {
                // Product and singleMoldDisc have BTC
                if (!string.IsNullOrWhiteSpace(productDisc.BTC) && !string.IsNullOrWhiteSpace(singleMoldDisc.BTC))
                        //TODO: Pr�fen, ob einfach der BTC als Vergleich genutzt werden darf. Passt das mit dem PU-Faktor?

                    // && singleMoldDisc.HcDiameter != null && singleMoldDisc.HcDiameter > 0
                        // && singleMoldDisc.HcHoles != null && singleMoldDisc.HcHoles > 0
                        // && singleMoldDisc.BoltDiameter != null && singleMoldDisc.BoltDiameter > 0)
                {
                    if (productDisc.BTC.Equals(singleMoldDisc.BTC))
                    {
                        singleMoldDisc.HasFittingBTC = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    // return (productDisc.HcDiameter == null || productDisc.HcDiameter <= 0 || (productDisc.SingleMoldDimensions.HcDiameter <= singleMoldDisc.HcDiameter + this.ToleranceSettings.Hc_Diameter && productDisc.SingleMoldDimensions.HcDiameter >= singleMoldDisc.HcDiameter - this.ToleranceSettings.Hc_Diameter))
                        // && (productDisc.HcHoleDiameter == null || productDisc.HcHoleDiameter <= 0 || (productDisc.SingleMoldDimensions.HcHoleDiameter >= singleMoldDisc.BoltDiameter - this.ToleranceSettings.Bolt_Diameter && productDisc.SingleMoldDimensions.HcHoleDiameter <= singleMoldDisc.BoltDiameter + this.ToleranceSettings.Bolt_Diameter))
                       // && (productDisc.HcHoles == null || productDisc.HcHoles <= 0 || productDisc.HcHoles == singleMoldDisc.HcHoles);
                }

                // Product has no fitting BTC
                else if (!string.IsNullOrWhiteSpace(singleMoldDisc.BTC) && string.IsNullOrWhiteSpace(productDisc.BTC))
                {
                    return false;
                    /*return singleMoldDisc.HcDiameter == null || singleMoldDisc.HcDiameter <= 0
                    || singleMoldDisc.HcHoles == null || singleMoldDisc.HcHoles <= 0
                    || singleMoldDisc.BoltDiameter == null || singleMoldDisc.BoltDiameter <= 0;*/
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public class ProductCupSingleMoldCompare : CompareRule
    {
        protected override IEnumerable<Type> Typen => new[] { typeof(ProductCup), typeof(SingleMoldCup) };

        public override bool Compare(Product a, Mold b)
        {
            object[] compareElements = new object[] { a, b };
            var productCup = compareElements.OfType<ProductCup>().Single();
            var singleMoldCup = compareElements.OfType<SingleMoldCup>().Single();

            if (productCup.CupType.Equals(singleMoldCup.CupType)
                && productCup.Size == singleMoldCup.Size
                && ((singleMoldCup.CoreSingleMold == null && productCup.SingleMoldDimensions.InnerDiameter >= singleMoldCup.InnerDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)
                || (singleMoldCup.CoreSingleMold != null && productCup.SingleMoldDimensions.InnerDiameter >= singleMoldCup.CoreSingleMold.OuterDiameter - this.ToleranceSettings.Product_InnerDiameter_MIN)))
            {
                // Product and singleMold have BTC
                if (!string.IsNullOrWhiteSpace(productCup.BTC) && !string.IsNullOrWhiteSpace(singleMoldCup.BTC))
                {
                    if (singleMoldCup.BTC.Equals(productCup.BTC))
                    {
                        singleMoldCup.HasFittingBTC = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                // SingleMold has BTC but product does not.
                else if (!string.IsNullOrWhiteSpace(singleMoldCup.BTC) && string.IsNullOrWhiteSpace(productCup.BTC))
                {
                    return false;
                }

                // Product has BTC but singleMold does not.
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}