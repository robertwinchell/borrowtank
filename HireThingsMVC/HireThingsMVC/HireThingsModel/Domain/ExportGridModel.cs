

namespace ASOL.HireThings.Model
{
    public class ExportGridModel : BaseModel, IExportGridModel
    {
    

        public string HeaderTitle { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public object Value { get; set; }
    }
}
