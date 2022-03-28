using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Domain.Model.Dostawca
{
    public class Dostawca
    {
        public virtual int Id { get; set; }

        public virtual string Nazwa { get; set; }       // Nazwa przewoźnika            (VO)

        public virtual string Szczegoly { get; set; }   // Szczegoly odnosnie dostawcy  (E)

        public virtual IList<Paczka.Paczka> Paczka { get; set; }

        public Dostawca()
        {
            Paczka = new List<Paczka.Paczka>();
        }
    }
}
