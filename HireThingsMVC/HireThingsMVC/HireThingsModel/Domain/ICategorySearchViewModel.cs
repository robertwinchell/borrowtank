using System;
using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
     public interface ICategorySearchViewModel:IBaseModel
    {

         List<ICategoryModel> CategoryList { get; set; }
         long SearchId { get; set; }
         List<ISelectListItem> DDLSearch { get; set; }
         string TxtSearch { get; set; }
    }
}
