using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IWebApiObjectSearchService : IBaseAsyncService<IWebApiObjectSearchViewModel>
    {
      Task<IWebApiObjectSearchViewModel> SearchAsync(HttpContext context, ISearchModel model, CancellationToken cancellationToken);
    }
}
