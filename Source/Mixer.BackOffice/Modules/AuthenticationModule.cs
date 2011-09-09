using System;
using Mixer.BackOffice.Infrastructure.Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace Mixer.BackOffice.Modules
{
    public class MyUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class AuthenticationModule : MixerModule
    {
        public AuthenticationModule()
        {
            Get["/login"] = _ =>
            {
                return View["Login"];
            };

            Post["/login"] = req =>
            {
                var user = this.Bind<MyUser>();

                var userGuid = Guid.NewGuid();
                var expiry = DateTime.Now.AddDays(7);

                return this.LoginAndRedirect(userGuid, expiry);
            };

            Get["/logout"] = x =>
            {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}