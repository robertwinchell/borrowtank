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
    public interface IWebAPIRoleObjectService : IBaseAsyncService<IWebAPIRoleObjectViewModel>
    {
        Task<IWebAPIRoleObjectViewModel> IndexAsync(HttpContext context, long roleId, SearchModel searchModel, CancellationToken cancellationToken);
        Task<IWebAPIRoleObjectViewModel> SaveAsync(HttpContext context, IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken);
    }
}
