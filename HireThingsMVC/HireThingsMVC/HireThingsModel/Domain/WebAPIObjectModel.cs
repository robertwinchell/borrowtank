using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class WebApiObjectModel : BaseModel, IWebApiObjectModel
    {
        public long? WAObjectId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "ModuleId " + ErrorMessage.RequiredMessage)]
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string URL { get; set; }
        public bool AllowGet { get; set; }
        public bool AllowPut { get; set; }
        public bool AllowPost { get; set; }
        public bool AllowDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }

        public List<ISelectListItem> ModuleList { get; set; }        
    }
}
