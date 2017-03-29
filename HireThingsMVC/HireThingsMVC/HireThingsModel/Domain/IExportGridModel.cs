

namespace ASOL.HireThings.Model
{
    public interface IExportGridModel : IBaseModel
    {
     
        
        //Result values
        string HeaderTitle { get; set; }
        string Name { get; set; }
        int Order { get; set; }
        object Value { get; set; }
    }
}
