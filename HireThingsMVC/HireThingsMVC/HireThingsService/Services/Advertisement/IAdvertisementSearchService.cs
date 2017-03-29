using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IAdvertisementSearchService : IBaseAsyncService<IAdvertisementSearchViewModel>


    {
        Task<IAdvertisementSearchViewModel> SearchAsync(HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken);
        Task<IAdvertisementSearchViewModel> IndexSearchAsync(string searchText, string themeid, string categoryid, HttpContext context, CancellationToken cancellationToken);

    }
}
