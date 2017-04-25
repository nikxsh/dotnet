using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiServices.Contracts;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly ISecurityAdapter _securityAdapter;
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpPost]
        public IHttpActionResult Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<LoginRequest>(loginRequest));
                var response = _securityAdapter.Authenticate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }
    }
}
