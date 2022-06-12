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
    public class OnlyStringValidationcs : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string sVal = value as string;

            if (string.IsNullOrEmpty(sVal))
            {
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                    return new ValidationResult(false, "Molimo Vas popunite ovo polje");
                else
                    return new ValidationResult(false, "Please fill this field");
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(sVal, @"[^a-zA-Z]+$"))
            {
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                    return new ValidationResult(false, "Molimo Vas unesite broj");
                else
                    return new ValidationResult(false, "This field requires only words");
                
            }

            return new ValidationResult(true, null);
        }
    }
}
