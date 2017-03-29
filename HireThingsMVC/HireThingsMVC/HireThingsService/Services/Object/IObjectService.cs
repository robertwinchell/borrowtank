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
    public interface IObjectService :IBaseAsyncService<IObjectModel>
    {
        Task<IObjectModel> IndexAsync(HttpContext context, long objectId, CancellationToken cancellationToken);
        Task<IObjectModel> SaveAsync(HttpContext context, IObjectModel model, CancellationToken cancellationToken);
    }
}
