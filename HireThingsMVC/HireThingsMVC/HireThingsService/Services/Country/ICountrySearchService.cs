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
    public interface ICountrySearchService : IBaseAsyncService<ICountrySearchViewModel>
    {
        //ISystemSearchViewModel Search(HttpContext context,ISearchModel model);
        Task<ICountrySearchViewModel> SearchAsync(HttpContext context, CancellationToken cancellationToken, ISearchModel model);
    }
}
