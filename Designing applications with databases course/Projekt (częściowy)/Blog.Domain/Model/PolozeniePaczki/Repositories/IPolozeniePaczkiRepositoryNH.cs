using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesComponent;
using Dostawa.Domain.Model.PolozeniePaczki;

namespace Dostawa.Domain.Model.PolozeniePaczki.Repositories
{
    public interface IPolozeniePaczkiRepositoryNH : UtilityComponent<PolozeniePaczki>, IDisposable
    {
        //void Insert(Worker dost);           // Tworzenie nowego 

        //void Delete(int id);                // Usuwanie 

        //Worker Find(int id);                // Znajdowanie
        //void Dispose();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void CloseTransaction();

        void CloseSession();

    }


}
