using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IEmailServerService : IBaseAsyncService<IEmailServerModel>
    {
        Task<IEmailServerModel> IndexAsync(HttpContext context, string ServerId, CancellationToken cancellationToken);
        Task<IEmailServerModel> SaveAsync(HttpContext context, IEmailServerModel model, CancellationToken cancellationToken);
    }
}
