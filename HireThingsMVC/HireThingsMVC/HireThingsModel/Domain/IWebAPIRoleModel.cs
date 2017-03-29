using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IWebApiRoleModel : IBaseModel
    {
        long? WARoleId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        int Rank { get; set; }
        bool IsDeleted { get; set; }
        long DeleteUserId { get; set; }
        DateTime DeleteDate { get; set; }
        string DeleteReason { get; set; }
        bool IsSystemGenerated { get; set; }
        List<ISelectListItem> RankList { get; set; }
    }
}
