using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOL.HireThings.Portal.App_Start
{
    public class AuthConfig
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            // do other configuration 
        }
    }
}