using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class AllergenRepository
    {
        public List<Allergen> Allergens { get; set; }
        private Serializer<Allergen> allergensSerializer;

        private string allergensFile;

        public AllergenRepository(string allergensFileName)
        {
            Allergens = new List<Allergen>();
            allergensSerializer = new Serializer<Allergen>();
            allergensFile = allergensFileName;
        }

        public void Serialize()
        {
            allergensSerializer.toCSV(allergensFile, Allergens);
        }

        public void Deserialize()
        {
            Allergens = allergensSerializer.fromCSV(allergensFile);
        }

        public Allergen AddAllergen(Allergen allergen)
        {
            Allergens.Add(allergen);
            return allergen;
        }

        public Allergen DeleteAllergen(Allergen allergen)
        {
            return Allergens.Remove(allergen) ? allergen: null;
        }

    }
}
