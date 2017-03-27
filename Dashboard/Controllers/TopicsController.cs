using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/topics")]
    public class TopicsController : ApiController
    {
        private EntityFrameworkDemo.IForumRepository _forumRepository;
        public TopicsController(EntityFrameworkDemo.IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public IEnumerable<Topic> Get()
        {
            var topic = _forumRepository.GetTopics().Select(x => new Topic { Id = x.Id, Title = x.Title, Body = x.Body, Created = x.Created });
            return topic;
        }

        [Route("{topicId:Guid}/replies")]
        [HttpGet]
        public IEnumerable<Reply> GetReplies(Guid topicId)
        {
            var replies = _forumRepository.GetRepliesByTopic(topicId).Select(x => new Reply { Id = x.Id, Body = x.Body, Created = x.Created });
            return replies;
        }

        [Route("Send")]
        [HttpPost]
        public IHttpActionResult SubmitTopic(Topic topic)
        {
            _forumRepository.SubmitTopic(new EntityFrameworkDemo.Topic { Id = Guid.NewGuid(), Title = topic.Title, Body = topic.Body, Created = DateTime.Now });
            return Ok("Success");
        }
    }
}
