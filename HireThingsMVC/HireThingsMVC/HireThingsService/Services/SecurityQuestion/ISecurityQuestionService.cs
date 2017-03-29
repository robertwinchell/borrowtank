using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface ISecurityQuestionService : IBaseAsyncService<ISecurityQuestionModel>
    {
        Task<ISecurityQuestionModel> IndexAsync(HttpContext context, long securityQuestionId, CancellationToken cancellationToken);
        Task<ISecurityQuestionModel> SaveAsync(HttpContext context, ISecurityQuestionModel model, CancellationToken cancellationToken);
    }
}
