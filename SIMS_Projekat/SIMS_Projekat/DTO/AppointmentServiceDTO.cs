using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DTO
{
    public class AppointmentServiceDTO
    {
        public Doctor doctor { get; set; }
        public string date { get; set; }
        public Room room { get; set; }
        public Patient patient { get; set; }

    }
}
