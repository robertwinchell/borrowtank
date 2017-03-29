using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class ObjectModel : BaseModel, IObjectModel
    {
        public long? ObjectId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "ModuleId " + ErrorMessage.RequiredMessage)]
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Name { get; set; }

        public string ParentName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Caption " + ErrorMessage.RequiredMessage)]
        public string Caption { get; set; }
        public string URL { get; set; }
        public long ParentObjectId { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }

        [RegularExpression(RegularExpression.NumberRegularExpression, ErrorMessage = "Object Order " + ErrorMessage.NumberMessage)]
        public byte? ObjectOrder { get; set; }
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string ObjectImage { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "ObjectLevel " + ErrorMessage.RequiredMessage)]
        public Int32 ObjectLevel { get; set; }

        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public bool IsWithoutSearch { get; set; }
        public List<ISelectListItem> ModuleList { get; set; }
        public List<ISelectListItem> ParentObjectList { get; set; }
        public List<ISelectListItem> ObjectLevelList { get; set; }
    }
}
