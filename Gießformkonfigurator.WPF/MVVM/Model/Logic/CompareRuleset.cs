//-----------------------------------------------------------------------
// <copyright file="CompareRuleset.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_products;

    /// <summary>
    /// Contains all CompareRules.
    /// </summary>
    public class CompareRuleSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareRuleSet"/> class.
        /// </summary>
        public CompareRuleSet()
        {
            // hier müssen alle Regeln registriert werden, damit sie verwendet werden.
            this.CompareRules = new CompareRule[]
                    {
                    new ProductCupModularMoldCompare(),
                    new ProductDiscModularMoldCompare(),
                    new ProductDiscSingleMoldCompare(),
                    };
        }

        private IEnumerable<CompareRule> CompareRules { get; set; }

        public bool Compare(Product a, Mold b)
        {
            var passendeKombinationen = this.CompareRules.Where(k => k.Akzeptiert(a.GetType(), b.GetType()));

            if (!passendeKombinationen.Any())
            {
                return false;
            }

            return passendeKombinationen.All(k => k.Compare(a, b));
        }
    }
}
