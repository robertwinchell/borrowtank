using ASOL.HireThings.Core;
using ASOL.HireThings.Service;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{
    public class ErrorController : BaseController<IErrorService>
    {
        IErrorService _service = null;

        public ErrorController(IErrorService service)
        {
            _service = service;
        }

        public ActionResult NotFound()
        {
            ViewData[Constant.HeaderTitle]  = "Page not Found!";
            return View();
        }
        public ActionResult PublicNotFound()
        {
            ViewData[Constant.HeaderTitle] = "Page not Found!";
            return View();
        }
    }
}