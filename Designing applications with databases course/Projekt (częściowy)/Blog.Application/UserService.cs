using Dostawa.Infrastructure.Repositories;
using Dostawa.Domain.Model.Paczka;
using Dostawa.Domain.Model.Paczka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Application
{
    public class UserService : IUserService, IDisposable
    {
        private IPaczkaRepositoryNH paczkaRepository;

        public UserService()
        {
            paczkaRepository = new PaczkaNH();
        }
        public UserService(IPaczkaRepositoryNH repo)
        {
            paczkaRepository = repo;
        }

        public Paczka searchForPack(int ident)
        {
            return paczkaRepository.Find(ident);
        }

        public void Dispose()
        {
            paczkaRepository.Dispose();
        }
    }
}
