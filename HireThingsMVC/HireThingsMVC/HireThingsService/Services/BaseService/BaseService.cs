using MEPS.MEPSTemp.Model;
using System.Web;


namespace MEPS.MEPSTemp.Service
{
    public abstract class BaseService<T> : IBaseService
    {
        public abstract T Index(HttpContext context);        
    }
}
