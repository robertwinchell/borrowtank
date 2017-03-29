using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IEmailServerSearchService : IBaseAsyncService<IEmailServerSearchViewModel>
    {
        Task<IEmailServerSearchViewModel> SearchAsync(HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken);
    }
}
