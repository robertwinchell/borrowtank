using ASOL.HireThings.Portal.Utils;


namespace ASOL.HireThings.Portal.Controllers
{
    [PageSizeActionFilter]
    public class BaseSearchController<T> : AuthorizedController<T> 
    {
       
    }
}