using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/Dhashboard")]
    public class DashboardController : ApiController
    {
        private EntityFrameworkDemo.IForumRepository _forumRepository;
        public DashboardController(EntityFrameworkDemo.IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [Route("topics")]
        public IEnumerable<Topic> Get()
        {
            var topic = _forumRepository.GetTopics().Select(x => new Topic { Id = x.Id, Title = x.Title, Body = x.Body, Created = x.Created });
            return topic;
        }

        [Route("topics/{topicId:Guid}/replies")]
        [HttpGet]
        public IEnumerable<Reply> GetReplies(Guid topicId)
        {
            var replies = _forumRepository.GetRepliesByTopic(topicId).Select(x => new Reply { Id = x.Id, Body = x.Body, Created = x.Created });
            return replies;
        }

        [Route("topics/Send")]
        [HttpPost]
        public IHttpActionResult SubmitTopic(Topic topic)
        {
            _forumRepository.SubmitTopic(new EntityFrameworkDemo.Topic { Id = Guid.NewGuid(), Title = topic.Title, Body = topic.Body, Created = DateTime.Now });
            return Ok("Success");
        }

        [Route("users")]
        [HttpGet]
        public IHttpActionResult GetUserData()
        {
            var users = _forumRepository.GetUsers();
            return Ok(users);
        }
    }
}
