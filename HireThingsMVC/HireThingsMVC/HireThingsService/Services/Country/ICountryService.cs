using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface ICountryService : IBaseAsyncService<ICountryModel>
    {
        Task<ICountryModel> SaveAsync(HttpContext context, ICountryModel model, CancellationToken cancellationToken);
        Task<ICountryModel> IndexAsync(HttpContext context, CancellationToken cancellationToken, string countryId);
    }
}
