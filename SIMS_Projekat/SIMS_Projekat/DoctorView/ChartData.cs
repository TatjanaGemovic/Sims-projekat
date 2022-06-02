using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DoctorView
{
    public class ChartData
    {
        public int RequestsNum { get; set; }
        public string Month { get; set; }

        public ChartData(string m, int br)
        {
            RequestsNum = br;
            Month = m;
        }
    }
}
