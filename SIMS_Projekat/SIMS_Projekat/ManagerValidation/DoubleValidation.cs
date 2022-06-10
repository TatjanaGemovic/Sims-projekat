using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerValidation
{
    public class DoubleValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return new ValidationResult(true, null);

            }

            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, "Molimo Vas popunite ovo polje");

            return new ValidationResult(false, "Molimo Vas unesite broj");
        }
    }
}
