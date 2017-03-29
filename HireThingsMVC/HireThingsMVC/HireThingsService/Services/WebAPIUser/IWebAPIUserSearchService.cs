using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IWebApiUserSearchService : IBaseAsyncService<IWebApiUserSearchViewModel>
    {        
        Task<IWebApiUserSearchViewModel> SearchAsync(HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken);
    }
}
