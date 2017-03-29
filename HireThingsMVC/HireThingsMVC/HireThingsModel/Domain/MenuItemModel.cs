using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class MenuItemModel : BaseModel, IMenuItemModel
    {
       public long ObjectId { get; set; }
       public string ObjectName { get; set; }
       public string Caption { get; set; }
       public string URL { get; set; }
       public long ParentObjectId { get; set; }
       public byte ObjectOrder { get; set; }
       public string ObjectImage { get; set; }
       public int ObjectLevel { get; set; }
    }
}
