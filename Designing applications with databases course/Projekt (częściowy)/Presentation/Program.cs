using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Application;
using Dostawa.Domain.Model.Dostawca;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.PolozeniePaczki;
using Dostawa.Domain.Model.Worker;
using Dostawa.Domain.Model.Wymiar;
using NHibernateHelper;

namespace Presentation.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dostawca d = new Dostawca()
            {
                Nazwa = "Ten tamten",
                Szczegoly = "Drugi z tamtych"
            };

            PolozeniePaczki pol = new PolozeniePaczki()
            {
                Szczegoly = "Tam i tutaj od tego"
            };

            Wymiar wym = new Wymiar()
            {
                Dlugosc = 9f,
                Szerokosc = 9f,
                Wysokosc = 9f
            };

            Worker work = new Worker()
            {
                Login = "Pracownik111",
                Password = "Tehaslo"
            };

            Paczka p = new Paczka()
            {
                NumerPaczki = 1232111,
                Adresat = "Wojtek Testowy",
                Nadawca = "Market Mrowka",
                Waga = 13.0f,
                IdStatus = Status_dostawy.InCompletion
        };

            p.IdDostawca = d;
            p.IdPolozenie = pol;
            p.IdWymiary = wym;
            p.IdWorker = work;

            using (PaczkaModifyService pmodserv = new PaczkaModifyService())
            {
                
                
                try
                {
                    pmodserv.BeginTransaction();
                    pmodserv.CreateDostawca(d);
                    
                    pmodserv.CommitTransaction();
                }
                catch
                {
                    pmodserv.RollbackTransaction();
                }

            }
            Console.ReadKey();

            /*
            using (PaczkaModifyService pmodserv = new PaczkaModifyService())
            {
                pmodserv.BeginTransaction();
                var pack = pmodserv.GetPaczki();
                foreach (Paczka pa in pack)
                {
                    Console.WriteLine($"{pa.Nadawca} {pa.Adresat} {pa.Id} {pa.IdWorker.Login}");
                }
                
                pmodserv.CommitTransaction();
                Console.ReadKey();
            }*/
        }
    }
}

