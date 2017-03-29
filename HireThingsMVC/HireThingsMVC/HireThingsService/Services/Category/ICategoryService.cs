using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface ICategoryService : IBaseAsyncService<ICategoryModel>
    {
        Task<ICategoryModel> IndexAsync(HttpContext context, string CategoryId, CancellationToken cancellationToken);
        Task<ICategoryModel> SaveAsync(HttpContext context, ICategoryModel model, CancellationToken cancellationToken);

    }
}
