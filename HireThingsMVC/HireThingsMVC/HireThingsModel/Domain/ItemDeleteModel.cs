


namespace ASOL.HireThings.Model
{
    public class ItemDeleteModel:BaseModel, IItemDeleteModel
    {
        public string ObjectName { get; set; }
        public string Reason { get; set; }
        public long RecordId { get; set; }
     
    }
}
