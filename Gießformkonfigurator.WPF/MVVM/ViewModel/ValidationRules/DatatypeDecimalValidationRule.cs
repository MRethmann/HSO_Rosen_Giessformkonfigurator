using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel.ValidationRules
{
    class DatatypeDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int inputValue;
            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "Das Feld darf nicht leer sein!");
            }
            else if (int.TryParse(value.ToString(), out inputValue))
            {
                if (inputValue < 0 || inputValue > 100000)
                {
                    result = new ValidationResult(false, "Der Eingabewert darf nicht negativ oder größer 100.000 sein.");
                }
            }
            else if (decimal.TryParse(value.ToString(), out decimal output) == false)
            {
                result = new ValidationResult(false, "Eingabewert muss eine Zahl zwischen 0 und 100.000 sein.");
            }

            return result;
        }
    }
}
