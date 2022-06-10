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
                return new ValidationResult(false, "Molimo Vas popunite polje");
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(sVal, @"[^a-zA-Z]+$"))
            {
                return new ValidationResult(false, "Naziv treba da sadrzi samo slova");
            }

            return new ValidationResult(true, null);
        }
    }
}
