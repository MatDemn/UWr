using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Worker;
using Dostawa.Domain.Model.Wymiar;
using Dostawa.Mapping;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using Dostawa.Domain.Model.PolozeniePaczki;
using NHibernate.AdoNet.Util;

namespace NHibernateHelper
{
    public class NHHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
            
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure().Diagnostics(m => m.Enable(true))
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Server=localhost;Data Source=(localdb)\MSSQLLocalDB;Database=DostawaD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").ShowSql().FormatSql().AdoNetBatchSize(0))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dostawca>().Add<DostawcaMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Paczka>().Add<PaczkaMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PolozeniePaczki>().Add<PolozeniePaczkiMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Worker>().Add<WorkerMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Wymiar>().Add<WymiarMapping>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true,true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
