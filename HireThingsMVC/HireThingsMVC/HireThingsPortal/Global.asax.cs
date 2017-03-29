using ASOL.HireThings.Model;
using ASOL.HireThings.Portal;
using ASOL.HireThings.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HireThingsPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Tell data annotations not to add implicit required att for non nullable dates etc
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            RegisterCustomControllerFactory();
            //SeedUser();

            
        }

        private void RegisterCustomControllerFactory()
        {
            IControllerFactory factory = new CustomControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();
        //    //Log error here
        //    ASOL.HireThings.Logger.Logger.Log(ex);
            
        //    Response.Redirect("~/Error/NotFound", true);
        //    return;
        //}
       
        /// <summary>
        /// Add default user only if does not Exists 
        /// </summary>
        private void SeedUser()
        {
            //UserManager<IHireThingsUser> _userManager = new UserManager<IHireThingsUser>(new CustomUserStore());
            //var userToInsert = new HireThingsUser { UserName = "admin@hirethings.com", Pin = "Pas$w0rd", RoleId = 1, FirstName = "Awais", LastName = "Malik", EmailId = "Awais@hirethings.com", IsActive = true, IsEmailConfirmed=true, AddUserId = 1, SecurityAnswer = "Answer" };
            //_userManager.Create(userToInsert, "Pas$w0rd");
        }

    
  
    }
}
