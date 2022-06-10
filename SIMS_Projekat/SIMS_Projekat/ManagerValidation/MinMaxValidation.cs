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
                    return new ValidationResult(false, "Minimalna vrednost za premestanje je 1");
                }
                else if (d > Max)
                {
                    return new ValidationResult(false, "Uneli ste vecu vrednost od dostupne kolicine za premestanje");
                }

               
            }
            return new ValidationResult(true, null);
        }
    }
}
