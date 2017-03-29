using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class MenuModel : BaseModel, IMenuModel
    {


        public List<IMenuItemModel> MenuItemList { get; set; }
        public List<IMenuItemModel> ParentMenuList { get; set; }
    }

}
