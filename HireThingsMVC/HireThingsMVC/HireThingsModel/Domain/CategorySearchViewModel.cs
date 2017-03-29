using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class CategorySearchViewModel : BaseModel , ICategorySearchViewModel
    {
        public List<ICategoryModel> CategoryList { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }     
    }
}
