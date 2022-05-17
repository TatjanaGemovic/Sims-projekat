﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMS_Projekat.PatientView.Converters
{
    internal class ToolTipTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if (value.Equals("Operacija"))
                {
                    return "Ne mozete da pomerite termin operacije";
                }
                else
                    return "Vec ste pomerili termin pregleda!";

            }
            else 
                return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}