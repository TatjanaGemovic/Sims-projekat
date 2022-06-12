using SIMS_Projekat.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerValidation
{
    public class IntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

           if(int.TryParse(value.ToString(), out int result))
            {
                return new ValidationResult(true, null);

            }

           if(string.IsNullOrEmpty(value.ToString()))
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                    return new ValidationResult(false, "Molimo Vas popunite ovo polje");
                else
                    return new ValidationResult(false, "Please fill this field");

            if (Settings.Default.CurrentLanguage == "sr-LATN")
                return new ValidationResult(false, "Molimo Vas unesite broj");
            else
                return new ValidationResult(false, "This field requires integer");
        }
    }
}
