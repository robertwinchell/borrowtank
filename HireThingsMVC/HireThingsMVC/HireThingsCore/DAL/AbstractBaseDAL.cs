using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public abstract class AbstractBaseDAL<T> : DBConnection, IBaseAsyncDAL<T>
    {
        abstract public Task<Int64> SaveAsync(T model, CancellationToken cancellationToken);
        abstract public Task<bool> UpdateAsync(T model, CancellationToken cancellationToken);
        abstract public Task<T> SelectByIDAsync(Int64 id, Int64 userId, CancellationToken cancellationToken);
        abstract public Task<List<T>> SelectAllAsync(CancellationToken cancellationToken);
        abstract public Task<List<T>> SelectAllByFilterAsync(IPredicate filterList, CancellationToken cancellationToken, IPageInfo pageInfo = null);
       
    }
}
