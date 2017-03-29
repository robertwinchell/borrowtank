using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace ASOL.HireThings.Portal
{
    [AuthorizeFilter]
    public class AuthorizedController<T> : BaseController<T>
    {
        public AuthorizedController() { }
        public AuthorizedController(T service) { }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var rd = filterContext.RequestContext.RouteData;
            string action = rd.GetRequiredString("action");
            AuthModel auth = this.HttpContext.Items[Constant.AuthObj] as AuthModel;
            ViewBag.AuthObj = auth;
            ViewData[Constant.HeaderTitle] = auth.ObjectName;
            base.OnResultExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session[Constant.ObjectModel] == null)
            {
                MenuDAL dal = new MenuDAL();
                IMenuModel model = dal.SelectMenu(Convert.ToInt64(User.Identity.GetUserId()));
                Session[Constant.ObjectModel] = model;
            }
        }
    }
}