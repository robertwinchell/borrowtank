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
    public interface IRoleService : IBaseAsyncService<IRoleModel>
    {
        Task<IRoleModel> IndexAsync(HttpContext context, long roleId, CancellationToken cancellationToken);
        Task<IRoleModel> SaveAsync(HttpContext context, IRoleModel model, CancellationToken cancellationToken);
    }
}
