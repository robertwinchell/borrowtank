using Microsoft.Owin;
using Owin;
using ASOL.HireThings.Portal.Utils;

[assembly: OwinStartupAttribute(typeof(ASOL.HIRETHINGS.PORTAL.Startup))]
namespace ASOL.HIRETHINGS.PORTAL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
