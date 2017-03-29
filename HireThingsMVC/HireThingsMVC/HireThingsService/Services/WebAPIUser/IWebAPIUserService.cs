using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IWebApiUserService : IBaseAsyncService<IWebApiUser>
    {
        Task<IWebApiUser> IndexAsync(HttpContext context, long UserId, CancellationToken cancellationToken);
        //Task<IHireThingsUser> IndexAsync(HttpContext context, IWebApiUser model, CancellationToken cancellationToken);
        Task<IWebApiUser> SaveAsync(HttpContext context, IWebApiUser model, CancellationToken cancellationToken);
    }
}
