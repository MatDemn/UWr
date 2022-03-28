using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesComponent;

namespace Dostawa.Domain.Model.Worker.Repositories
{
    public interface IWorkerRepositoryNH : UtilityComponent<Worker>, IDisposable
    {
        //void Insert(Worker dost);           // Tworzenie nowego konta pracownika

        void ChangePass(int id, string newpass);    // Zmiana hasla na nowe

        //void Delete(int id);                // Usuwanie pracownika

        //Worker Find(int id);                // Znajdowanie pracownika

        //void Dispose();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void CloseTransaction();

        void CloseSession();


    }
}
