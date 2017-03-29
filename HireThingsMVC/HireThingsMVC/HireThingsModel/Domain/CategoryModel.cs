using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASOL.HireThings.Model
{
    public class CategoryModel : BaseModel , ICategoryModel
    {
        public long? CategoryId { get; set; }
        public long? ParentCategoryId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Theme " + ErrorMessage.RequiredMessage)]
        public long ThemeId { get; set; }        
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Serial No " + ErrorMessage.RequiredMessage)]
        [RegularExpression(RegularExpression.AlphaNumericRegularExpression, ErrorMessage = ErrorMessage.AlphaNumericMessage)]
        public string CategorySerialNo { get; set; }

        [Required(ErrorMessage = "Description " + ErrorMessage.RequiredMessage)]
        public string CategoryDesc { get; set; }

       
        public long AddUserId { get; set; }
        public DateTime AddDate { get; set; }
        public long UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Theme { get; set; }
        public bool ShowOnHomepage { get; set; }
        public bool IsActive { get; set; }

       
        public List<ISelectListItem> ThemeList { get; set; }
       
        public override bool Equals(object other)
        {
            CategoryModel Category = (CategoryModel)other;
            // Check whether the compared object is null.

            if (Object.ReferenceEquals(other, null)) return false;

            // Check whether the compared object references the same data.

            if (Object.ReferenceEquals(this, other)) return true;

            // Check whether the objects’ properties are equal.

            return CategoryId.Equals(Category.CategoryId);

        }

        public override int GetHashCode()
        {
            return CategoryId.GetHashCode();
        }

        public static bool operator == (CategoryModel model,CategoryModel model2)
        {
            return model.CategoryId == model2.CategoryId;
        }

        public static bool operator !=(CategoryModel model, CategoryModel model2)
        {
            return model.CategoryId != model2.CategoryId;
        }
    }
}
