using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class RoleController : AuthorizedController<IRoleService>
    {
        IRoleService _service = null;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Role";
            IRoleModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(RoleModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (RoleModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }



            ModelState.Clear();
            model = (RoleModel) await _service.SaveAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved. Make sure the 'Name' isn't being duplicated.";
                if (model.RoleId == 0)
                    model.RoleId = null;
            }
            else
            {
                ViewData[Constant.FormTitle] = "EDIT Role";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/RoleSearch/Index");
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(long hdRoleId, CancellationToken cancellationToken)
        {
            if (hdRoleId > 0)
            {
                ViewData[Constant.FormTitle] = "EDIT Role";
                IRoleModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdRoleId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/RoleSearch/Index");
            }
        }
    }
}