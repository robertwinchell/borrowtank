using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IMenuModel : IBaseModel
    {
        List<IMenuItemModel> ParentMenuList { get; set; }
        List<IMenuItemModel> MenuItemList { get; set; }
    }
}
