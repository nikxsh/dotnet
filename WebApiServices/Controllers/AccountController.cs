using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;

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

                    var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string> ("grant_type","password"),
                        new KeyValuePair<string, string> ("username",request.Data.Username),
                        new KeyValuePair<string, string> ("password",request.Data.Password)
                    };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalHelper.Issuer);

                    var postParams = new HttpRequestMessage(HttpMethod.Post, "oauth/token");
                    postParams.Content = new FormUrlEncodedContent(formData);

                    var authServerResponse = client.SendAsync(postParams).Result;

                    if (authServerResponse.IsSuccessStatusCode)
                    {
                        var jsonObject = authServerResponse.Content.ReadAsStringAsync().Result;
                        var oauthResult = new JavaScriptSerializer().Deserialize<OAuthResponse>(jsonObject);
                        response.ResponseData.JWToken = oauthResult.access_token;
                    }
                    else
                    {
                        response.ResponseData.IsAuthenticated = false;
                        response.Message = "Token Authentication failed.";
                    }
                }
                else
                {
                    response.ResponseData.IsAuthenticated = false;
                    response.Message = "Username or Password is incorrect.";
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
