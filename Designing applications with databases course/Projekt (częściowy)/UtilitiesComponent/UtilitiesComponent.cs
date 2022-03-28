using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesComponent
{
    public interface UtilityComponent<T>
    {
        void Insert(T o);
        void Delete(int id);
        T Find(int id);
        List<T> FindAll();
    }
}
