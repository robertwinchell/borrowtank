using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASOL.HireThings.Portal
{
    public class PublicBaseController<T> : AsyncController
    {
        // On every controller level exception --
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    try
        //    {
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            filterContext.ExceptionHandled = true;

        //            ASOL.HireThings.Logger.Logger.Log(filterContext.Exception);
        //            if (Session[Constant.RoleId] != null && Session[Constant.RoleId].ToString() == "70043")
        //            {
        //                Response.Redirect("~/Error/PublicNotFound", true);
        //            }
        //            else {
        //                Response.Redirect("~/Error/NotFound", true);
        //            }
                      
        //        }
        //    }
        //    catch (Exception ex) { }
        //}
        protected CancellationToken GetCanellationToken(CancellationToken cancellationToken)
        {
            CancellationToken disconnectedToken = Response.ClientDisconnectedToken;
            var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, disconnectedToken);
            return source.Token;
        }
    }
}