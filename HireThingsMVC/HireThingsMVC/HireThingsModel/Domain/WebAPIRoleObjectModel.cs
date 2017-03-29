using System;

namespace ASOL.HireThings.Model
{
    public class RoleObjectModel : BaseModel, IRoleObjectModel
    {
        public long RoleObjectId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long ObjectID { get; set; }
        public string ObjectName { get; set; }
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public bool ShowActive { get; set; }
        public bool ShowWrite { get; set; }
        public bool ShowDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsChange { get; set; }
        public long ParentObjectId { get; set; }
        public Byte ObjectOrder { get; set; }
        public int ObjectLevel { get; set; }


    }
}
