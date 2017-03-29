
namespace ASOL.HireThings.Model
{
    public class EmailLogModel : BaseModel, IEmailLogModel
    {
        //public long UserId { get; set; }
        public long SystemId { get; set; }
        public long OrganizationId { get; set; }
        public long WSId { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public short SentStatus { get; set; }
        public string UnSentReason { get; set; }
        public int EmailType { get; set; }
        
    }
}