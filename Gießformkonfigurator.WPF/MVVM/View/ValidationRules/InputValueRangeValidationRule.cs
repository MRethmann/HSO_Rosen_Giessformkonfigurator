using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Giessformkonfigurator.WPF.MVVM.View.ValidationRules
{
    /// <summary>
    /// ValidationRule to check if the Input Value is positive and not greater than 100.000. This rule should avoid input mistakes.
    /// </summary>
    public class InputValueRangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int inputIntValue;
            double inputDoubleValue;

            if (value != null)
            {
                if (int.TryParse(value.ToString(), out inputIntValue))
                {
                    if (inputIntValue < 0 || inputIntValue > 100000)
                    {
                        result = new ValidationResult(false, "Der Eingabewert darf nicht negativ oder größer 100.000 sein.");
                    }
                }
                else if (double.TryParse(value.ToString(), out inputDoubleValue))
                {
                    if (inputDoubleValue < 0 || inputDoubleValue > 100000)
                    {
                        result = new ValidationResult(false, "Der Eingabewert darf nicht negativ oder größer 100.000 sein.");
                    }
                }
            }

            return result;
        }
    }
}
