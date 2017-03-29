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
    public interface IRoleObjectService : IBaseAsyncService<IRoleObjectViewModel>
    {
        Task<IRoleObjectViewModel> IndexAsync(HttpContext context, long roleId, SearchModel searchModel, CancellationToken cancellationToken);
        Task<IRoleObjectViewModel> SaveAsync(HttpContext context, IRoleObjectViewModel model, CancellationToken cancellationToken);
    }
}
