using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public interface IRepository<Entity> where Entity : new()
    {
        Entity Add(Entity entity);

        Entity Delete(Entity entity);

        Entity Edit(Entity entity);

        Entity GetByID(string id);

        List<Entity> GetAll();


        void Deserialize();

        void Serialize();

    }
}
