using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class ErrorService : BaseService<IErrorViewModel>, IErrorService
    {
        public async override Task<IErrorViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IErrorViewModel model = new ErrorViewModel();
            return model;
        }

        
    }
}
