using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Dostawca.Repositories;

namespace Dostawa.Infrastructure.Repositories
{
    class DostawcaIM : IDostawcaRepository
    {
        private List<Dostawca> dostawcy = new List<Dostawca>();

        public DostawcaIM()
        {
            dostawcy = new List<Dostawca>
            {
                new Dostawca { Id = 1, Nazwa = "DPD", Szczegoly="K46A012"},
                new Dostawca { Id = 2, Nazwa = "UPS", Szczegoly="UAGWA2456D21"}
            };
        }

        public void Insert(Dostawca dost)
        {
            dostawcy.Add(dost);
        }

        public void Delete(int id)
        {
            foreach (var d in dostawcy)
                if (d.Id == id)
                    dostawcy.Remove(d);
        }

        public Dostawca Find(int id)
        {
            foreach (var d in dostawcy)
                if (d.Id == id)
                    return d;
            return null;
        }

        public List<Dostawca> FindAll()
        {
            return dostawcy;
        }

        public void Change(int id, string s)
        {
            foreach (var d in dostawcy)
                if (d.Id == id) { 
                    d.Szczegoly = s;
                    return;
                }
        }
    }
}
