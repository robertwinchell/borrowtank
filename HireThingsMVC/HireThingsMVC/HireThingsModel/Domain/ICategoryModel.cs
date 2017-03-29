using System;
using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface ICategoryModel:IBaseModel
    {

         long? CategoryId { get; set; }
         long? ParentCategoryId { get; set; }

        
         long ThemeId { get; set; }
         string CategoryName { get; set; }

         string CategorySerialNo { get; set; }

         string CategoryDesc { get; set; }


         long AddUserId { get; set; }
         DateTime AddDate { get; set; }
         long UpdateUserId { get; set; }
         DateTime UpdateDate { get; set; }
         string Theme { get; set; }
        bool ShowOnHomepage { get; set; }
         bool IsActive { get; set; }
        List<ISelectListItem> ThemeList { get; set; }
        
    }
}
