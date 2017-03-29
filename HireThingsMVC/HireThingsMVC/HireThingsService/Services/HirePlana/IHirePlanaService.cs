using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IHirePlanaService : IBaseAsyncService<IHirePlanaModel>


    {
        Task<IHirePlanaModel> SaveAsync(HttpContext context, IHirePlanaModel model, CancellationToken cancellationToken);
        Task<IHirePlanaModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string HirePlanaId);
    }
}
