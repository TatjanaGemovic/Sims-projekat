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
    internal class DoctorSpecialityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DoctorSpeciality doctorSpeciality = (DoctorSpeciality)value;

            return doctorSpeciality switch
            {
                DoctorSpeciality.PEDIATRICIAN => "Pedijatar",
                DoctorSpeciality.GENERAL_PRACTITIONER => "Opšta praksa",
                DoctorSpeciality.INTERNIST => "Internista",
                DoctorSpeciality.DERMATOLOGIST => "Dermatolog",
                DoctorSpeciality.CARDIOLOGIST => "Kardiolog",
                DoctorSpeciality.SURGEON => "Hirurg",
                _ => "",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string doctorSpeciality = value as string;

            switch (doctorSpeciality)
            {
                case "Pedijatar":
                    return DoctorSpeciality.PEDIATRICIAN;
                case "Opšta praksa":
                    return DoctorSpeciality.GENERAL_PRACTITIONER;
                case "Internista":
                    return DoctorSpeciality.INTERNIST;
                case "Kardiolog":
                    return DoctorSpeciality.CARDIOLOGIST;
                case "Hirurg":
                    return DoctorSpeciality.SURGEON;
                case "Dermatolog":
                    return DoctorSpeciality.DERMATOLOGIST;
                default:
                    return "";
            }

        }
    }
}
