using ASOL.HireThings.Model;
using ASOL.HireThings.Portal.Controllers;
using ASOL.HireThings.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class ItemDeleteController   : BaseSearchController<IItemDeleteService>
    {
        IItemDeleteService _service = null;

        public ItemDeleteController(IItemDeleteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(long ItemDeleteId,CancellationToken cancellationToken)
        {
            IItemDeleteModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context,ItemDeleteId, GetCanellationToken(cancellationToken));
            return PartialView("_ItemDeleteConfirmBox", model);
        }

        [HttpPost]
        public async Task<bool> Index(ItemDeleteModel modal,CancellationToken cancellationToken)
        {
            IItemDeleteModel model = await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, modal, GetCanellationToken(cancellationToken));
            return true;
        }
    }
}