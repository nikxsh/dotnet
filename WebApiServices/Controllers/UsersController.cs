using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiServices.Adapter;
using WebApiServices.Helper;
using WebApiServices.Models;

namespace WebApiServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserAdapter _userAdapter;

        public UsersController(IUserAdapter UserAdapter)
        {
            _userAdapter = UserAdapter;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult GetUsers([FromBody]PagingRequest pagingRequest)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<PagingRequest>(pagingRequest));
                var response = _userAdapter.GetUsers(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("{keyword}/search")]
        [HttpGet]
        public IHttpActionResult GetUserMetaData(string keyword)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<string>(keyword));
                var result = _userAdapter.GlobalSearch(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("count")]
        [HttpGet]
        public IHttpActionResult GetUserCount()
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase());
                var userCount = _userAdapter.GetUserCount(request);
                return Ok(userCount);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("{userId:Guid}")]
        [HttpGet]
        public IHttpActionResult GetUserById(Guid userId)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<Guid>(userId));
                var result = _userAdapter.GetUserById(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Save([FromBody]User user)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<User>(user));
                var result = _userAdapter.SaveUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("edit")]
        [HttpPost]
        public IHttpActionResult Edit([FromBody]User user)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<User>(user));
                var result = _userAdapter.EditUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }

        [Route("{userId:Guid}/delete")]
        [HttpPost]
        public IHttpActionResult Delete(Guid userId)
        {
            try
            {
                var request = UserManager.PrepareRequest(new RequestBase<Guid>(userId));
                var result = _userAdapter.DeleteUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToErrorResponse());
            }
        }
    }
}
