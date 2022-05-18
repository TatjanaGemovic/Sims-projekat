using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DTO
{
    public class ReplacmentMedicineDTO : Serialization.Serializable
    {
        public string dtoID;
        public string mainMedicineID;
        public string replacmentMedicineID;

        public void fromCSV(string[] values)
        {
            dtoID = values[0];
            mainMedicineID = values[1];
            replacmentMedicineID = values[2];
 
        }

        public string[] toCSV()
        {
            string[] values = {
                dtoID,
                mainMedicineID,
                replacmentMedicineID

            };

            return values;
        }

    }
}
