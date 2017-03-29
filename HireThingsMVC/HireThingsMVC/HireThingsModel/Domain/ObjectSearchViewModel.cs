using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class ObjectSearchViewModel : BaseModel, IObjectSearchViewModel
    {
       public List<IObjectModel> ObjectList { get; set; }
       public long SearchId { get; set; }
       public List<ISelectListItem> DDLSearch { get; set; }
       public string TxtSearch { get; set; }  
    }
}
