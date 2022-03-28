using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Domain.Model.PolozeniePaczki
{
    public class PolozeniePaczki
    {
        public virtual int Id { get; set; }                 // (VO)

        public virtual string Szczegoly { get; set; }       //  (E)

        public virtual IList<Paczka.Paczka> Paczka { get; set; }

        public PolozeniePaczki()
        {
            Paczka = new List<Paczka.Paczka>();
        }
    }
}
