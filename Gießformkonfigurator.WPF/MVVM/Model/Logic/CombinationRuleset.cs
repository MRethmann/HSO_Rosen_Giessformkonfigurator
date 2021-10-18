//-----------------------------------------------------------------------
// <copyright file="CombinationRuleset.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.Model.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_components;

    /// <summary>
    /// Contains all CombinationRules.
    /// </summary>
    public class CombinationRuleset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationRuleset"/> class.
        /// </summary>
        public CombinationRuleset()
        {
            // hier müssen alle Regeln registriert werden, damit sie verwendet werden.
            this.CombinationRules = new CombinationRule[]
                    {
                    new BaseplateCoreCombination(),
                    new BaseplateInsertPlateCombination(),
                    new BaseplateRingCombination(),
                    new InsertPlateCoreCombination(),
                    new RingAddition(),
                    new CupformCoreCombination(),
                    new CoreRingAddition(),
                    };
        }

        private IEnumerable<CombinationRule> CombinationRules { get; set; }

        /// <summary>
        /// Looks within the CombinationRuleSet if any fitting Rule is found.
        /// The lookup is based on parameters within the method call.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Combine(Component a, Component b)
        {
            var passendeKombinationen = this.CombinationRules.Where(k => k.Akzeptiert(a.GetType(), b.GetType()));

            if (!passendeKombinationen.Any())
            {
                return false;
            }

            return passendeKombinationen.All(k => k.Combine(a, b));
        }
    }
}
