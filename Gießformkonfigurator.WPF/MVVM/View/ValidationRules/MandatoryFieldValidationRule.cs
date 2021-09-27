using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gießformkonfigurator.WPF.MVVM.View.ValidationRules
{
    class MandatoryFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int inputValue;

            /*if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "Das Feld darf nicht leer sein!");
            }*/

            if (value.ToString() == "Albert")
            {
                result = new ValidationResult(false, "Der Eingabewert darf nicht Albert sein.");
            }

            return result;
        }
    }
}
