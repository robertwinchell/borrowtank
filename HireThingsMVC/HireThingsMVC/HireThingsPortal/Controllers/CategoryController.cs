using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class CategoryController : AuthorizedController<ICategoryService>
    {
        ICategoryService _service = null;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            ViewData[Constant.FormTitle] = "ADD Category";
            ICategoryModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CategoryModel model,  CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (CategoryModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }
            ModelState.Clear();

            model = (CategoryModel) await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {
                if (model.CategoryId == 0)
                    model.CategoryId = null;
                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot  be Saved. Make sure the 'Serial No' isn't being duplicated.";
            }
           else
            {
                ViewData[Constant.FormTitle] = "Edit Category";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> IndexEdit(CancellationToken cancellationToken)
        {
            return Redirect("~/CategorySearch/Index");
        }


        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnCategoryId, CancellationToken cancellationToken)
        {
            if (System.Convert.ToInt64(hdnCategoryId) > 0)
            {
                ViewData[Constant.FormTitle] = "Edit Category";
                ICategoryModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, hdnCategoryId, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/CategorySearch/Index");
            }

        }

    }
}