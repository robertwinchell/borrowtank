using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IUserSearchViewModel:IBaseModel
    {
        List<IHireThingsUser> UserList { get; set; }
        long LocationId { get; set; }
        List<ISelectListItem> DDLCountry { get; set; }
        long CountryId { get; set; }
        List<ISelectListItem> DDLLocation { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }
    }
}
