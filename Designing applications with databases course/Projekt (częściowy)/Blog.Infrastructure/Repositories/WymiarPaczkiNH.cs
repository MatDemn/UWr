using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Wymiar;
using Dostawa.Domain.Model.Wymiar.Repositories;
using NHibernate;
using NHibernateHelper;

namespace Dostawa.Infrastructure.Repositories
{
    public class WymiarPaczkiNH : IWymiarRepositoryNH
    {

        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public WymiarPaczkiNH()
        {
            _session = NHHelper.OpenSession();
        }

        public WymiarPaczkiNH(ISession session)
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

        public virtual void Insert(Wymiar o)
        {
            //_session.SaveOrUpdate(o);
            _session.SaveOrUpdate(o);
        }

        public virtual List<Wymiar> FindAll()
        {
            return _session.Query<Wymiar>().ToList();
        }

        public virtual void Delete(int id)
        {
            _session.Delete(_session.Load<Wymiar>(id));
        }

        public virtual Wymiar Find(int id)
        {
            return _session.Load<Wymiar>(id);
        }

    }
}
