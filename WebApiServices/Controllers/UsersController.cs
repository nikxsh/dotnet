using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiServices.Models;

namespace WebApiServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private EFDataStorage.Contracts.IUserRepository _userRepository;
        public UsersController(EFDataStorage.Contracts.IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult GetUsers([FromBody]PagingRequest pagingRequest)
        {
            var result = _userRepository.GetUsers(pagingRequest.PageSize, pagingRequest.PageNumber, pagingRequest.SearchString);
            return Ok(result);
        }

        [Route("{keyword}/search")]
        [HttpGet]
        public IHttpActionResult GetUserMetaData(string keyword)
        {
            var result = _userRepository.GlobalSearch(keyword);
            return Ok(result);
        }

        [Route("count")]
        [HttpGet]
        public IHttpActionResult GetUserCount()
        {
            var userCount = _userRepository.GetUserCount();
            return Ok(userCount);
        }

        [Route("{userId:Guid}")]
        [HttpGet]
        public IHttpActionResult GetUserById(Guid userId)
        {
            var user = _userRepository.GetUserById(userId);
            var result = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = user.Dob.ToString("MM/dd/yyyy")
            };
            return Ok(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Save([FromBody]User user)
        {
            _userRepository.SaveUser(new EFDataStorage.Entities.User
            {
                Id = Guid.NewGuid(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = DateTime.Parse(user.Dob)
            });
            return Ok("Success");
        }

        [Route("edit")]
        [HttpPost]
        public IHttpActionResult Edit([FromBody]User user)
        {
            _userRepository.EditUser(new EFDataStorage.Entities.User
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = DateTime.Parse(user.Dob)
            });
            return Ok("Success");
        }

        [Route("{userId:Guid}/delete")]
        [HttpPost]
        public IHttpActionResult Delete(Guid userId)
        {
            _userRepository.DeleteUser(userId);
            return Ok("Success");
        }
    }
}
