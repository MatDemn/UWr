using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.PolozeniePaczki;
using Dostawa.Domain.Model.PolozeniePaczki.Repositories;
using NHibernate;
using NHibernateHelper;

namespace Dostawa.Infrastructure.Repositories
{
    public class PolozeniePaczkiNH : IPolozeniePaczkiRepositoryNH
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public PolozeniePaczkiNH()
        {
            _session = NHHelper.OpenSession();
        }

        public PolozeniePaczkiNH(ISession session)
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

        public virtual void Insert(PolozeniePaczki o)
        {
            //_session.SaveOrUpdate(o);
            _session.SaveOrUpdate(o);
        }

        public virtual List<PolozeniePaczki> FindAll()
        {
            return _session.Query<PolozeniePaczki>().ToList();
        }

        public virtual void Delete(int id)
        {
            _session.Delete(_session.Load<PolozeniePaczki>(id));
        }

        public virtual PolozeniePaczki Find(int id)
        {
            return _session.Load<PolozeniePaczki>(id);
        }
    }
}
