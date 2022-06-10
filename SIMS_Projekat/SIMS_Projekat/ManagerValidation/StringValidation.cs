using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerValidation
{
    public class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string sVal = value as string;

            if (System.Text.RegularExpressions.Regex.IsMatch(sVal, @"[^a-zA-Z]+$"))
            {
                return new ValidationResult(false, "Naziv treba da sadrzi samo slova");
            }

            return new ValidationResult(true, null);
        }
    }
}
