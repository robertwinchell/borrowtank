using System;
using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IHirePlanaModel:IBaseModel
    {
        long HirePlanaId { get; set; }
        string CartId { get; set; }
        string Name { get; set; }
        List<ICartModel> CartList { get; set; }
        DateTime AddDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
