using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface ILocationSearchService : IBaseAsyncService<ILocationSearchViewModel>
    {
        //ILocationSearchViewModel Search(HttpContext context, ISearchModel model);

        Task<ILocationSearchViewModel> SearchAsync(HttpContext context, ISearchModel model, CancellationToken cancellationToken);
    }
}
