using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using FluentNHibernate.Mapping;

namespace Dostawa.Mapping
{
    public class DostawcaMapping : ClassMap<Dostawca>
    {
        public DostawcaMapping()
        {
            Table("Dostawca");
            //public int Id { get; set; }
            //Id(x => x.Id).Column("DostawcaId").GeneratedBy.HiLo("hilo", "nexthi", "10", "tablekey = 'Dostawca'");
            Id(x => x.Id).Column("DostawcaId").GeneratedBy.Identity();

            //public string Nazwa { get; set; }       // Nazwa przewoźnika            (VO)
            Map(x => x.Nazwa).Column("Nazwa").Length(100);
            
            //public string Szczegoly { get; set; }   // Szczegoly odnosnie dostawcy  (E)
            Map(x => x.Szczegoly).Column("Szczegoly").Length(100);

            HasMany(x => x.Paczka).Inverse().Cascade.All();
        }

    }
}
