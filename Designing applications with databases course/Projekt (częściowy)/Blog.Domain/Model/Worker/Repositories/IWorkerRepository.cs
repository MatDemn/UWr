using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesComponent;

namespace Dostawa.Domain.Model.Worker.Repositories
{
    public interface IWorkerRepository : UtilityComponent<Worker>
    {
        //void Insert(Worker dost);           // Tworzenie nowego konta pracownika

        void ChangePass(int id, string newpass);    // Zmiana hasla na nowe

        //void Delete(int id);                // Usuwanie pracownika

        //Worker Find(int id);                // Znajdowanie pracownika

    }
}
