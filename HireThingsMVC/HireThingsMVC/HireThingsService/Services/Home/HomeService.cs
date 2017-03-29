using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class HomeService : BaseService<IHomeModel> , IHomeService
    {
        public async override Task<IHomeModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
