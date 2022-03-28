using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Wymiar;

namespace Dostawa.Domain.Model.Paczka
{
    public class Paczka
    {
        public virtual int Id { get; set; }             // VO

        public virtual int NumerPaczki { get; set; }    // VO

        public virtual string Adresat { get; set; }     // VO

        public virtual string Nadawca { get; set; }     // VO

        public virtual float Waga { get; set; }       // VO

        public virtual Status_dostawy IdStatus { get; set; }       // E

        public virtual Dostawca.Dostawca IdDostawca { get; set; }     // E

        public virtual Wymiar.Wymiar IdWymiary { get; set; }      // VO

        public virtual PolozeniePaczki.PolozeniePaczki IdPolozenie { get; set; }    // E

        public virtual Worker.Worker IdWorker { get; set; }       // E
    }
}
