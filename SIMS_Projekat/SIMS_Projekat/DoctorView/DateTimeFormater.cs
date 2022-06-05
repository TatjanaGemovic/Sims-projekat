using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DoctorView
{
    public class DateTimeFormater
    {
        public int FormatDate(string selectedDate)
        {
            String[] datePart2 = selectedDate.Split(" ");
            String date2 = datePart2[0];
            String[] detePart2 = date2.Split("/");
            return int.Parse(detePart2[1]);
        }

        public string FormatTime(Model.FreeDayRequest r)
        {
            string from1 = r.from.ToString();
            string until1 = r.until.ToString();
            String[] parts = from1.Split(" ");
            String[] parts2 = until1.Split(" ");
            return parts[0] + "  -  " + parts2[0];
        }

        public int ChangeDateFormat(DateTime date)
        {
            DateTime dt = date;
            string dateTime = dt.ToString("MM/dd/yyyy HH:mm");
            String[] datePart = dateTime.Split(" ");
            string date1 = datePart[0]; //datum
            String[] deoDatuma = date1.Split("/");
            int dan = int.Parse(deoDatuma[1]);
            return dan;
        }

        public int ChangeDateFormat2(string date)
        {
            String[] deoDatuma2 = date.Split("/");
            int dan2 = int.Parse(deoDatuma2[1]);
            return dan2;
        }
    }
}
