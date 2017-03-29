using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface ILocationService : IBaseAsyncService<ILocationModel>
    {
        Task<ILocationModel> IndexAsync(HttpContext context, string orgId, CancellationToken cancellationToken);
        Task<ILocationModel> SaveAsync(HttpContext context, ILocationModel model, CancellationToken cancellationToken);
    }
}
