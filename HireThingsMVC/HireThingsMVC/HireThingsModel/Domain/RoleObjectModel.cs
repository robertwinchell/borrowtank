using System;

namespace ASOL.HireThings.Model
{
    public class WebAPIRoleObjectModel : BaseModel, IWebAPIRoleObjectModel
    {
        public long RoleObjectId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long ObjectID { get; set; }
        public string ObjectName { get; set; }
        public bool AllowGet { get; set; }
        public bool AllowPost { get; set; }
        public bool AllowPut { get; set; }
        public bool AllowDelete { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public bool ShowGet { get; set; }
        public bool ShowPost { get; set; }
        public bool ShowPut { get; set; }
        public bool ShowDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsChange { get; set; }
    }
}
