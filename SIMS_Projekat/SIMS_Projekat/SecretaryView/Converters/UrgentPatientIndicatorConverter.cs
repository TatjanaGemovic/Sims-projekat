using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMS_Projekat.SecretaryView.Converters
{
    internal class UrgentPatientIndicatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;

            if (state)
                return "Da";
            return "Ne";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string state = value as string;
            if (state.Equals("Da"))
                return true;
            return false;

        }
    }
}
