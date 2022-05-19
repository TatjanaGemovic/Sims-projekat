using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMS_Projekat.PatientView
{
    internal class LabelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {


            //if (values == null) 
            //    return "Visible";
            //foreach (object value in values)
            //{
            //    if ((bool)value)
            //        return "Hidden";
            //}
            //return "Visible";
            if (values == null) return false;
            return values.Any(v =>
            {
                bool? b = v as bool?;
                return b.HasValue && b.Value;
            });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
