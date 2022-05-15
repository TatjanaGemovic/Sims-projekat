using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SIMS_Projekat.Model;

namespace SIMS_Projekat.SecretaryView.Converters
{
    internal class BloodTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BloodType bloodType = (BloodType)value;

            return bloodType switch
            {
                BloodType.A_Positive => "A+",
                BloodType.A_Negative => "A-",
                BloodType.B_Positive => "B+",
                BloodType.B_Negative => "B-",
                BloodType.AB_Positive => "AB+",
                BloodType.AB_Negative => "AB-",
                BloodType.O_Positive => "O+",
                BloodType.O_Negative => "0-",
                _ => "",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string bloodType = value as string;

            switch (bloodType)
            {
                case "A+":
                    return BloodType.A_Positive;
                case "A-":
                    return BloodType.A_Negative;
                case "B+":
                    return BloodType.B_Positive;
                case "B-":
                    return BloodType.B_Negative;
                case "AB+":
                    return BloodType.AB_Positive;
                case "AB-":
                    return BloodType.AB_Negative;
                case "0+":
                    return BloodType.O_Positive;
                case "0-":
                    return BloodType.O_Negative;
                default:
                    return "";
            }

        }
    }
}
