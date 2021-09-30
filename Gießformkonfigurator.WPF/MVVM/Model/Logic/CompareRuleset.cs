//-----------------------------------------------------------------------
// <copyright file="CompareRuleset.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gie�formkonfigurator.WPF.MVVM.Model.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Gie�formkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gie�formkonfigurator.WPF.MVVM.Model.Db_products;

    class CompareRuleSet
    {
        private IEnumerable<CompareRule> CompareRules { get; set; }

        public CompareRuleSet()
        {
            // hier m�ssen alle Regeln registriert werden, damit sie verwendet werden.
            this.CompareRules = new CompareRule[]
                    {
                    new ProductCupModularMoldCompare(),
                    new ProductDiscModularMoldCompare(),
                    new ProductDiscSingleMoldCompare()
                    };
        }

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
