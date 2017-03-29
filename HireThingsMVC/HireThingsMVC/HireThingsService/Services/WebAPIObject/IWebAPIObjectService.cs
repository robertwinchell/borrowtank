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
    public interface IWebApiObjectService : IBaseAsyncService<IWebApiObjectModel>
    {
        Task<IWebApiObjectModel> IndexAsync(HttpContext context, long objectId, CancellationToken cancellationToken);
        Task<IWebApiObjectModel> SaveAsync(HttpContext context, IWebApiObjectModel model, CancellationToken cancellationToken);
    }
}
