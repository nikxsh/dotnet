using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiServices.Models;

namespace WebApiServices.Controllers
{
    [RoutePrefix("api/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private EntityFrameworkDemo.IForumRepository _forumRepository;
        public UsersController(EntityFrameworkDemo.IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var users = _forumRepository.GetUsers();
            return Ok(users);
        }


        [Route("add")]
        [HttpPost]
        public IHttpActionResult Save([FromBody]User user)
        {
            _forumRepository.SaveUser(new EntityFrameworkDemo.User
            {
                Id = Guid.NewGuid(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = user.Dob
            });
            return Ok("Success");
        }
    }
}
