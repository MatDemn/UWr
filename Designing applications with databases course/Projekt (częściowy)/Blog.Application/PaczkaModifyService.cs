using Dostawa.Infrastructure.Repositories;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Paczka.Repositories;
using Dostawa.Domain.Model.Worker;
using Dostawa.Domain.Model.PolozeniePaczki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca.Repositories;
using Dostawa.Domain.Model.PolozeniePaczki.Repositories;
using Dostawa.Domain.Model.Worker.Repositories;
using Dostawa.Domain.Model.Wymiar.Repositories;
using Dostawa.Domain.Model.Wymiar;

namespace Dostawa.Application
{
    public class PaczkaModifyService: IPaczkaModifyService, IDisposable
    {
        private IPaczkaRepositoryNH paczkaRepository;

        private IDostawcaRepositoryNH dostawcaRepository;

        private IPolozeniePaczkiRepositoryNH polozenieRepository;

        private IWorkerRepositoryNH workerRepository;

        private IWymiarRepositoryNH wymiarRepository;

        public PaczkaModifyService()
        {
            paczkaRepository = new PaczkaNH();
            dostawcaRepository = new DostawcaNH();
            polozenieRepository = new PolozeniePaczkiNH();
            workerRepository = new WorkerNH();
            wymiarRepository = new WymiarPaczkiNH();
        }

        public PaczkaModifyService(IPaczkaRepositoryNH repo)
        {
            paczkaRepository = repo;
        }

        public IList<Paczka> GetPaczki()
        {
            return paczkaRepository.FindAll();
        }

        public void CreatePaczka(Paczka p)
        {
            paczkaRepository.Insert(p);
        }

        public void CreateDostawca(Dostawca d)
        {
            dostawcaRepository.Insert(d);
        }

        public void CreatePolozenie(PolozeniePaczki p)
        {
            polozenieRepository.Insert(p);
        }

        public void CreateWorker(Worker w)
        {
            workerRepository.Insert(w);
        }

        public void CreateWymiar(Wymiar w)
        {
            wymiarRepository.Insert(w);
        }

        public void DeletePaczka(int id)
        {
            paczkaRepository.Delete(id);
        }

        public void ChangeDetails(int id, Status_dostawy IdStatus, Dostawca IdDostawca, PolozeniePaczki IdPolozenie, Worker IdWorker)
        {
            paczkaRepository.ModifyDetails(id, IdStatus, IdDostawca, IdPolozenie, IdWorker);
        }

        public void Dispose()
        {
            paczkaRepository.Dispose();
            dostawcaRepository.Dispose();
            polozenieRepository.Dispose();
            workerRepository.Dispose();
            wymiarRepository.Dispose();
    }

        public void BeginTransaction()
        {
            paczkaRepository.BeginTransaction();
            dostawcaRepository.BeginTransaction();
            polozenieRepository.BeginTransaction();
            workerRepository.BeginTransaction();
            wymiarRepository.BeginTransaction();
        }

        public void CommitTransaction()
        {
            paczkaRepository.CommitTransaction();
            dostawcaRepository.CommitTransaction();
            polozenieRepository.CommitTransaction();
            workerRepository.CommitTransaction();
            wymiarRepository.CommitTransaction();
            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            paczkaRepository.RollbackTransaction();
            dostawcaRepository.RollbackTransaction();
            polozenieRepository.RollbackTransaction();
            workerRepository.RollbackTransaction();
            wymiarRepository.RollbackTransaction();
        }

        private void CloseTransaction()
        {
            paczkaRepository.Dispose();
            dostawcaRepository.Dispose();
            polozenieRepository.Dispose();
            workerRepository.Dispose();
            wymiarRepository.Dispose();
        }

        private void CloseSession()
        {
            paczkaRepository.CloseSession();
            paczkaRepository.Dispose();
            paczkaRepository = null;

            dostawcaRepository.CloseSession();
            dostawcaRepository.Dispose();
            dostawcaRepository = null;

            polozenieRepository.CloseSession();
            polozenieRepository.Dispose();
            polozenieRepository = null;

            workerRepository.CloseSession();
            workerRepository.Dispose();
            workerRepository = null;

            wymiarRepository.CloseSession();
            wymiarRepository.Dispose();
            wymiarRepository = null;
        }
    }
}
