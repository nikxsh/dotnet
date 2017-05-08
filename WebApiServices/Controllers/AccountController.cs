using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;
using WebApiServices.Providers;

namespace WebApiServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly ISecurityAdapter _securityAdapter;
        
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody]LoginRequest loginRequest)
        {
            var response = new ResponseBase<LoginResponse> { Status = false };

            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<LoginRequest>(loginRequest));
                response = _securityAdapter.Authenticate(request);
                if(response.Status && response.ResponseData.IsAuthenticated)
                {
                    var tokenProvider = new JWTokenProvider(ConfigurationManager.AppSettings["Issuer"]);

                    var identity = new ClaimsIdentity("Password");
                    identity.AddClaim(new Claim("userid", response.ResponseData.UserId.ToString()));
                    identity.AddClaim(new Claim("username", loginRequest.Username));

                    var authProp = new AuthenticationProperties()
                    {
                         IssuedUtc = DateTime.UtcNow,
                         ExpiresUtc = DateTime.UtcNow.AddDays(1)
                    };

                    var ticket = new AuthenticationTicket(identity, authProp);
                    response.ResponseData.JWToken = tokenProvider.Protect(ticket);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseData.IsAuthenticated = false;
                return Ok(ex.ToErrorResponse());
            }
        }
    }
}
