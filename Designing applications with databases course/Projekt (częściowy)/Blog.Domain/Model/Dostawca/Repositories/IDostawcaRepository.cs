using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesComponent;

namespace Dostawa.Domain.Model.Dostawca.Repositories
{
    public interface IDostawcaRepository : UtilityComponent<Dostawca>
    {
        //void Insert(Dostawca dost);     // Wstawianie nowych dostawcow

        void Change(int id, string dane);       // Zmienianie szczegolow dostawcow

        //void Delete(int id);            // Usuwane dostawcy

        //Dostawca Find(int id);          // Wyszukiwanie dostawcy wedlug podanego id

        List<Dostawca> FindAll();       // Wyszukanie wszystkich dostawcow
    }
}
