using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerValidation
{
    public class EmptyDatePickerValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            if (DateTime.TryParse(value.ToString(), out DateTime result))
            {
                return new ValidationResult(true, null);

            }

            return new ValidationResult(false, "Molimo Vas izaberite datum");
        }
    }
}
