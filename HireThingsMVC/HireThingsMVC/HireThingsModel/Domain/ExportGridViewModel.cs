

using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class ExportGridViewModel : BaseModel, IExportGridViewModel
    {

        public IExportGridMetaInfo exportGridMetaInfo { get; set; }
        public List<List<IExportGridModel>> exportGridList { get; set; }
    }
}
