using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ASOL.HireThings.Service;
using System.Threading.Tasks;
using System.Threading;

namespace ASOL.HireThings.Portal
{
    public class HomeController : BaseController<IHomeService>
    {
        IHomeService _service = null;

        public HomeController(IHomeService service)
        {
            _service = service;
        }

        // GET: Home
        [AuthorizeFilter(Constant.AllowAllUser)]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            if (Session[Constant.ObjectModel] == null)
            {
                MenuDAL dal = new MenuDAL();
                IMenuModel model = await dal.SelectMenuAsync(Convert.ToInt64(User.Identity.GetUserId()), GetCanellationToken(cancellationToken));
                Session[Constant.ObjectModel] = model;
            }

            return View();
        }
    }
}