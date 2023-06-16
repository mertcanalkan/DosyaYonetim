using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;

namespace DosyaYonetim.Auth
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var uyeServis = new UyeService();
            var uye = uyeServis.UyeOturumAc(context.UserName,context.Password);

            List<string> uyeYetkileri = new List<string>();

            if (uye != null)
            {
                string yetki = "";
                if (uye.UyeAdmin == 1)
                {
                    yetki = "Admin";
                }
                else
                {
                    yetki = "Uye";
                }
                uyeYetkileri.Add(yetki);


                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, yetki));
                identity.AddClaim(new Claim(ClaimTypes.PrimarySid,uye.UyeId.ToString()));

                AuthenticationProperties prop = new AuthenticationProperties(new Dictionary<string, string>
                {

                    {"uyeId",uye.UyeId.ToString() },
                    {"uyeKadi", uye.KullaniciAdi },
                    {"uyeYetkileri", Newtonsoft.Json.JsonConvert.SerializeObject(uyeYetkileri)}
                });
                AuthenticationTicket ticket = new AuthenticationTicket(identity, prop);


                context.Validated(ticket);
            }
            else
            {
                context.SetError("Geçersiz istek !", "Hatalı kullanıcı bilgisi !");
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> prop in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(prop.Key, prop.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}