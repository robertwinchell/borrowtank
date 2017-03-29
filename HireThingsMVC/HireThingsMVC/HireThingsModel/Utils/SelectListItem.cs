using System;
namespace ASOL.HireThings.Model
{
    public class SelectListItem : ISelectListItem
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Identifier { get; set; }
        public long SavedValue { get; set; }
        public bool IsOther { get; set; }
        public override bool Equals(object other)
        {
            SelectListItem item = (SelectListItem)other;
            // Check whether the compared object is null.

            if (Object.ReferenceEquals(other, null)) return false;

            // Check whether the compared object references the same data.

            if (Object.ReferenceEquals(this, other)) return true;

            // Check whether the objects’ properties are equal.

            return Value.Equals(item.Value);

        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
    public class ValueId : IValueId
    {
        public double Price { get; set; }
        public long Id { get; set; }
    }
    }
