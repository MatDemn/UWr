using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Paczka.Repositories;
using Dostawa.Domain.Model.PolozeniePaczki;
using Dostawa.Domain.Model.Worker;
using NHibernate;
using NHibernateHelper;

namespace Dostawa.Infrastructure.Repositories
{
    public class PaczkaNH : IPaczkaRepositoryNH
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public virtual void Insert(Paczka o)
        {
            //_session.SaveOrUpdate(o);
            _session.SaveOrUpdate(o);
        }

        public virtual void Change(int id, string dane)
        {
            (_session.Load<Dostawca>(id)).Szczegoly = dane;
        }

        public virtual List<Paczka> FindAll()
        {
            return _session.Query<Paczka>().ToList();
        }

        public virtual void Delete(int id)
        {
            _session.Delete(_session.Load<Paczka>(id));
        }

        public virtual Paczka Find(int id)
        {
            return _session.Load<Paczka>(id);
        }

        public void ModifyDetails(int id, Status_dostawy IdStatus, Dostawca IdDostawca, PolozeniePaczki IdPolozenie, Worker IdWorker)
        {
            Paczka pack = _session.Load<Paczka>(id);
            pack.IdStatus = IdStatus;
            pack.IdDostawca = IdDostawca;
            pack.IdPolozenie = IdPolozenie;
            pack.IdWorker = IdWorker;
        }

        public PaczkaNH()
        {
            _session = NHHelper.OpenSession();
        }

        public PaczkaNH(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();

            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        public void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        public void CloseSession()
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                CommitTransaction();
            }
            if (_session != null)
            {
                _session.Flush();
                CloseSession();
            }
        }
    }
}
