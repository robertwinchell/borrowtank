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
    [Authorize]
    public class BaseController<T> : PublicBaseController<T>
    {
        public BaseController() { }
       // public BaseController(T service) { _service = service; }


       
      

    }
}