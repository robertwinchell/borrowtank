using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;



namespace ASOL.HireThings.Service
{
    public interface IBaseService<T>
    {
        T Index(HttpContext context);    // Change Datatype to HttpContext
        Task<T> ExportGridAsync(HttpContext context, ISearchModel model, CancellationToken cancellationToken);
        
    }
}
