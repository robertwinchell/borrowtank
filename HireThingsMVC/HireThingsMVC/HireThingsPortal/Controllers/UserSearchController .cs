using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class UserSearchController : BaseSearchController<IUserSearchService>
    {
        IUserSearchService _service = null;

        public UserSearchController(IUserSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string UserId, CancellationToken cancellationToken)
            {

            CancellationToken someToken = GetCanellationToken(cancellationToken);
            IUserSearchViewModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(UserSearchViewModel model, string UserId, CancellationToken cancellationToken)
        {

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterUserSearch(SearchModel model, CancellationToken cancellationToken)
        {
            model.UserId = System.Convert.ToInt64(User.Identity.GetUserId());
            IUserSearchViewModel userModel = await _service.SearchAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            return PartialView("_UserGrid", userModel.UserList);
        }
    }
}