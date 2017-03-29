using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class RoleModel: BaseModel, IRoleModel
    {
        public long? RoleId { get; set; }

        [Required(ErrorMessage = "Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description " + ErrorMessage.RequiredMessage)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public int Rank { get; set; }
        public bool IsSystemGenerated { get; set; }
        public List<ISelectListItem> RankList { get; set; }
    }
}
