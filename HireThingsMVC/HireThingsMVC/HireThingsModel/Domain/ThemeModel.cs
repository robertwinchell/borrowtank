using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class ThemeModel : BaseModel, IThemeModel
    {
        public long? ThemeId { get; set; }

        [Required(ErrorMessage = "Name " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Name { get; set; }

        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Code " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = "Code " + ErrorMessage.AlphaNumericMessage)]
        public string Code { get; set; }

        public bool IsActive { get; set; }
        public long AdduserId { get; set; }
        public DateTime AddDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
