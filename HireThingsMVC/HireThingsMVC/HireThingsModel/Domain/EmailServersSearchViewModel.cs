using System.Collections.Generic;

namespace ASOL.HireThings.Model
{
    public class EmailServerSearchViewModel : BaseModel , IEmailServerSearchViewModel
    {
        public List<IEmailServerModel> EmailServerList { get; set; }
        public long SearchId { get; set; }
        public List<ISelectListItem> DDLSearch { get; set; }
        public string TxtSearch { get; set; }     
    }
}
