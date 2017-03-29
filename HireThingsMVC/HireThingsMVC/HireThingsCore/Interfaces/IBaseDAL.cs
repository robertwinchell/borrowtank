using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;

namespace ASOL.HireThings.Core
{
    public interface IBaseDAL<T>
    {
        Int64 Save(T model);
        bool Update(T model);
        T SelectByID(Int64 id, Int64 userId);
        List<T> SelectAll();
        List<T> SelectAllByFilter(IPredicate filterList, IPageInfo pageInfo = null);
    }
}
