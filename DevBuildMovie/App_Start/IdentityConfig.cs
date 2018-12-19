using DevBuildMovie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBuildMovie.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DevBuildMovieDBEntities());
            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
            new RoleManager<AppRole>(new RoleStore<AppRole>(context.Get<DevBuildMovieDBEntities>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Home/Login"),
            });
        }
    }
}