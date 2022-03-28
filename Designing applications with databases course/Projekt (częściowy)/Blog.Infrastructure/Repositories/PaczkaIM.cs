using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Paczka.Repositories;
using Dostawa.Domain.Model.PolozeniePaczki;
using Dostawa.Domain.Model.Worker;

namespace Dostawa.Infrastructure.Repositories
{
    public class PaczkaIM : IPaczkaRepository
    {
        private List<Paczka> paczki = new List<Paczka>();

        public PaczkaIM()
        {
            paczki = new List<Paczka> {
                //new Paczka { Id = 101, NumerPaczki = 12345, Adresat = "Jan Kowalski ul. Jesienna Poznań", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 25.6f, IdStatus = 1, IdDostawca = 1, IdWymiary = 2, IdPolozenie = 101, IdWorker = 101 },
                //new Paczka { Id = 102, NumerPaczki = 54321, Adresat = "Adam Janusz ul. Zimowa Wrocław", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 10.6f, IdStatus = 2, IdDostawca = 2, IdWymiary = 1, IdPolozenie = 102, IdWorker = 101 },
                //new Paczka { Id = 103, NumerPaczki = 32123, Adresat = "Jakub Michał ul. Letnia Warszawa", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 5.0f, IdStatus = 3, IdDostawca = 1, IdWymiary = 3, IdPolozenie = 0, IdWorker = 102 },
                //new Paczka { Id = 104, NumerPaczki = 26123, Adresat = "Dariusz Tektura ul. Wiosenna Gdańsk", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 1.5f, IdStatus = 3, IdDostawca = 2, IdWymiary = 1, IdPolozenie = 0, IdWorker = 102 }

                new Paczka { Id = 101, NumerPaczki = 12345, Adresat = "Jan Kowalski ul. Jesienna Poznań", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 25.6f, IdStatus = Status_dostawy.InCompletion, IdDostawca = null, IdWymiary = null, IdPolozenie = null, IdWorker = null },
                new Paczka { Id = 102, NumerPaczki = 54321, Adresat = "Janusz Tracz ul. Zimowa Wrocław", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 10.6f, IdStatus = Status_dostawy.InCompletion, IdDostawca = null, IdWymiary = null, IdPolozenie = null, IdWorker = null },
                new Paczka { Id = 103, NumerPaczki = 32123, Adresat = "Jakub Michał ul. Letnia Warszawa", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 5.0f, IdStatus = Status_dostawy.InCompletion, IdDostawca = null, IdWymiary = null, IdPolozenie = null, IdWorker = null },
                new Paczka { Id = 104, NumerPaczki = 26123, Adresat = "Dariusz Tektura ul. Wiosenna Gdańsk", Nadawca = "Sklep Internetowy 'Taki a taki'", Waga = 1.5f, IdStatus = Status_dostawy.InCompletion, IdDostawca = null, IdWymiary = null, IdPolozenie = null, IdWorker = null }

            };
        }

        public void Insert(Paczka paczka)
        {
            paczki.Add(paczka);
        }

        public void Delete(int id)
        {
            foreach (Paczka p in paczki)
                if (p.Id == id)
                    paczki.Remove(p);
        }

        public Paczka Find(int id)
        {
            foreach (Paczka p in paczki)
                if (p.Id == id)
                    return p;
            return null;
        }

        public List<Paczka> FindAll()
        {
            return paczki;
        }

        public void ModifyDetails(int id, Status_dostawy IdStatus, Dostawca IdDostawca, PolozeniePaczki IdPolozenie, Worker IdWorker)
        {
            Paczka p = Find(id);
            if (IdStatus != p.IdStatus)
                p.IdStatus = IdStatus;
            if (IdDostawca != null)
                p.IdDostawca = IdDostawca;
            if (IdPolozenie != null)
                p.IdPolozenie = IdPolozenie;
            p.IdWorker = IdWorker;
        }
    }
}
