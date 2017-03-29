using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public interface IBaseAsyncDAL<T>
    {
        Task<Int64> SaveAsync(T model, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T model, CancellationToken cancellationToken);
        Task<T> SelectByIDAsync(Int64 id, Int64 userId, CancellationToken cancellationToken);
        Task<List<T>> SelectAllAsync( CancellationToken cancellationToken);
        Task<List<T>> SelectAllByFilterAsync(IPredicate filterList, CancellationToken cancellationToken, IPageInfo pageInfo = null);
    }
}
