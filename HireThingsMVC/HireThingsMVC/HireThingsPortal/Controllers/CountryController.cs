using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class CountryController : AuthorizedController<ICountryService>
    {
        ICountryService _service = null;

        public CountryController(ICountryService service)
        {
            _service = service;
        }
    [HttpGet]
        public ActionResult IndexEdit()
        {          
            return Redirect("~/CountrySearch/Index");
        }


    

       

        #region 

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            
            ICountryModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            ViewData[Constant.FormTitle] = "ADD Country";
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CountryModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
                ViewData[Constant.QuerySuccess] = false;
                model = (CountryModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
                return View(model);
            }

            ModelState.Clear();
            model =  (CountryModel) await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
            ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

            if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
            {

                ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved. Make sure 'Country Name' isn't being duplicated.";
                if (model.CountryId == 0)
                    model.CountryId = null;
            }
            else
            {
                ViewData[Constant.FormTitle] = "EDIT Country";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnCountryId, CancellationToken cancellationToken)
        {
     

            if (System.Convert.ToInt64(hdnCountryId) > 0)
            {

                ViewData[Constant.FormTitle] = "EDIT Country";
                ICountryModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken), hdnCountryId);
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/CountrySearch/Index");
            }
        }

        #endregion

    }
}