using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
   public interface ILocationSearchViewModel : IBaseModel
    {
       List<ILocationModel> LocationList { get; set; }
       List<ISelectListItem> DDLCountry{ get; set; }
       List<ISelectListItem> DDLLocation { get; set; }
       List<ISelectListItem> DDLSearch { get; set; }
     
       string TxtSearch { get; set; }        
    }
}

