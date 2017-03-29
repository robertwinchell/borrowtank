using ASOL.HireThings.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public interface IThemeService : IBaseAsyncService<IThemeModel>
    {
        Task<IThemeModel> IndexAsync(HttpContext context, string ThemeId, CancellationToken cancellationToken);
        Task<IThemeModel> SaveAsync(HttpContext context, IThemeModel model, CancellationToken cancellationToken);
    }
}
