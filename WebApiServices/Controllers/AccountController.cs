using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Net.Http;
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
                if (response.Status && response.ResponseData.IsAuthenticated)
                {
                    //response.ResponseData.JWToken = GlobalHelper.GetMappedToken(response.ResponseData.UserId, loginRequest.Username);

                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalHelper.Issuer);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var postParams = new { grant_type = "password", username = loginRequest.Username, password = loginRequest.Password };

                    var authServerResponse = client.PostAsJsonAsync("oauth/token", postParams);

                    if (authServerResponse.Result.IsSuccessStatusCode)
                    {
                        response.ResponseData.JWToken = authServerResponse.Result.ToString();
                    }
                    else
                        response.ResponseData.IsAuthenticated = false;

                }
                else
                    response.ResponseData.IsAuthenticated = false;

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
