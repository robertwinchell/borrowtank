namespace ASOL.HireThings.Model
{
    public interface IExportGridMetaInfo : ISearchModel
    {
        string SystemName { get; set; }
        string OrganizationName { get; set; }
        string DeviceName { get; set; }
        string LocalizedDateTime { get; set; }
        string FormTitle { get; set; }

    }

    public class ExportGridMetaInfo : SearchModel, IExportGridMetaInfo
    {
        public string SystemName { get; set; }
        public string OrganizationName { get; set; }
        public string DeviceName { get; set; }
        public string LocalizedDateTime { get; set; }
        public string FormTitle { get; set; }
    }
}
