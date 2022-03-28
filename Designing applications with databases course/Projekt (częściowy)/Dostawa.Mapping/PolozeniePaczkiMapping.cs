using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.PolozeniePaczki;
using FluentNHibernate.Mapping;

namespace Dostawa.Mapping
{
    public class PolozeniePaczkiMapping : ClassMap<PolozeniePaczki>
    {
        public PolozeniePaczkiMapping()
        {
            Table("Polozenie");
            //Id(x => x.Id).Column("PolozenieId").GeneratedBy.HiLo("hilo", "nexthi", "10", "tablekey = 'Polozenie'");
            Id(x => x.Id).Column("PolozenieId").GeneratedBy.Identity();

            Map(x => x.Szczegoly).Column("Szczegoly").Length(100);

            HasMany(x => x.Paczka).Inverse().Cascade.All();

        }
        

    }
}
