using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class AdvertisementController : AuthorizedController<IAdvertisementService>
    {
        IAdvertisementService _service = null;

        public AdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }
    [HttpGet]
        public ActionResult IndexEdit()
        {          
            return Redirect("~/AdvertisementSearch/Index");
        }

        [HttpPost]
        public async Task<string> Index(AdvertisementModel model,CancellationToken cancellationToken) {

            ResponseModel _response = new ResponseModel();
            if (ModelState.IsValid)
            {

                ModelState.Clear();
                model = (AdvertisementModel)await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];
                _response.isSuccess = Convert.ToBoolean(ViewData[Constant.QuerySuccess]);
                if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
                {

                _response.Message = "Error : Record cannot be Saved.";

                }
                else {
                    _response.Message = "Success : Record Saved Successfully.";
                }
                


                return Newtonsoft.Json.JsonConvert.SerializeObject(_response);

            }
            else
            {
                string errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList().ToString();

                _response.isSuccess = false;
                _response.Message = errors;
                return Newtonsoft.Json.JsonConvert.SerializeObject(_response);
            }
        }
    

       

        #region 

        [HttpGet]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            
            IAdvertisementModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            ViewData[Constant.FormTitle] = "ADD Advertisement";
            return View(model);
        }

        //[HttpPost]
        //public async Task<ActionResult> Index(AdvertisementModel model, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewData[Constant.CustomSuccessMessage] = Constant.CustomValidationErrorMessage;
        //        ViewData[Constant.QuerySuccess] = false;
        //        model = (AdvertisementModel) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
        //        return View(model);
        //    }

        //    ModelState.Clear();
        //    model =  (AdvertisementModel) await _service.SaveAsync(HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));
        //    ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
        //    ViewData[Constant.FormTitle] = HttpContext.Items[Constant.FormTitle];

        //    if (!Convert.ToBoolean(ViewData[Constant.QuerySuccess]))
        //    {

        //        ViewData[Constant.CustomSuccessMessage] = "Error : Record cannot be Saved. Make sure 'Advertisement Name' isn't being duplicated.";
               
        //    }
        //    else
        //    {
        //        ViewData[Constant.FormTitle] = "EDIT Advertisement";
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<ActionResult> IndexEdit(string hdnAdvertisementId, CancellationToken cancellationToken)
        {
     

            if (System.Convert.ToInt64(hdnAdvertisementId) > 0)
            {

                ViewData[Constant.FormTitle] = "EDIT Advertisement";
                IAdvertisementModel model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken), hdnAdvertisementId);
                ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                return View("Index", model);
            }
            else
            {
                return Redirect("~/AdvertisementSearch/Index");
            }
        }

        #endregion

    }
}