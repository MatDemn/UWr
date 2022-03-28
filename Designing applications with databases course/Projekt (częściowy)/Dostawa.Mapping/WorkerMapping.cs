using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Worker;
using FluentNHibernate.Mapping;

namespace Dostawa.Mapping
{
    public class WorkerMapping : ClassMap<Worker>
    {
        public WorkerMapping()
        {
            Table("Worker");
            //public int Id { get; set; }             // VO
            //Id(x => x.Id).Column("WorkerId").GeneratedBy.HiLo("hilo", "nexthi", "10", "tablekey = 'Worker'");
            Id(x => x.Id).Column("WorkerId").GeneratedBy.Identity();

            //public string Login { get; set; }       // VO
            Map(x => x.Login).Column("Login").Length(100);

            //public string Password { get; set; }    // E
            Map(x => x.Password).Column("Password").Length(100);

            HasMany(x => x.Paczka).Inverse().Cascade.All();

        }
    }
}
