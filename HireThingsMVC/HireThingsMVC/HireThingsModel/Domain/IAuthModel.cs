
namespace ASOL.HireThings.Model
{
    public interface IAuthModel : IBaseModel
    {
        bool AllowWrite { get; set; }
        bool AllowDelete { get; set; }
        string ObjectName { get; set; }
        string URL { get; set; }
        bool IsWithoutSearch { get; set; }
    }
}



