using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IBaseAsyncService<T>
    {
        Task<T> IndexAsync(HttpContext context, CancellationToken cancellationToken);    // Change Datatype to HttpContext
        Task<string> SelectCurrentDateTimeLocalizedAsync(System.Web.HttpContext context, long userId, CancellationToken cancellationToken);
        }
}

