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
    internal class RoomTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoomType roomType = (RoomType)value;

            return roomType switch
            {
                RoomType.examRoom => "Soba za preglede",
                RoomType.operatingRoom => "Operaciona sala",
                RoomType.meetingRoom => "Soba za sastanke",
                RoomType.recoveryRoom => "Soba za oporavak",
                _ => "",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string roomType = value as string;

            switch (roomType)
            {
                case "Soba za preglede":
                    return RoomType.examRoom;
                case "Operaciona sala":
                    return RoomType.operatingRoom;
                case "Soba za sastanke":
                    return RoomType.meetingRoom;
                case "Soba za oporavak":
                    return RoomType.recoveryRoom;
                default:
                    return "";
            }

        }
    }
}
