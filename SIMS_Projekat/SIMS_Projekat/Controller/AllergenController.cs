using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class AllergenController
    {
        public AllergenService AllergenService { get; set; }

        public Allergen AddAllergen(Allergen allergen)
        {
            return AllergenService.AddAllergen(allergen);
        }

        public Allergen DeleteAllergen(Allergen allergen)
        {
            return AllergenService.DeleteAllergen(allergen);
        }

        public List<Allergen> GetAllAlergens()
        {
            return AllergenService.GetAllAllergens();
        }

        public void Serialize()
        {
            AllergenService.Serialize();
        }

        public void Deserialize()
        {
            AllergenService.Deserialize();
        }
    }
}
