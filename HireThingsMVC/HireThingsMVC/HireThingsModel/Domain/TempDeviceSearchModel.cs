using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
   public class TempDeviceSearchModel: BaseModel, ITempDeviceSearchModel
    {   
        public List<ITempDeviceModel> TempDeviceList { get; set; }
        public long SystemId { get; set; }
        public List<ISelectListItem> DDLSystem { get; set; }
        public long OrganizationId { get; set; }
        public List<ISelectListItem> DDLOrganization { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }
    }
}
