

using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public interface IExportGridViewModel : IBaseModel
    {
        IExportGridMetaInfo exportGridMetaInfo { get; set; }
        
        //Result values
        List<List<IExportGridModel>> exportGridList { get; set; }
    }
}
