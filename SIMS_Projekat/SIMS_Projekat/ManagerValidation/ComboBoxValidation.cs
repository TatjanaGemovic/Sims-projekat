using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.ManagerValidation
{
    public class ComboBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var i = int.Parse(value.ToString());
            if ( i== -1)
                return new ValidationResult(false, "Molimo Vas popunite ovo polje");

            return new ValidationResult(true, null);
        }
    }
}
