using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IItemDeleteService : IBaseAsyncService<IItemDeleteModel>
    {
        Task<IItemDeleteModel> IndexAsync(HttpContext context, long ItemDeleteId, CancellationToken cancellationToken);
        Task<IItemDeleteModel> SaveAsync(HttpContext context, IItemDeleteModel model, CancellationToken cancellationToken);
    }
}
