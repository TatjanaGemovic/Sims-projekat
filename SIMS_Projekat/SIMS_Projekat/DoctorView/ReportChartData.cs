using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DoctorView
{
    public class ReportChartData
    {
        public int AppNum { get; set; }
        public int Days { get; set; }

        public ReportChartData(int m, int br)
        {
            AppNum = br;
            Days = m;
        }
    }
}
