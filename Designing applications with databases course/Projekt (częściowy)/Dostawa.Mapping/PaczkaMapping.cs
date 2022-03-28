using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Wymiar;
using FluentNHibernate.Mapping;
using NHibernate.Engine;

namespace Dostawa.Mapping
{
    public class PaczkaMapping : ClassMap<Paczka>
    {
        public PaczkaMapping()
        {
            Table("Paczka");
            //public int Id { get; set; }             // VO
            //Id(x => x.Id).Column("PaczkaId").GeneratedBy.HiLo("hilo", "nexthi", "10", "tablekey = 'Paczka'");
            Id(x => x.Id).Column("PaczkaId").GeneratedBy.Identity();

            //public int NumerPaczki { get; set; }    // VO
            Map(x => x.NumerPaczki).Column("Numer").Length(int.MaxValue);

            //public string Adresat { get; set; }     // VO
            Map(x => x.Adresat).Column("Adresat").Length(int.MaxValue);

            //public string Nadawca { get; set; }     // VO
            Map(x => x.Nadawca).Column("Nadawca").Length(int.MaxValue);

            //public float Waga { get; set; }       // VO
            Map(x => x.Waga).Column("Waga").Length(int.MaxValue);

            //public int IdStatus { get; set; }       // E
            Map(x => x.IdStatus).Column("StatusId").CustomType<Status_dostawy>();

            //public int IdDostawca { get; set; }     // E
            //References(x => x.IdDostawca).Column("DostawcaId");
            References(x => x.IdDostawca).Column("DostawcaId");

            //public int IdWymiary { get; set; }      // VO
            //References(x => x.IdWymiary).Column("WymiaryId");
            References(x => x.IdWymiary).Column("WymiaryId");

            //public int IdPolozenie { get; set; }    // E
            //References(x => x.IdPolozenie).Column("PolozenieId");
            References(x => x.IdPolozenie).Column("PolozenieId");

            //public int IdWorker { get; set; }       // E
            //References(x => x.IdWorker).Column("WorkerId");
            References(x => x.IdWorker).Column("WorkerId");


        }
    }
}
