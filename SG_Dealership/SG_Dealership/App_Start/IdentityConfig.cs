using Data;
using Data.Repos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Models.Identity;
using Owin;


namespace SG_Dealership.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new EntityRepo());

            app.CreatePerOwinContext<UserManager<AppUser>>((options, context) =>
                new UserManager<AppUser>(
                    new UserStore<AppUser>(context.Get<EntityRepo>())));

            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
            new RoleManager<AppRole>(
                new RoleStore<AppRole>(context.Get<EntityRepo>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login"),
            });
        }
    }
}