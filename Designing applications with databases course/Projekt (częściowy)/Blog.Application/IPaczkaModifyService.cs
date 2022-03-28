using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.PolozeniePaczki;
using Dostawa.Domain.Model.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Application
{
    public interface IPaczkaModifyService
    {
        IList<Paczka> GetPaczki();

        void CreatePaczka(Paczka p);

        void DeletePaczka(int id);

        void ChangeDetails(int id, Status_dostawy IdStatus, Dostawca IdDostawca, PolozeniePaczki IdPolozenie, Worker IdWorker);
    }
}
