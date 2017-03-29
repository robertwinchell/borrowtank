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
    public interface IWebApiRoleService : IBaseAsyncService<IWebApiRoleModel>
    {
        Task<IWebApiRoleModel> IndexAsync(HttpContext context, long roleId, CancellationToken cancellationToken);
        Task<IWebApiRoleModel> SaveAsync(HttpContext context, IWebApiRoleModel model, CancellationToken cancellationToken);
    }
}
