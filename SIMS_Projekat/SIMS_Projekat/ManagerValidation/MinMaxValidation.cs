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
    public class MinMaxValidation : ValidationRule
    {
        public int Min
        {
            get; set;
        }
        public int Max
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int result))
            {
                int d = result;
                if (d < Min)
                {
                    if (Settings.Default.CurrentLanguage == "sr-LATN")
                        return new ValidationResult(false, "Minimalna vrednost za premeštanje je 1");
                    else
                        return new ValidationResult(false, "Minimal quantity is 1");
                    
                }
                else if (d > Max)
                {
                    if (Settings.Default.CurrentLanguage == "sr-LATN")
                        return new ValidationResult(false, "Uneli ste veću vrednost od dostupne količine za premeštanje");
                    else
                        return new ValidationResult(false, "You have exceeded the limits");
                   
                }

               
            }
            return new ValidationResult(true, null);
        }
    }
}
