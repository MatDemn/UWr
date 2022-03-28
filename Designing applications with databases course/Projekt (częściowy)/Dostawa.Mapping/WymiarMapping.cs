using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Wymiar;
using FluentNHibernate.Mapping;

namespace Dostawa.Mapping
{
    public class WymiarMapping : ClassMap<Wymiar>
    {
        public WymiarMapping()
        {
            Table("Wymiar");
            //Id(x => x.Id).Column("WymiarId").GeneratedBy.HiLo("hilo", "nexthi", "10", "tablekey = 'Wymiar'");
            Id(x => x.Id).Column("WymiarId").GeneratedBy.Identity();

            Map(x => x.Szerokosc).Column("Szerokosc").Length(100);

            Map(x => x.Wysokosc).Column("Wysokosc").Length(100);

            Map(x => x.Dlugosc).Column("Dlugosc").Length(100);

            HasMany(x => x.Paczka).Inverse().Cascade.All();
        }
    }
}
