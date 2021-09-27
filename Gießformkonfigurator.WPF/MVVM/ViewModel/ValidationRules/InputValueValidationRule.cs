using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gießformkonfigurator.WPF.MVVM.ViewModel.ValidationRules
{
    class InputValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int inputIntValue;
            double inputDoubleValue;

            if (int.TryParse(value.ToString(), out inputIntValue))
            {
                if (inputIntValue < 0 || inputIntValue > 100000)
                {
                    result = new ValidationResult(false, "Der Eingabewert darf nicht negativ oder größer 100.000 sein.");
                }
            }
            else if(double.TryParse(value.ToString(), out inputDoubleValue))
            {
                if (inputDoubleValue < 0 || inputDoubleValue > 100000)
                {
                    result = new ValidationResult(false, "Der Eingabewert darf nicht negativ oder größer 100.000 sein.");
                }
            }

            return result;
        }
    }
}
