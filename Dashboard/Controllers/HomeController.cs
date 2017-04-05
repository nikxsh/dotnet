using Dashboard.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Dashboard() => View();

        public ActionResult Table() => View();
        public ActionResult AngularTable() => View();

        public ActionResult Flot() => View();
        public ActionResult Morris() => View();

        public ActionResult Forms() => View();

        public ActionResult Panels() => View();

        public ActionResult Buttons() => View();

        public ActionResult Notifications() => View();

        public ActionResult Typography() => View();

        public ActionResult Icons() => View();

        public ActionResult Grid() => View();

        public ActionResult Forum() => View();

        [HttpPost]
        public ActionResult Forum(Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var dataobject = new EFDataStorage.Repositories.ForumRepository();
            dataobject.SubmitTopic(new EFDataStorage.Entities.Topic
            {
                Id = Guid.NewGuid(),
                Title = topic.Title,
                Body = topic.Body,
                Created = DateTime.Now
            });

            ModelState.Clear();
            var topicList = dataobject.GetTopics().Select(x => new Topic { Id = x.Id, Title = x.Title });
            return PartialView("GetTopicsHeader", topicList);
        }

        [HttpGet]
        public ActionResult SubmitReply(Guid id)
        {
            return PartialView(new Reply { TopicId = id });
        }

        [HttpPost]
        public ActionResult SaveReply(Reply reply)
        {
            if (!ModelState.IsValid)
            {
                return View(reply);
            }

            var dataobject = new EFDataStorage.Repositories.ForumRepository();
            dataobject.SubmitReply(new EFDataStorage.Entities.Reply { Id = Guid.NewGuid(), Body = reply.Body, Created = DateTime.Now, TopicId = reply.TopicId });
            return RedirectToActionPermanent("GetTopics", new { topicId = reply.TopicId });
        }

        public ActionResult GetTopicsHeader()
        {
            var dataobject = new EFDataStorage.Repositories.ForumRepository();
            var topic = dataobject.GetTopics().Select(x => new Topic { Id = x.Id, Title = x.Title });
            return PartialView(topic);
        }

        public ActionResult GetTopics(Guid topicId)
        {
            var dataobject = new EFDataStorage.Repositories.ForumRepository();
            var topic = dataobject.GetTopicById(topicId);
            var result = new Topic
            {
                Id = topic.Id,
                Title = topic.Title,
                Body = topic.Body,
                Created = topic.Created,
                Replies = dataobject.GetRepliesByTopic(topicId).Select(x => new Reply
                {
                    Id = x.Id,
                    Body = x.Body,
                    Created = x.Created
                }).OrderByDescending(x => x.Created)
            };
            return View(result);
        }

    }
}