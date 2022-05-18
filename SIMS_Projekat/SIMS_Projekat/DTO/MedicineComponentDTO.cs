using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DTO
{
    public class MedicineComponentDTO : Serialization.Serializable
    {
        public string dtoID;
        public string mainMedicineID;
        public string component;

        public void fromCSV(string[] values)
        {
            dtoID = values[0];
            mainMedicineID = values[1];
            component = values[2];

        }

        public string[] toCSV()
        {
            string[] values = {
                dtoID,
                mainMedicineID,
                component

            };

            return values;
        }
    }
}
