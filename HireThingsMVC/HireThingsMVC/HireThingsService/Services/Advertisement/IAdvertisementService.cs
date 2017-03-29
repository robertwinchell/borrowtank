using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IAdvertisementService : IBaseAsyncService<IAdvertisementModel>


    {
        Task<IAdvertisementModel> SaveAsync(HttpContext context, IAdvertisementModel model, CancellationToken cancellationToken);
        Task<IAdvertisementModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string AdvertisementId);

    }
}
