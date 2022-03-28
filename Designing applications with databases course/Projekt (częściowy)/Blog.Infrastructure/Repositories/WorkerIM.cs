using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dostawa.Domain.Model.Worker;
using Dostawa.Domain.Model.Worker.Repositories;

namespace Dostawa.Infrastructure.Repositories
{
    class WorkerIM : IWorkerRepository
    {
        private List<Worker> workers = new List<Worker>();

        public WorkerIM()
        {
            workers = new List<Worker>
            {
                new Worker { Id = 101, Login = "Admin", Password="Adm1nPa55"},
                new Worker { Id = 102, Login = "KJarzyna", Password="zeSzczecina"}
            };
        }

        public void Insert(Worker w)
        {
            workers.Add(w);
        }

        public void Delete(int id)
        {
            foreach (var w in workers)
                if (w.Id == id)
                    workers.Remove(w);
        }

        public Worker Find(int id)
        {
            foreach (var w in workers)
                if (w.Id == id)
                    return w;
            return null;
        }

        public List<Worker> FindAll()
        {
            return workers;
        }

        public void ChangePass(int id, string s)
        {
            foreach (var w in workers)
                if (w.Id == id)
                {
                    w.Password = s;
                    return;
                }
        }
    }
}
