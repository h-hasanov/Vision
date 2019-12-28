using System.Diagnostics;
using System.Windows.Controls;

namespace HH.View.Utils.ValidationRules
{
    // see https://blog.magnusmontin.net/2013/08/26/data-validation-in-wpf/ how to use
    [DebuggerNonUserCode]
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Please enter a valid integer value.");
            }

            int i;
            if (int.TryParse(value.ToString(), out i))
            {
                //return new ValidationResult(true, null);
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Please enter a valid integer value.");
        }
    }
}