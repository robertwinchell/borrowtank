using System;

namespace ASOL.HireThings.Model
{
    public interface IItemDeleteModel : IBaseModel
    {

        
        string ObjectName { get; set; }
        string Reason { get; set; }
        long RecordId { get; set; }
        //bool IsActive { get; set; }
        //long AddUserId { get; set; }
        //DateTime AddDate { get; set; }
        //long UpdateUserId { get; set; }
        //DateTime UpdateDate { get; set; }
    }
}
