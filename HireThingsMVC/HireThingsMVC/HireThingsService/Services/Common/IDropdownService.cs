using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public interface IDropdownService : IBaseAsyncService<IDropdownModel>
    {
        Task<IDropdownModel> PopulateLocationDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken);
        Task<IDropdownModel> PopulateParentObjectDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken);
        Task<IDropdownModel> PopulateParentObjectLevelDropDownListAsync(IDropdownModel model, CancellationToken cancellationToken);
    }
}
