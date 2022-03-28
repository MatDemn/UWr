using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Dostawca.Repositories;
using NHibernate;
using NHibernateHelper;

namespace Dostawa.Infrastructure.Repositories
{
    public class DostawcaNH : IDostawcaRepositoryNH
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public DostawcaNH()
        {
            _session = NHHelper.OpenSession();
        }

        public DostawcaNH(ISession session)
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

        public virtual void Insert(Dostawca o)
        {
            //_session.SaveOrUpdate(o);
            _session.SaveOrUpdate(o);
        }

        public virtual void Change(int id, string dane)
        {
            (_session.Load<Dostawca>(id)).Szczegoly = dane;
        }

        public virtual List<Dostawca> FindAll()
        {
            return _session.Query<Dostawca>().ToList();
        }

        public virtual void Delete(int id)
        {
            _session.Delete(_session.Load<Dostawca>(id));
        }

        public virtual object GetById(Type objType, object objId)
        {
            return _session.Load(objType, objId);
        }

        public virtual Dostawca Find(int id)
        {
            return _session.Load<Dostawca>(id);
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
