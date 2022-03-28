using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Worker;
using Dostawa.Domain.Model.Worker.Repositories;
using NHibernate;
using NHibernateHelper;

namespace Dostawa.Infrastructure.Repositories
{
    public class WorkerNH : IWorkerRepositoryNH
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public WorkerNH()
        {
            _session = NHHelper.OpenSession();
        }

        public WorkerNH(ISession session)
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

        public void ChangePass(int id, string newpass)
        {
            _session.Load<Worker>(id).Password = newpass;
        }

        public virtual void Insert(Worker o)
        {
            //_session.SaveOrUpdate(o);
            _session.SaveOrUpdate(o);
        }

        public virtual List<Worker> FindAll()
        {
            return _session.Query<Worker>().ToList();
        }

        public virtual void Delete(int id)
        {
            _session.Delete(_session.Load<Worker>(id));
        }

        public virtual Worker Find(int id)
        {
            return _session.Load<Worker>(id);
        }
    }
}
