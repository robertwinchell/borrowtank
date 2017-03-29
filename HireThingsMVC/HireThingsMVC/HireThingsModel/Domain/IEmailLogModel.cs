
namespace ASOL.HireThings.Model
{
    public interface IEmailLogModel : IBaseModel
    {
        //long UserId { get; set; }
        long SystemId { get; set; }
        long OrganizationId { get; set; }
        long WSId { get; set; }
        string To { get; set; }
        string Cc { get; set; }
        string Bcc { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        short SentStatus { get; set; }
        string UnSentReason { get; set; }
        int EmailType { get; set; }

    }
}