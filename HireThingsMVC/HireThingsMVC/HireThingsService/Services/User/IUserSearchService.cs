﻿using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IUserSearchService : IBaseAsyncService<IUserSearchViewModel>
    {        
        Task<IUserSearchViewModel> SearchAsync(HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken);
    }
}
