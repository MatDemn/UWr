using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesComponent;

namespace Dostawa.Domain.Model.Paczka.Repositories
{
    public interface IPaczkaRepository : UtilityComponent<Paczka>
    {
        //void Insert(Paczka paczka);

        //void Delete(int id);

        //Paczka Find(int id);

        void ModifyDetails(int id, Status_dostawy IdStatus, Dostawca.Dostawca IdDostawca, PolozeniePaczki.PolozeniePaczki IdPolozenie, Worker.Worker IdWorker);

        List<Paczka> FindAll();
    }
}
