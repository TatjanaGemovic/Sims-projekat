using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class AllergenService
    {
        public AllergenRepository AllergenRepository { get; set; }

        public Allergen AddAllergen(Allergen allergen)
        {
            return AllergenRepository.AddAllergen(allergen);
        }

        public Allergen DeleteAllergen(Allergen allergen)
        {
            return AllergenRepository.DeleteAllergen(allergen);
        }

        public List<Allergen> GetAllAllergens()
        {
            return AllergenRepository.Allergens;
        }
        public void Serialize()
        {
            AllergenRepository.Serialize();
        }

        public void Deserialize()
        {
            AllergenRepository.Deserialize();
        }

    }
}
