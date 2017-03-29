using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IMenuItemModel : IBaseModel
    {
       long ObjectId { get; set; }
       string ObjectName { get; set; }
       string Caption { get; set; }
       string URL { get; set; }
       long ParentObjectId { get; set; }
       byte ObjectOrder { get; set; }
       string ObjectImage { get; set; }
       int ObjectLevel { get; set; }
    }
}
