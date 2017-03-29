using ASOL.HireThings.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal.Utils
{
    public class PageSizeActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            DropDownListDAL _ddlDAL = new DropDownListDAL();
            if (filterContext.HttpContext.Session[Sessions.DDLPageSize] == null)
            {
                var pageSizeDDL = _ddlDAL.GetPageSizeList();
                if (pageSizeDDL != null)
                {
                    filterContext.HttpContext.Session[Sessions.DDLPageSize] = pageSizeDDL;
                }
            }
        }
    }

    public class ValidateAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                return;

            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var errorModel =
                        from x in modelState.Keys
                        where modelState[x].Errors.Count > 0
                        select new
                        {
                            key = x,
                            errors = modelState[x].Errors.
                                                   Select(y => y.ErrorMessage).
                                                   ToArray()
                        };
                filterContext.Result = new JsonResult()
                {
                    Data = errorModel
                };
                filterContext.HttpContext.Response.StatusCode =
                                                      (int)HttpStatusCode.ExpectationFailed;
            }
        }
    }
}