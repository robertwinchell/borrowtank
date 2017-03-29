using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
   public class UserSearchViewModel: BaseModel, IUserSearchViewModel
    {   
        public List<IHireThingsUser> UserList { get; set; }
        public long LocationId { get; set; }
        public List<ISelectListItem> DDLCountry { get; set; }
        public long CountryId { get; set; }
        public List<ISelectListItem> DDLLocation { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }
    }
}
