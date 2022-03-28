using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Domain.Model.Worker
{
    public class Worker
    {
        public virtual int Id { get; set; }             // VO

        public virtual string Login { get; set; }       // VO

        public virtual string Password { get; set; }    // E

        public virtual IList<Paczka.Paczka> Paczka { get; set; }

        public Worker()
        {
            Paczka = new List<Paczka.Paczka>();
        }
    }
}
