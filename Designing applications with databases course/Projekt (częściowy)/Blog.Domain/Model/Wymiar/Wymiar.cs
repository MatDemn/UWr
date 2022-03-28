using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Domain.Model.Wymiar
{
    public class Wymiar
    {
        public virtual int Id { get; set; }             // VO

        public virtual double Szerokosc { get; set; }   // VO

        public virtual double Wysokosc { get; set; }    // VO

        public virtual double Dlugosc { get; set; }     // VO

        public virtual IList<Paczka.Paczka> Paczka { get; set; }

        public Wymiar()
        {
            Paczka = new List<Paczka.Paczka>();
        }
    }
}
