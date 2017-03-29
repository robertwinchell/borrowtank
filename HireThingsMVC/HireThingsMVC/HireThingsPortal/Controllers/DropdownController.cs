using ASOL.HireThings.Model;
using ASOL.HireThings.Service;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOL.HireThings.Portal
{

    public class DropdownController : PublicBaseController<IDropdownService>
    {
        IDropdownService _service = null;

        public DropdownController(IDropdownService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> PopulateLocationDropDownList(DropdownModel dModel, CancellationToken cancellationToken)
        {
            IDropdownModel model = await _service.PopulateLocationDropDownListAsync(dModel, GetCanellationToken(cancellationToken));
            model.DropdownList[0].Identifier = "ddlLocation";

            return PartialView("~/Views/Shared/_Partial/_ddlWithClick.cshtml", model.DropdownList);
        }

       
        

        [HttpGet]
        public async Task<ActionResult> PopulateParentObjectDropDownListAsync(DropdownModel objModel, CancellationToken cancellationToken)
        {
            IDropdownModel model = await _service.PopulateParentObjectDropDownListAsync(objModel, GetCanellationToken(cancellationToken));
            model.DropdownList[0].Identifier = "ddlParentObject";

            return PartialView("~/Views/Shared/_Partial/_ddlWithClick.cshtml", model.DropdownList);
        }

        [HttpGet]
        public async Task<ActionResult> PopulateParentObjectLevelDropDownList(DropdownModel objModel, CancellationToken cancellationToken)
        {
            IDropdownModel model = await _service.PopulateParentObjectLevelDropDownListAsync(objModel, GetCanellationToken(cancellationToken));
            model.DropdownList[0].Identifier = "ddlParentObject";

            return PartialView("~/Views/Shared/_Partial/_ddlWithClick.cshtml", model.DropdownList);
        }

    }
}