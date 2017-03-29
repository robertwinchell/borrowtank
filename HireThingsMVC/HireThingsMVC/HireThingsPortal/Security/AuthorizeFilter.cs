using System.Web;
using System.Web.Mvc;
using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using Microsoft.AspNet.Identity;
using System.IO;

namespace ASOL.HireThings.Portal
{
    /// <summary>
    /// Class to facilitate authorization of MVC controls 
    /// </summary>
    /// 
    public class AuthorizeFilter : AuthorizeAttribute
    {
        GlobalDAL _dal;
        string objName = string.Empty;

        string controller;
        string action;
        long userId;
        bool isPublicUser=false;

        public AuthorizeFilter(string url = "")
        {
            objName = url;
            _dal = new GlobalDAL();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Solution for Ajax call session time out 
            if (httpContext.Request.IsAjaxRequest())
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    string[] currUrl = httpContext.Request.UrlReferrer.AbsolutePath.Split('/');
                    string authority = httpContext.Request.UrlReferrer.Authority;
                    string scheme = httpContext.Request.UrlReferrer.Scheme;
                    string returnUrl = "";
                    string url = httpContext.Request.ApplicationPath;
                    UriHostNameType hostType = httpContext.Request.UrlReferrer.HostNameType;

                    string loginUrl = string.Format("{0}://{1}{2}/{3}/{4}", scheme, authority, url, Constant.LoginController, Constant.LoginAction);

                    // Set ReturnUrl for LocalHost and IIS application 
                    if (currUrl.Length > 2)
                    {
                        if (hostType == UriHostNameType.Dns)
                            returnUrl = string.Format("/{0}/{1}", currUrl[1], currUrl[2]);
                        else
                            returnUrl = string.Format("{0}/{1}/{2}", url, currUrl[2], currUrl[3]);
                    }

                    loginUrl = string.Format("{0}?ReturnUrl={1}", loginUrl, returnUrl);

                    HttpContext.Current.Response.Write("<script>window.location.href='" + loginUrl + "';</script>");
                }
            }
            

            userId = Convert.ToInt64(httpContext.User.Identity.GetUserId());

            AuthModel authObj = new AuthModel();

            var rd = httpContext.Request.RequestContext.RouteData;
            action = rd.GetRequiredString("action");
            controller = rd.GetRequiredString("controller");

            if (controller == "ItemDelete")
            {
                httpContext.Items[Constant.AuthObj] = authObj;
                return true;
            }

            if (objName == Constant.AllowAllUser)
            {
                return true;
            }

            if (string.IsNullOrEmpty(objName))
            {
                objName = controller.ToLower().Replace("search", "");
            }

            authObj = _dal.AuthorizeObject(userId, objName, action.ToLower().Trim());

            if (authObj == null) //Object not found
                return false;

            httpContext.Items[Constant.AuthObj] = authObj;
            isPublicUser = authObj.IsPublicUser;

            if (controller.Contains("Search")) // Search Controller 
            {
                if (action.ToLower() == "delete")
                    return authObj.AllowDelete;
                else
                    return true;
            }
            else
            {
                if (!authObj.IsWithoutSearch)
                    if (action == "delete")
                        return authObj.AllowDelete;
                    else
                        return authObj.AllowWrite;
                else
                    return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
           

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

            
                filterContext.Result = new RedirectResult(urlHelper.Action("NotFound", "Error"));
               

            }
           
        }

    }
}
