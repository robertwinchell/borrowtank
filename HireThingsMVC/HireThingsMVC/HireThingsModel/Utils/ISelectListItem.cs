using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface ISelectListItem
    {
        bool Selected { get; set; }
        string Text { get; set; }
        string Value { get; set; }
        string Identifier { get; set; }
        long SavedValue { get; set; }
        bool IsOther { get; set; }
    }

    public interface IValueId
    {
        double Price { get; set; }
        long Id { get; set; }
    }
}
