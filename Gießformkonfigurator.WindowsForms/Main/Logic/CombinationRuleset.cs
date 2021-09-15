namespace Gießformkonfigurator.WindowsForms.Main.Logik
{
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WindowsForms.Main.Db_components;

    public class CombinationRuleset
    {
        private IEnumerable<CombinationRule> CombinationRules { get; set; }

        public CombinationRuleset()
        {
            // hier müssen alle Regeln registriert werden, damit sie verwendet werden.
            this.CombinationRules = new CombinationRule[]
                    {
                    new BaseplateCoreCombination(),
                    new BaseplateInsertPlateCombination(),
                    new BaseplateRingCombination(),
                    new InsertPlateCoreCombination(),
                    new InnerRingAddition(),
                    };
        }

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
