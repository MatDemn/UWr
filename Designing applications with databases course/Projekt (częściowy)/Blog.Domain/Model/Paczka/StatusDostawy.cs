using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dostawa.Domain.Model.Paczka
{
    public enum Status_dostawy
    {
        InCompletion = 1,
        Ready = 2,
        InTravel = 3,
        Delivered = 4
    }

    public class Status_dostawyType : NHibernate.Type.EnumStringType<Status_dostawy>
    {
        public static string GetDescription(Status_dostawy status)
        {
            switch(status)
            {
                case Status_dostawy.InCompletion:    return "Kompletowanie";
                case Status_dostawy.Ready:           return "Gotowe do wysłania";
                case Status_dostawy.InTravel:        return "W podróży";
                case Status_dostawy.Delivered:       return "Dostarczona";
                default: return string.Empty;
            }
        }
    }
}
