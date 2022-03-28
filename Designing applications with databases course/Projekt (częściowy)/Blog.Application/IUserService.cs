using Dostawa.Domain.Model.Paczka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Application
{
    public interface IUserService
    {
        Paczka searchForPack(int ident);
    }
}
