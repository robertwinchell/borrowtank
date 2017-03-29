using ASOL.HireThings.Core;
using System.Web.Mvc;
using System.Web.Routing;

namespace HireThingsPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "PublicAdvertisement",
               url: "CreateAdvertisement",

               defaults: new
               {
                   controller = "PublicAdvertisement",
                   action = "Index",
                   id = UrlParameter.Optional
               }
           );
            routes.MapRoute(
               name: "Browse",
               url: "Browse/{searchterm}",

               defaults: new
               {
                   controller = "PublicAdvertisementSearch",
                   action = "Index",
                   id = UrlParameter.Optional,
                   searchterm=""
               }
           );
            routes.MapRoute(
               name: "BrowseTheme",
               url: "Browse/Theme/{themeid}",

               defaults: new
               {
                   controller = "PublicAdvertisementSearch",
                   action = "Index",
                   id = UrlParameter.Optional,
                   themeid = ""
               }
           );
            routes.MapRoute(
             name: "BrowseCategory",
             url: "Browse/Category/{categoryid}",

             defaults: new
             {
                 controller = "PublicAdvertisementSearch",
                 action = "Index",
                 id = UrlParameter.Optional,
                 categoryid = ""
             }
         );
            routes.MapRoute(
               name: "Register",
               url: "Register",

               defaults: new
               {
                   controller = "Account",
                   action = "PublicRegister",
                   id = UrlParameter.Optional
               }
           );

            routes.MapRoute(
              name: "AdvertisementDetail",
              url: "AdvertisementDetail/{advertisementId}",

              defaults: new
              {
                  controller = "PublicAdvertisement",
                  action = "Detail",
                  advertisementId=""
              }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = Constant.MainController, action = Constant.MainAction, id = UrlParameter.Optional }
            );
        }
    }
}
